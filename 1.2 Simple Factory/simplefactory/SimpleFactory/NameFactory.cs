using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    internal class NameFactory
    {
        public Name getName(string name)
        {
            if (name.Contains(",")) return new LastFirst(name);

            return new FirstFirst(name);
        }
    }
}
