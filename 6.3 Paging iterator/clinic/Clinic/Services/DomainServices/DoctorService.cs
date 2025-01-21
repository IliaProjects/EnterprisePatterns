using Clinic.DataGateway;
using Clinic.Models.Domain;
using Clinic.Models.SearchModels;
using Clinic.Models.SearchModels.SearchFactories;
using Clinic.Models.UpdateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Services.DomainServices
{
    public class DoctorService
    {
        private IDataMapper<Doctor> _doctorMapper;

        public DoctorService()
        {
            _doctorMapper = new DoctorMapper();
        }
        public IQueryable<Doctor> searchDoctors(DoctorSearchModel searchModel)
        {
            return new DoctorSearchFactory(_doctorMapper).newSelection(searchModel);
        }

        public IEnumerable<Doctor> getAllDoctors()
        {
            return _doctorMapper.GetAll();
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
        public Doctor newDoctor(string fullName, DateTime hired)
        {
            var doctor = new Doctor()
            {
                FullName = fullName,
                Hired = hired,
            };
            _doctorMapper.Insert(doctor);
            return doctor;
        }
        public void updateDoctor(DoctorUpdateModel doctor)
        {
            _doctorMapper.Update(doctor);
        }
        public void deleteDoctor(int doctorId)
        {
            _doctorMapper.Delete(doctorId);
        }
    }
}
