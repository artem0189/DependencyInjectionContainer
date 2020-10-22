using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DependencyInjectionContainerLib.Reflection;

namespace DependencyInjectionContainerLib
{
    public class DependencyProvider
    {
        private DependenciesConfiguration _dependencies;
        public DependencyProvider(DependenciesConfiguration dependencies)
        {
            // Container validation
            _dependencies = dependencies;
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T), false);
        }

        private object Resolve(Type type, bool isCreateAllImplementation)
        {
            object instance = null;
            List<Type> implementations = null;
            if (!_dependencies.TryGetValue(type, out implementations))
            {
                if (type.GetInterface("IEnumerable") != null)
                {
                    instance = Resolve(type.GetGenericArguments().First(), true);
                }
            }
            else
            {
                if (isCreateAllImplementation)
                {
                    instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
                    for (int i = 0; i < implementations.Count; i++)
                    {
                        (instance as IList).Add(ObjectCreator.CreateInstance(implementations[i], type.GetGenericArguments()));
                    }
                }
                else
                {
                    instance = ObjectCreator.CreateInstance(implementations[0], type.GetGenericArguments());
                }
            }
            return instance;
        }
    }
}
