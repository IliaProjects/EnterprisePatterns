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
        DoctorService _doctorService;
        IActiveRecord<Patient> patientRecord;
        IActiveRecord<Visit> visitRecord;
        public PatientService(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public IEnumerable<Patient> getAllPatients()
        {
            return patientRecord.GetAll();
        }
        public Patient findPatientByPhone(string phoneNr)
        {
            return patientRecord.Find("phoneNr", phoneNr).FirstOrDefault();
        }
        public Patient newPatient(string fullName, string phoneNr)
        {
            Patient patient = new Patient()
            {
                FullName = fullName,
                PhoneNr = phoneNr,
            };
            patient.Id = patientRecord.Insert(patient);
            return patient;
        }
        public void updatePatient(Patient patient)
        {
            patientRecord.Update(patient);
        }
        public void deletePatient(int patientId)
        {
            patientRecord.Delete(patientId);
        }
        public IEnumerable<Visit> getPatientsVisits(int patientId)
        {
            return patientRecord.Get(patientId).Visits;
        }
        public Visit newVisit(DateTime time, int doctorId, int patientsId)
        {
            Visit visit = new Visit()
            {
                DoctorId = doctorId,
                PatientId = patientsId,
                Time = time,
            };
            visit.Id = visitRecord.Insert(visit);
            return visit;
        }
        public void updateVisitTime(DateTime newTime, int visitId)
        {
            var visit = visitRecord.Get(visitId);
            visit.Time = newTime;
            visitRecord.Update(visit);
        }
        public void deleteVisit(int visitId)
        {
            visitRecord.Delete(visitId);
        }
    }
}
