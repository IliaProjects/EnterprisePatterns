using Clinic.DataGateway;
using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.UpdateModels.UpdateFactories
{
    internal class PatientUpdateFactory
    {
        private Patient _patient;
        public PatientUpdateFactory(Patient patient)
        {
            _patient = patient;
        }
        public Patient update(PatientUpdateModel model)
        {
            if (model.bFullName)
            {
                _patient.FullName = model.FullName;
            }
            if (model.bPhoneNr)
            {
                _patient.PhoneNr = model.PhoneNr;
            }
            if (model.bBirthDate)
            {
                _patient.BirthDate = model.BirthDate;
            }
            if (model.bVisitAdd)
            {
                _patient.Visits.Add(model.VisitAdd);
            }
            if (model.bVisitRemove)
            {
                _patient.Visits.Remove(_patient.Visits.Single(v => v.Id == model.VisitRemoveId));
            }
            return _patient;
        }
    }
}
