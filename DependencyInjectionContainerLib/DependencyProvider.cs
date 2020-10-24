using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using DependencyInjectionContainerLib.Reflection;
using DependencyInjectionContainerLib.Implementation;

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

        private object Resolve(Type type, bool isCreateAllImplementations)
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
                IDependencyLife dependencyLifeObject = null;
                if (isCreateAllImplementations)
                {
                    instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
                    for (int i = 0; i < implementations.Count; i++)
                    {
                        dependencyLifeObject = (IDependencyLife)ObjectCreator.CreateInstance(implementations[i]);
                        (instance as IList).Add(dependencyLifeObject.GetInstance(type.GetGenericArguments(), GetConstructorParams(implementations[i])));
                    }
                }
                else
                {
                    dependencyLifeObject = (IDependencyLife)ObjectCreator.CreateInstance(implementations[0]);
                    instance = dependencyLifeObject.GetInstance(type.GetGenericArguments(), GetConstructorParams(implementations[0]));
                }
            }
            return instance;
        }

        private object[] GetConstructorParams(Type type)
        {
            object[] constructorParams = new object[0];
            List<object> buffParams = new List<object>();
            ConstructorInfo[] constructors = type.GetConstructors().OrderByDescending(cn => cn.GetParameters().Length).ToArray();
            for (int i = 0; i < constructors.Length; i++)
            {
                ParameterInfo[] parameters = constructors[i].GetParameters();
                for (int j = 0; j < parameters.Length; j++)
                {
                    buffParams.Add(Resolve(parameters[j].ParameterType, false));
                }
                if (constructorParams.Where(pr => pr != null).Count() <= buffParams.Where(pr => pr != null).Count())
                {
                    constructorParams = buffParams.ToArray();
                }
                buffParams.Clear();
            }
            return constructorParams;
        }
    }
}
