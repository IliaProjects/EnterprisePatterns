using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataGateway
{
    public interface IActiveRecord<T>
    {
        public IEnumerable<T> GetAll();
        public IEnumerable<T> Find<FType>(string column, FType value);
        public T Get(int id);
        public int Insert(T item);
        public bool Update(T item);
        public bool Delete(int id);
    }
}
