using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    internal class FirstFirst : Name
    {
        public FirstFirst(string username)
        {
            var index = username.Trim().IndexOf(" ");

            if (index <= 0) return;

            firstName = username.Substring(0, index).Trim();
            lastName = username.Substring(index + 1).Trim();
        }
    }
}
