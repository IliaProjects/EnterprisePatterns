using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataGateway
{
    internal class VisitMapper : DataMapperBase, IDataMapper<Visit>
    {

        public Visit Get(int id)
        {
            return _dbContext.Visits.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<Visit> Find<FType>(string column, FType value)
        {
            if (column == "dateTime")
            {
                return _dbContext.Visits.Where(v => v.Time.Equals(value));
            }
            if (column == "doctor")
            {
                return _dbContext.Visits.Where(v => v.DoctorId.Equals(value));
            }
            if (column == "patient")
            {
                return _dbContext.Visits.Where(v => v.PatientId.Equals(value));
            }
            throw new Exception($"Table Visits has no column '{column}'");
        }
        public IQueryable<Visit> GetAll()
        {
            return _dbContext.Visits;
        }

        public bool Insert(Visit item)
        {
            _dbContext.Visits.Add(item);
            return TryToSaveDataChanges();
        }

        public bool Update(Visit item)
        {
            var visit = _dbContext.Visits.Single(v => v.Id == item.Id);
            visit.Time = item.Time;
            visit.Doctor = item.Doctor;
            visit.Patient = item.Patient;
            return TryToSaveDataChanges();
        }
        public bool Delete(int id)
        {
            var visit = _dbContext.Visits.FirstOrDefault(v => v.Id == id);
            if (visit != null)
            {
                _dbContext.Visits.Remove(visit);
                return TryToSaveDataChanges();
            }
            return false;
        }
    }
}
