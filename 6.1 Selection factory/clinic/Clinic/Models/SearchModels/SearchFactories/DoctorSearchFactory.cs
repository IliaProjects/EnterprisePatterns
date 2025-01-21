using Clinic.DataGateway;
using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.SearchModels.SearchFactories
{
    internal class DoctorSearchFactory
    {
        IDataMapper<Doctor> _doctorMapper;
        public DoctorSearchFactory(IDataMapper<Doctor> doctorMapper) {
            _doctorMapper = doctorMapper;
        }

        public IQueryable<Doctor> newSelection(DoctorSearchModel searchModel)
        {
            var result = _doctorMapper.GetAll();

            if (searchModel.bFullName)
            {
                result = result.Where(x => x.FullName.Contains(searchModel.FullName));
            }
            if (searchModel.bSpecialities)
            {
                var _result = new List<Doctor>();
                foreach (var sp in searchModel.Specialities)
                {
                    var spDoctors = result.Where(x => x.Specialities.Any(s => s.Name.Equals(sp.Name)));
                    foreach (var spDoctor in spDoctors)
                    {
                        if(!_result.Any(d=> d.Id.Equals(spDoctor.Id)))
                            _result.Add(spDoctor);
                    }
                }
                result = _result.AsQueryable();

            }
            if (searchModel.bMinStageYears)
            {
                result = result.Where(x => ((DateTime.Now - x.Hired).Days / 365) >= searchModel.MinStageYears);
            }
            if (searchModel.bMaxStageYears)
            {
                result = result.Where(x => ((DateTime.Now - x.Hired).Days / 365) <= searchModel.MaxStageYears);
            }
            if (searchModel.bVisitDate)
            {
                result = result.Where(x => x.Visits.Any(v => v.Time.Equals(searchModel.VisitDate)));
            }
            if (searchModel.bVisitsFrom)
            {
                result = result.Where(x => x.Visits.Any(v => v.Time >= searchModel.VisitsFrom));
            }
            if (searchModel.bVisitsTo)
            {
                result = result.Where(x => x.Visits.Any(v => v.Time <= searchModel.VisitsTo));
            }
            return result;
        }
    }
}
