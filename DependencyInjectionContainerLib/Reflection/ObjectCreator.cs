using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainerLib.Reflection
{
    internal static class ObjectCreator
    {
        internal static object CreateInstance(Type type)
        {
            return CreateInstance(type, null, null);
        }

        internal static object CreateInstance(Type type, Type[] genericArguments)
        {
            return CreateInstance(type, genericArguments, null);
        }

        internal static object CreateInstance(Type type, object[] constructorParams)
        {
            return CreateInstance(type, null, constructorParams);
        }

        internal static object CreateInstance(Type type, Type[] genericArguments, object[] constructorParams)
        {
            object instance = null;
            if (type.IsGenericType)
            {
                instance = Activator.CreateInstance(type.MakeGenericType(genericArguments), constructorParams);
            }
            else
            {
                instance = Activator.CreateInstance(type, constructorParams);
            }
            return instance;
        }
    }
}
