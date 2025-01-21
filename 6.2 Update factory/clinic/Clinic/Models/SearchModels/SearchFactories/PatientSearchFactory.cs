using Clinic.DataGateway;
using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.SearchModels.SearchFactories
{
    internal class PatientSearchFactory
    {
        IDataMapper<Patient> _patientMapper;
        public PatientSearchFactory(IDataMapper<Patient> patientMapper) {
            _patientMapper = patientMapper;
        }

        public IQueryable<Patient> newSelection(PatientSearchModel searchModel)
        {
            var result = _patientMapper.GetAll();
            if (searchModel.bFullName)
            {
                result = result.Where(x => x.FullName.Contains(searchModel.FullName));
            }
            if (searchModel.bPhoneNr)
            {
                result = result.Where(x => x.PhoneNr.Contains(searchModel.PhoneNr));
            }
            if (searchModel.bDoctorId)
            {
                result = result.Where(x => x.Visits.Any(v => v.DoctorId.Equals(searchModel.DoctorId)));
            }
            if (searchModel.bMinAge)
            {
                result = result.Where(x => ((DateTime.Now - x.BirthDate).Days / 365) >= searchModel.MinAge);
            }
            if (searchModel.bMaxAge)
            {
                result = result.Where(x => ((DateTime.Now - x.BirthDate).Days / 365) <= searchModel.MaxAge);
            }
            if (searchModel.bVisitDate)
            {
                result = result.Where(x => x.Visits.Any(v => v.Time.Equals(searchModel.VisitDate)));
            }
            if (searchModel.bVisitDateFrom)
            {
                result = result.Where(x => x.Visits.Any(v => v.Time >= searchModel.VisitDateFrom));
            }
            if (searchModel.bVisitDateTo)
            {
                result = result.Where(x => x.Visits.Any(v => v.Time <= searchModel.VisitDateTo));
            }
            return result;
        }
    }
}
