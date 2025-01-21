using CachePatterns.DataGateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachePatterns
{
    public class ClientWrapper
    {
        IDataAccessor _dataAccessor;
        public ClientWrapper(IDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }
        public void PrimeBooksCache(IEnumerable<int> ids)
        {
            foreach(int id in ids) 
            {
                _dataAccessor.getBook(id);
            }
        }
        public void PrimeMagazinesCache(IEnumerable<int> ids)
        {
            foreach (int id in ids)
            {
                _dataAccessor.getMagazine(id);
            }
        }
    }
}
