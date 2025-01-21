using Clinic.DataGateway;
using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Services
{

    public class PatientService
    {
        IDataMapper<Patient> _patientMapper;
        IDataMapper<Visit> _visitMapper;

        public PatientService()
        {
            _patientMapper = new PatientMapper();
            _visitMapper = new VisitMapper();
        }
        public IEnumerable<Patient> getAllPatients()
        {
            return _patientMapper.GetAll();
        }
        public Patient findPatientByPhone(string phoneNr)
        {
            return _patientMapper.Find("phoneNr", phoneNr).FirstOrDefault();
        }
        public IEnumerable<Visit> getPatientsVisits(int patientId)
        {
            return _patientMapper.Get(patientId).Visits;
        }
        public Patient newPatient(string fullName, string phoneNr)
        {
            Patient patient = new Patient()
            {
                FullName = fullName,
                PhoneNr = phoneNr,
            };
            _patientMapper.Insert(patient);
            return patient;
        }
        public Visit newVisit(DateTime time, int doctorId, int patientsId)
        {
            Visit visit = new Visit()
            {
                DoctorId = doctorId,
                PatientId = patientsId,
                Time = time,
            };
            _visitMapper.Insert(visit);
            return visit;
        }
        public void updatePatient(Patient patient)
        {
            _patientMapper.Update(patient);
        }
        public void updateVisitTime(DateTime newTime, int visitId)
        {
            var visit = _visitMapper.Get(visitId);
            visit.Time = newTime;
            _visitMapper.Update(visit);
        }
        public void deletePatient(int patientId)
        {
            _patientMapper.Delete(patientId);
        }
        public void deleteVisit(int visitId)
        {
            _visitMapper.Delete(visitId);
        }
    }
}
