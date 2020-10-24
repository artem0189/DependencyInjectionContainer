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

    public class DependenciesConfiguration
    {
        private Dictionary<int, List<Type>> _dependencies;
        private static Dictionary<DependencyLifeTime, Type> _lifeTypes;

        public DependenciesConfiguration()
        {
            _dependencies = new Dictionary<int, List<Type>>();
            _lifeTypes = new Dictionary<DependencyLifeTime, Type>()
            {
                { DependencyLifeTime.InstancePerDependency, typeof(InstancePerDependency<>) },
                { DependencyLifeTime.Singletone, typeof(Singletone<>) }
            };
        }

        public void Register<T, F>(DependencyLifeTime dependencyLifeTime)
        {
            Register(typeof(T), typeof(F), dependencyLifeTime);
        }

        public void Register(Type dependencyType, Type implementationType, DependencyLifeTime dependencyLifeTime)
        {
            int metadataToken = dependencyType.MetadataToken;
            if (_dependencies.ContainsKey(metadataToken))
            {
                if (!_dependencies[metadataToken].Where(type => type.GenericTypeArguments.First().MetadataToken == implementationType.MetadataToken).Any())
                {
                    _dependencies[metadataToken].Add(_lifeTypes[dependencyLifeTime].MakeGenericType(implementationType));
                }
            }
            else
            {
                _dependencies.Add(metadataToken, new List<Type>() { _lifeTypes[dependencyLifeTime].MakeGenericType(implementationType) });
            }
        }

        internal bool TryGetValue(Type type, out List<Type> implementations)
        {
            return _dependencies.TryGetValue(type.MetadataToken, out implementations);
        }
    }
}
