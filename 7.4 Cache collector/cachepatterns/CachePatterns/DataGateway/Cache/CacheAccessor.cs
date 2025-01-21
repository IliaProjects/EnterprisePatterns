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

        private Dictionary<string, (Book , DateTime)> _booksCache;
        private Dictionary<string, (Magazine, DateTime)> _magazinesCache;

        private TimeSpan cacheTimeout = TimeSpan.FromMinutes(10);
        public CacheAccessor(IDataAccessor accessor)
        {
            _accessor = accessor;
            _booksCache = new Dictionary<string, (Book, DateTime)>();
            _magazinesCache = new Dictionary<string, (Magazine, DateTime)>();
            CacheCollectionTask();
        }
        public Book getBook(int id)
        {
            IKeyFactory keyFactory = new BookKeyFactory();
            string partKey = keyFactory.NewPartialKey(id);

            if (!_booksCache.ContainsKey(partKey))
            {
                Book book = _accessor.getBook(id);
                _booksCache.Add(partKey, (book, DateTime.UtcNow));
            }
            
            return _booksCache[partKey].Item1;
        }

        public Magazine getMagazine(int id)
        {
            IKeyFactory keyFactory = new MagazineKeyFactory();
            string partKey = keyFactory.NewPartialKey(id);

            if (!_magazinesCache.ContainsKey(partKey))
            {
                Magazine magazine = _accessor.getMagazine(id);
                _magazinesCache.Add(partKey, (magazine, DateTime.UtcNow));
            }

            return _magazinesCache[partKey].Item1;
        }

        public async Task CacheCollectionTask()
        {
            while (true)
            {
                await Task.Delay(1000);
                foreach (var record in _booksCache)
                {
                    if (record.Value.Item2.Add(cacheTimeout) < DateTime.UtcNow)
                    {
                        _booksCache.Remove(record.Key);
                    }
                }
                foreach (var record in _magazinesCache)
                {
                    if (record.Value.Item2.Add(cacheTimeout) < DateTime.UtcNow)
                    {
                        _booksCache.Remove(record.Key);
                    }
                }
            }
        }
    }
}
