using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataGateway
{
    internal class SpecialityMapper : DataMapperBase, IDataMapper<Speciality>
    {
        public Speciality Get(int id)
        {
            return _dbContext.Specialities.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<Speciality> Find<FType>(string column, FType value)
        {
            if (column == "name")
            {
                return _dbContext.Specialities.Where(s => s.Name.Equals(value));
            }
            if (column == "doctor")
            {
                int doctorId = Convert.ToInt32(value);
                return _dbContext.Specialities.Where(s => s.Doctors.Any(s => s.Id == doctorId));
            }
            throw new Exception($"Table Specialities has no column '{column}'");
        }

        public IQueryable<Speciality> GetAll()
        {
            return _dbContext.Specialities;
        }

        public bool Insert(Speciality item)
        {
            _dbContext.Specialities.Add(item);
            return TryToSaveDataChanges();
        }

        public bool Update(object item)
        {
            var sp = (Speciality)item;
            var speciality = _dbContext.Specialities.Single(s => s.Id == sp.Id);
            speciality.Name = sp.Name;
            return TryToSaveDataChanges();
        }

        public bool Delete(int id)
        {
            var speciality = _dbContext.Specialities.FirstOrDefault(s => s.Id == id);
            if (speciality != null)
            {
                _dbContext.Specialities.Remove(speciality);
                return TryToSaveDataChanges();
            }
            return false;
        }
    }
}
