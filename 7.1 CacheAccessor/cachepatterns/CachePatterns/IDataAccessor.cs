using CachePatterns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachePatterns
{
    public interface IDataAccessor
    {
        public Book getBook(int id);
        public Magazine getMagazine(int id);
    }
}
