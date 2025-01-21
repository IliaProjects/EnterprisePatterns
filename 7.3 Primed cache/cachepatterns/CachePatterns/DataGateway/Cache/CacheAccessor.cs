using CachePatterns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachePatterns.DataGateway.Cache
{
    public class CacheAccessor : IDataAccessor
    {
        private IDataAccessor _accessor;

        private Dictionary<string, Book> _booksCache;
        private Dictionary<string, Magazine> _magazinesCache;
        public CacheAccessor(IDataAccessor accessor)
        {
            _accessor = accessor;
            _booksCache = new Dictionary<string, Book>();
            _magazinesCache = new Dictionary<string, Magazine>();
        }
        public Book getBook(int id)
        {
            IKeyFactory keyFactory = new BookKeyFactory();
            string partKey = keyFactory.NewPartialKey(id);

            if (!_booksCache.ContainsKey(partKey))
            {
                Book book = _accessor.getBook(id);
                _booksCache.Add(partKey, book);
            }
            
            return _booksCache[partKey];
        }

        public Magazine getMagazine(int id)
        {
            IKeyFactory keyFactory = new MagazineKeyFactory();
            string partKey = keyFactory.NewPartialKey(id);

            if (!_magazinesCache.ContainsKey(partKey))
            {
                Magazine magazine = _accessor.getMagazine(id);
                _magazinesCache.Add(partKey, magazine);
            }

            return _magazinesCache[partKey];
        }
    }
}
