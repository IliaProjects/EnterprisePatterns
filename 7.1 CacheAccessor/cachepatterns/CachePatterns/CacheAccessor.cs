using CachePatterns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachePatterns
{
    public class CacheAccessor : IDataAccessor
    {
        private IDataAccessor _accessor;
        public CacheAccessor(IDataAccessor accessor)
        {
            _accessor = accessor;
        }
        public Book getBook(int id)
        {
            return _accessor.getBook(id);
        }

        public Magazine getMagazine(int id)
        {
            return _accessor.getMagazine(id);
        }
    }
}
