using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainerLib.Attribute
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class DependecyKeyAttribute : System.Attribute
    {
        public ushort Number { get; private set; }

        public DependecyKeyAttribute(ushort number)
        {
            Number = number;
        }
    }
}
