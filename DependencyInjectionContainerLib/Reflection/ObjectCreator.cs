using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainerLib.Reflection
{
    internal static class ObjectCreator
    {
        internal static object CreateInstance(Type type)
        {
            return CreateInstance(type, new Type[] { }, new object[] { });
        }

        internal static object CreateInstance(Type type, Type[] genericArguments, object[] constructorParams)
        {
            object instance = null;
            if (type.IsGenericTypeDefinition)
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
