using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainerLib.Attribute
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class DependecyKeyAttribute : System.Attribute
    {
        public uint Number { get; private set; }

        public DependecyKeyAttribute(uint number)
        {
            Number = number;
        }
    }
}
