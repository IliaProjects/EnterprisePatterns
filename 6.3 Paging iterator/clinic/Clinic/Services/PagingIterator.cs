using Clinic.Models;
using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Services
{
    public class PagingIterator<T>
    {
        private IQueryable<T> _q;
        private int _pageNr;
        private int _pageSize;
        public PagingIterator(IQueryable<T> q, int pageNr = 1, int pageSize = 10)
        {
            _q = q;
            _pageNr = pageNr;
            _pageSize = pageSize;
        }

        public IQueryable getGroup()
        {
            int recSkip = (_pageNr - 1) * _pageSize;
            return _q.Skip(recSkip).Take(_pageSize);
        }

        public PagerViewModel getPager()
        {
            return new PagerViewModel(_q.Count(), _pageNr, _pageSize);
        }
    }
}
