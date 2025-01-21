using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPatterns.DataMapper
{
    public interface IDataMapper<T>
    {
            public IEnumerable<T> GetAll();
            public IEnumerable<T> Find(string key, string value);
            public T Get(string id);
            public void Insert(T item);
            public void Update(T item);
            public void Delete(string id);
    }
}
