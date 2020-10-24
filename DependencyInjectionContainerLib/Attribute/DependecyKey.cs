using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainerLib.Attribute
{
    public class DependecyKey : System.Attribute
    {
        public uint Number { get; private set; }

        public DependecyKey(uint number)
        {
            Number = number;
        }
    }
}
