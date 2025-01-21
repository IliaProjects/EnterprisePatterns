using Clinic.Models.Domain;
using Clinic.Models.UpdateModels;
using Clinic.Models.UpdateModels.UpdateFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataGateway
{
    internal class PatientMapper : DataMapperBase, IDataMapper<Patient>
    {
        public Patient Get(int id)
        {
            return _dbContext.Patients.FirstOrDefault(s => s.Id == id);
        }
        public IQueryable<Patient> Find<FType>(string column, FType value)
        {
            if (column == "fullName")
            {
                return _dbContext.Patients.Where(s => s.FullName.Equals(value));
            }
            if (column == "phoneNr")
            {
                return _dbContext.Patients.Where(s => s.PhoneNr.Equals(value));
            }
            if (column == "visit")
            {
                int visitId = Convert.ToInt32(value);
                return _dbContext.Patients.Where(s => s.Visits.Any(s => s.Id == visitId));
            }
            throw new Exception($"Table Patients has no column '{column}'");
        }

        public IQueryable<Patient> GetAll()
        {
            return _dbContext.Patients;
        }

        public bool Insert(Patient item)
        {
            _dbContext.Patients.Add(item);
            return TryToSaveDataChanges();
        }

        public bool Update(object item)
        {
            var model = (PatientUpdateModel)item;
            var patient = _dbContext.Patients.Single(p => p.Id == model.PatientId);
            new PatientUpdateFactory(patient).update(model);
            return TryToSaveDataChanges();
        }
        public bool Delete(int id)
        { 
            var patient = _dbContext.Patients.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                return TryToSaveDataChanges();
            }
            return false;
        }
    }
}
