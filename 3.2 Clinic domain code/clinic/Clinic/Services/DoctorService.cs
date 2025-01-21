using Clinic.DataGateway;
using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Services
{
    public class DoctorService
    {
        PatientService _patientService;
        IActiveRecord<Doctor> _doctorRecord;
        IActiveRecord<Speciality> _specialitiesRecord;
        IActiveRecord<DoctorSpeciality> _doctorSpecialitiesRecord;
        IActiveRecord<Visit> _visitsRecord;

        public DoctorService(PatientService patientService)
        {
            _patientService = patientService;
        }
        public IEnumerable<Speciality> getSpecialities(int doctorId)
        {
            return _doctorRecord.Get(doctorId).Specialities;
        }
        public Speciality addSpeciality(int doctorId, string specialityName)
        {
            var speciality = _specialitiesRecord.Find("name", specialityName).FirstOrDefault();
            if (speciality == null)
            {
                speciality = new Speciality()
                {
                    Name = specialityName,
                };
                speciality.Id = _specialitiesRecord.Insert(speciality);
                _doctorSpecialitiesRecord.Insert(new DoctorSpeciality() 
                {
                    DoctorId = doctorId, 
                    SpecialityId = speciality.Id
                });
            }
            else
            {
                _doctorSpecialitiesRecord.Insert(new DoctorSpeciality() 
                { 
                    DoctorId = doctorId, 
                    SpecialityId = speciality.Id
                });
            }
            return speciality;
        }
        public void removeSpeciality(int doctorId, string specialityName)
        {
            var speciality = _specialitiesRecord.Find("name", specialityName).FirstOrDefault();
            int idOfBinding = _doctorSpecialitiesRecord.Find("doctorId", doctorId).FirstOrDefault(s => s.SpecialityId == speciality.Id).Id;
            _doctorSpecialitiesRecord.Delete(idOfBinding);
            
            if(!_doctorSpecialitiesRecord.Find("specialityId", speciality.Id).Any())
            {
                _specialitiesRecord.Delete(speciality.Id);
            }
        }
        public IEnumerable<Doctor> getAllDoctors()
        {
            return _doctorRecord.GetAll();
        }
        public IEnumerable<Doctor> getDoctors(string specialityName)
        {
            List<Doctor> doctors = new List<Doctor>();
            var speciality = _specialitiesRecord.Find("name", specialityName).FirstOrDefault();
            foreach (var doctorSpeciality in _doctorSpecialitiesRecord.Find("specialityId", speciality.Id))
            {
                doctors.Add(_doctorRecord.Get(doctorSpeciality.DoctorId));
            }
            return doctors;
        }
        public Doctor newDoctor(string fullName)
        {
            var doctor = new Doctor()
            {
                FullName = fullName,
            };
            doctor.Id = _doctorRecord.Insert(doctor);
            return doctor;
        }
        public void updateDoctor(Doctor doctor)
        {
            _doctorRecord.Update(doctor);
        }
        public void deleteDoctor(int doctorId)
        {
            _doctorRecord.Delete(doctorId);
        }
        public IEnumerable<Visit> getDoctorsVisits(int doctorId)
        {
            return _visitsRecord.Find("doctorId", doctorId);
        }
    }
}
