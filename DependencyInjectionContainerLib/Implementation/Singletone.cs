using System;
using System.Collections.Generic;
using System.Text;
using DependencyInjectionContainerLib.Reflection;

namespace DependencyInjectionContainerLib.Implementation
{
    internal class Singletone<T> : IDependencyLife
    {
        private static T instnace;

        public object GetInstance(Type[] genericArguments, object[] constructorParams)
        {
            if (instnace == null)
            {
                instnace = (T)ObjectCreator.CreateInstance(typeof(T), genericArguments, constructorParams);
            }
            return instnace;
        }
    }
}
