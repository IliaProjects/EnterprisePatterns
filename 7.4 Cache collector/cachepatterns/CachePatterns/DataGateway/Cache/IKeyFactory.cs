using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachePatterns.DataGateway.Cache
{
    public interface IKeyFactory
    {
        public string NewSpecificKey(object domainObj);
        public string NewPartialKey(int id);
    }
}
