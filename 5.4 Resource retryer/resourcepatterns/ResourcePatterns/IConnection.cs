using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePatterns
{
    public interface IConnection : IDisposable
    {
        public void Open();
        public void Close();
        public void ExecuteQuery();
    }
}
