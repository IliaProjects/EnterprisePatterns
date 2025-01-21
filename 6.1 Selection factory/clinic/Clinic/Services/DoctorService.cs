using Clinic.DataGateway;
using Clinic.Models.Domain;
using Clinic.Models.SearchModels;
using Clinic.Models.SearchModels.SearchFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Services
{
    public class DoctorService
    {
        private IDataMapper<Doctor> _doctorMapper;
        private IDataMapper<Speciality> _specialitiesMapper;

        public DoctorService()
        {
            _doctorMapper = new DoctorMapper();
            _specialitiesMapper = new SpecialityMapper();
        }
        public IQueryable<Doctor> searchDoctors(DoctorSearchModel searchModel)
        {
            return new DoctorSearchFactory(_doctorMapper).newSelection(searchModel);
        }
        public IEnumerable<Doctor> getAllDoctors()
        {
            return _doctorMapper.GetAll();
        }
        public IEnumerable<Speciality> getAllSpecialities()
        {
            return _specialitiesMapper.GetAll();
        }
        public Doctor getDoctor(int doctorId)
        {
            return _doctorMapper.Get(doctorId);
        }
        public IEnumerable<Visit> getDoctorsVisits(int doctorId)
        {
            return _doctorMapper.Get(doctorId).Visits;
        }
        public IEnumerable<Speciality> getDoctorsSpecialities(int doctorId)
        {
            return _doctorMapper.Get(doctorId).Specialities;
        }
        public Doctor newDoctor(string fullName)
        {
            var doctor = new Doctor()
            {
                FullName = fullName,
            };
            _doctorMapper.Insert(doctor);
            return doctor;
        }
        public void updateDoctor(Doctor doctor)
        {
            _doctorMapper.Update(doctor);
        }
        public void deleteDoctor(int doctorId)
        {
            _doctorMapper.Delete(doctorId);
        }
        public Speciality addSpecialityToDoctor(int doctorId, string specialityName)
        {
            var doctor = _doctorMapper.Get(doctorId);
            var speciality = _specialitiesMapper.Find("name", specialityName).FirstOrDefault();
            if (speciality == null)
            {
                speciality = new Speciality()
                {
                    Name = specialityName,
                };
                _specialitiesMapper.Insert(speciality);
            }

            if (!doctor.Specialities.Any(s => s.Id == speciality.Id))
            {
                doctor.Specialities.Add(speciality);
                _doctorMapper.Update(doctor);
            }
            return speciality;
        }
        public void removeSpecialityFromDoctor(int doctorId, string specialityName)
        {
            var doctor = _doctorMapper.Get(doctorId);
            var speciality = _specialitiesMapper.Find("name", specialityName).FirstOrDefault();
            
            doctor.Specialities.Remove(speciality);
            _doctorMapper.Update(doctor);

            if (!speciality.Doctors.Any())
                _specialitiesMapper.Delete(speciality.Id);
        }
    }
}
