using System;
using System.Linq;
using System.Collections.Generic;

namespace DependencyInjectionContainerLib
{
    public class DependenciesConfiguration
    {
        private Dictionary<int, List<Type>> _dependencies;

        public DependenciesConfiguration()
        {
            _dependencies = new Dictionary<int, List<Type>>();
        }

        public void Register<T, F>()
        {
            Register(typeof(T), typeof(F));
        }

        public void Register(Type dependenciType, Type implementationType)
        {
            int metadataToken = dependenciType.MetadataToken;
            if (_dependencies.ContainsKey(metadataToken))
            {
                if (!_dependencies[metadataToken].Where(type => type.MetadataToken == implementationType.MetadataToken).Any())
                {
                    _dependencies[metadataToken].Add(implementationType);
                }
            }
            else
            {
                _dependencies.Add(metadataToken, new List<Type>() { implementationType });
            }
        }

        public bool TryGetValue(Type type, out List<Type> implementations)
        {
            return _dependencies.TryGetValue(type.MetadataToken, out implementations);
        }
    }
}
