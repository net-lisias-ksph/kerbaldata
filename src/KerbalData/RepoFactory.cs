// -----------------------------------------------------------------------
// <copyright file="RepoFactory.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Configuration;

    /// <summary>
    /// Provides lookup and object creation of <see cref="IKerbalDataRepo{T}"/> instance based on configuration
    /// </summary>
    public class RepoFactory
    {
        private List<RepoLookUp> lookups = new List<RepoLookUp>();
        private ProcessorRegistry registry;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoFactory"/> class.
        /// </summary>
        /// <param name="config">configuration data to use</param>
        /// <param name="registry">processor registry to pass to created repositories</param>
        public RepoFactory(RepositoriesConfig config, ProcessorRegistry registry)
        {
            this.registry = registry;

            InitConfig(config);
        }

        /// <summary>
        /// Creates a new <see cref="IKerbalDataRepo{T}"/> instance
        /// </summary>
        /// <typeparam name="T">model type handled by the repo</typeparam>
        /// <param name="parameters">configuration parameters required by the repository. See the documentation for the specific repository type in use for required and optional parameters</param>
        /// <param name="name">name of repository to lookup</param>
        /// <returns>properly configured <see cref="IkerbalDataRepo{T}"/> instance</returns>
        public IKerbalDataRepo<T> Create<T>(IDictionary<string, object> parameters = null, string name = null) where T : class, IStorable, new()
        {
            var lookup = lookups.Where(c => c.Name == name).FirstOrDefault();

            if (lookup == null)
            {
                throw new KerbalDataException("Unable to locate repository configuration for repository named: " + name);
            }

            return lookup.GetRepo<T>(registry, parameters);
        }
        
        private void InitConfig(RepositoriesConfig config = null)
        {
            if (config != null && config.Count > 0)
            {
                foreach (RepositoryConfig repo in config)
                {
                    lookups.Add(new RepoLookUp(repo));
                }  
            }
        }

        private class RepoLookUp
        {
            public RepoLookUp() { }

            public RepoLookUp(RepositoryConfig config)
            {
                Type = Type.GetType(config.Type);
                Name = config.Name;
                Parameters = new Dictionary<string, object>();

                if (config.Parameters == null)
                {
                    return;
                }

                foreach (RepoParameterConfig param in config.Parameters)
                {
                    Parameters[param.Key] = param.Value;
                }
            }

            internal string Name { get; set; }
            private Type Type { get; set; }
            private IDictionary<string, object> Parameters { get; set; }

            public IKerbalDataRepo<T> GetRepo<T>(ProcessorRegistry registry, IDictionary<string, object> parameters = null) where T : class, IStorable, new()
            {
                var type = typeof(T);

                // Accept runtime provided values and overwrite any configured values of the same name (runtime values ALWAYS take precedence)
                var paramList = new Dictionary<string, object>(Parameters);

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        paramList[param.Key] = param.Value;
                    }
                }
                
                try
                {
                    return Activator.CreateInstance(Type.MakeGenericType(new Type[] { type }), new object[] { registry, paramList } ) as IKerbalDataRepo<T>;
                }
                catch (Exception ex)
                {
                    throw new KerbalDataException("An error has occurred while creating an instance of the repository of type " + Type.FullName + " See inner exception for details", ex);
                }
            }
        }
    }
}
