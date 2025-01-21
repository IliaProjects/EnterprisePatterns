using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPatterns.TableDataGateway
{
    public interface ITableGateway<T>
    {
        public IEnumerable<T> GetAll();
        public T Get(string id);
        public void Insert(T item);
        public void Update(T item);
        public void Delete(string id);
    }
}
