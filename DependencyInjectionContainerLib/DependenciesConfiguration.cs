using System;
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
            int metadataToken = typeof(T).MetadataToken;
            if (_dependencies.ContainsKey(metadataToken))
            {
                if (!_dependencies[metadataToken].Contains(typeof(F)))
                {
                    _dependencies[metadataToken].Add(typeof(F));
                }
            }
            else
            {
                _dependencies.Add(metadataToken, new List<Type>() { typeof(F) });
            }
        }

        public Dictionary<int, List<Type>> GetDependencies()
        {
            return _dependencies;
        }
    }
}
