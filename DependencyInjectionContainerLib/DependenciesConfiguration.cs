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
        private static Dictionary<DependencyLifeTime, Type> _lifeTypes;
        private Dictionary<int, Dictionary<int, List<Type>>> _dependencies;

        public DependenciesConfiguration()
        {
            _dependencies = new Dictionary<int, Dictionary<int, List<Type>>>();
            _lifeTypes = new Dictionary<DependencyLifeTime, Type>()
            {
                { DependencyLifeTime.InstancePerDependency, typeof(InstancePerDependency<>) },
                { DependencyLifeTime.Singletone, typeof(Singletone<>) }
            };
        }

        public void Register<T, F>(DependencyLifeTime dependencyLifeTime, ushort? dependencyName = null)
        {
            Register(typeof(T), typeof(F), dependencyLifeTime, dependencyName);
        }

        public void Register(Type dependencyType, Type implementationType, DependencyLifeTime dependencyLifeTime, ushort? dependencyName = null)
        {
            int dependencyMetadataToken = dependencyType.MetadataToken;
            int implementationMetadataToken = implementationType.MetadataToken;
            int dependencyNameKey = dependencyName != null ? Convert.ToInt32(dependencyName) : -1;
            if (_dependencies.ContainsKey(dependencyMetadataToken))
            {
                if (_dependencies[dependencyMetadataToken].ContainsKey(dependencyNameKey))
                {
                    if (!_dependencies[dependencyMetadataToken][dependencyNameKey].Where(impl => impl.GenericTypeArguments.First().MetadataToken == implementationMetadataToken).Any())
                    {
                        _dependencies[dependencyMetadataToken][dependencyNameKey].Add(_lifeTypes[dependencyLifeTime].MakeGenericType(implementationType));
                    }
                }
                else
                {
                    _dependencies[dependencyMetadataToken].Add(dependencyNameKey, new List<Type> { _lifeTypes[dependencyLifeTime].MakeGenericType(implementationType) });
                }
            }
            else
            {
                _dependencies.Add(dependencyMetadataToken, new Dictionary<int, List<Type>> { { dependencyNameKey, new List<Type> { _lifeTypes[dependencyLifeTime].MakeGenericType(implementationType) } } });
            }
        }

        internal bool TryGetValue(Type type, out List<Type> implementations, int dependencyName = -1)
        {
            implementations = null;
            int typeMetadataToken = type.MetadataToken;
            if (_dependencies.ContainsKey(typeMetadataToken))
            {
                if (_dependencies[typeMetadataToken].ContainsKey(dependencyName))
                {
                    implementations = _dependencies[typeMetadataToken][dependencyName];
                }
            }
            return implementations != null;
        }
    }
}
