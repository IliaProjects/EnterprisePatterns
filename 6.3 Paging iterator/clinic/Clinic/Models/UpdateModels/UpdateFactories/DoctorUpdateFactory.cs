using Clinic.DataGateway;
using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.UpdateModels.UpdateFactories
{
    internal class DoctorUpdateFactory
    {
        private Doctor _doctor;
        private IDataMapper<Speciality> _specialityMapper;
        public DoctorUpdateFactory(Doctor doctor)
        {
            _doctor = doctor;
            _specialityMapper = new SpecialityMapper();
        }
        public Doctor update(DoctorUpdateModel model)
        {
            if (model.bFullName) 
            {
                _doctor.FullName = model.FullName;
            }
            if (model.bHired) 
            {
                _doctor.Hired = model.Hired;            
            }
            if (model.bSpecialityAdd) 
            {
                var speciality = _specialityMapper.Get(model.SpecialityAddId);
                _doctor.Specialities.Add(speciality);
            }
            if (model.bSpecialityRemove) 
            {
                var speciality = _specialityMapper.Get(model.SpecialityRemoveId);
                _doctor.Specialities.Remove(speciality);
            }
            return _doctor;
        }
    }
}
