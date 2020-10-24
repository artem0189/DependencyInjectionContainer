using System;
using System.Linq;
using System.Collections.Generic;
using DependencyInjectionContainerLib.Implementation;

namespace DependencyInjectionContainerLib
{
    public enum DependencyLifeTime
    {
        InstancePerDependency,
        Singletone
    }

    internal struct Impl
    {
        internal Type Type { get; }
        internal int DependencyName { get; }

        internal Impl(Type type, uint? dependencyName = null)
        {
            Type = type;
            DependencyName = dependencyName != null ? Convert.ToInt32(dependencyName) : -1;
        }
    }

    public class DependenciesConfiguration
    {
        private Dictionary<int, List<Impl>> _dependencies;
        private static Dictionary<DependencyLifeTime, Type> _lifeTypes;

        public DependenciesConfiguration()
        {
            _dependencies = new Dictionary<int, List<Impl>>();
            _lifeTypes = new Dictionary<DependencyLifeTime, Type>()
            {
                { DependencyLifeTime.InstancePerDependency, typeof(InstancePerDependency<>) },
                { DependencyLifeTime.Singletone, typeof(Singletone<>) }
            };
        }

        public void Register<T, F>(DependencyLifeTime dependencyLifeTime, uint? dependencyName = null)
        {
            Register(typeof(T), typeof(F), dependencyLifeTime, dependencyName);
        }

        public void Register(Type dependencyType, Type implementationType, DependencyLifeTime dependencyLifeTime, uint? dependencyName = null)
        {
            int metadataToken = dependencyType.MetadataToken;
            if (_dependencies.ContainsKey(metadataToken))
            {
                if (!_dependencies[metadataToken].Where(type => type.Type.GenericTypeArguments.First().MetadataToken == implementationType.MetadataToken).Any())
                {
                    _dependencies[metadataToken].Add(new Impl(_lifeTypes[dependencyLifeTime].MakeGenericType(implementationType), dependencyName));
                }
            }
            else
            {
                _dependencies.Add(metadataToken, new List<Impl>() { new Impl(_lifeTypes[dependencyLifeTime].MakeGenericType(implementationType), dependencyName) });
            }
        }

        internal bool TryGetValue(Type type, out List<Type> implementations, int dependencyName = -1)
        {
            implementations = null;
            List<Impl> impls = new List<Impl>(); 
            if (_dependencies.TryGetValue(type.MetadataToken, out impls))
            {
                implementations = impls.Where(impl => impl.DependencyName == dependencyName).Select(impl => impl.Type).ToList();
            }
            return implementations != null && implementations.Count != 0;
        }
    }
}
