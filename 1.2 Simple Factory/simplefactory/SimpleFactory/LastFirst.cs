using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    internal class LastFirst : Name
    {
        public LastFirst(string username)
        {
            var index = username.Trim().IndexOf(",");

            if (index <= 0) return;

            lastName = username.Substring(0, index).Trim();
            firstName = username.Substring(index + 1).Trim();
        }
    }
}
