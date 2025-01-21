using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataGateway
{
    public interface IDataMapper<T>
    {
        public IQueryable<T> GetAll();
        public IQueryable<T> Find<FType>(string column, FType value);
        public T Get(int id);
        public bool Insert(T item);
        public bool Update(T item);
        public bool Delete(int id);
    }
}
