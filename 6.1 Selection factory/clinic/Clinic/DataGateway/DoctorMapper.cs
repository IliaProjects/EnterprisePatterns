using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataGateway
{
    internal class DoctorMapper : DataMapperBase, IDataMapper<Doctor>
    {
        public Doctor Get(int id)
        {
            return _dbContext.Doctors.FirstOrDefault(s => s.Id == id);
        }
        public IQueryable<Doctor> Find<FType>(string column, FType value)
        {
            if (column == "fullName")
            {
                return _dbContext.Doctors.Where(s => s.FullName.Equals(value));
            }
            if (column == "speciality")
            {
                int specId = Convert.ToInt32(value);
                return _dbContext.Doctors.Where(s => s.Specialities.Any(s => s.Id == specId));
            }
            throw new Exception($"Table Doctors has no column '{column}'");
        }
        public IQueryable<Doctor> GetAll()
        {
            return _dbContext.Doctors;
        }

        public bool Insert(Doctor item)
        {
            _dbContext.Doctors.Add(item);
            return TryToSaveDataChanges();
        }

        public bool Update(Doctor item)
        {
            var doctor = _dbContext.Doctors.Single(d => d.Id == item.Id);
            doctor.FullName = item.FullName;
            doctor.Specialities = item.Specialities;
            return TryToSaveDataChanges();
        }
        public bool Delete(int id)
        {
            var doctor = _dbContext.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctor != null)
            {
                _dbContext.Doctors.Remove(doctor);
                return TryToSaveDataChanges();
            }
            return false;
        }
    }
}
