using Clinic.DataGateway;
using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Services
{
    internal class SpecialityService
    {
        private IDataMapper<Speciality> _specialitiesMapper;

        public SpecialityService()
        {
            _specialitiesMapper = new SpecialityMapper();
        }
        public IQueryable<Speciality> getAllSpecialities()
        {
            return _specialitiesMapper.GetAll();
        }
        public Speciality newSpeciality(string name)
        {
            var speciality = new Speciality();
            speciality.Name = name;
            _specialitiesMapper.Insert(speciality);
            return speciality;
        }
        public Speciality renameSpeciality(int id, string newName)
        {
            Speciality speciality = _specialitiesMapper.Get(id);
            speciality.Name = newName;
            _specialitiesMapper.Update(speciality);
            return speciality;
        }
        public void deleteSpeciality(int id)
        {
            _specialitiesMapper.Delete(id);
        }
    }
}
