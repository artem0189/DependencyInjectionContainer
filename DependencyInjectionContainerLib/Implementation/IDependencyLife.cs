using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainerLib.Implementation
{
    internal interface IDependencyLife
    {
        object GetInstance(Type[] genericArguments, object[] constructorParams);
    }
}
