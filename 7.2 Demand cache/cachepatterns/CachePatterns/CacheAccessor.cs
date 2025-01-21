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
        private static Dictionary<int, Book> _booksCache;
        private static Dictionary<int, Magazine> _magazinesCache;
        public CacheAccessor(IDataAccessor accessor)
        {
            _accessor = accessor;
            _booksCache = new Dictionary<int, Book>();
            _magazinesCache = new Dictionary<int, Magazine>();
        }
        public Book getBook(int id)
        {
            if (!_booksCache.ContainsKey(id))
            {
                _booksCache.Add(id, _accessor.getBook(id));
            }
            return _booksCache[id];
        }

        public Magazine getMagazine(int id)
        {
            if (!_magazinesCache.ContainsKey(id))
            {
                _magazinesCache.Add(id, _accessor.getMagazine(id));
            }
            return _magazinesCache[id];
        }
    }
}
