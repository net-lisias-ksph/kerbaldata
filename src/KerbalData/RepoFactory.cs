// -----------------------------------------------------------------------
// <copyright file="RepoFactory.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using Configuration;

    /// <summary>
    /// Basic reposiotry placeholder
    /// </summary>
    public class RepoFactory
    {
        private List<RepoLookUp> lookups = new List<RepoLookUp>();

        private ProcessorRegistry registry;

        public RepoFactory(RepositoriesConfig config, ProcessorRegistry registry)
        {
            this.registry = registry;

            InitConfig(config);
        }

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

                if (config.Parameters != null)
                {
                    foreach (RepoParameterConfig param in config.Parameters)
                    {
                        Parameters[param.Key] = param.Value;
                    }
                }
            }

            public string Name { get; set; }
            public Type Type { get; set; }
            public IDictionary<string, object> Parameters { get; set; }

            public IKerbalDataRepo<T> GetRepo<T>(ProcessorRegistry registry, IDictionary<string, object> parameters = null) where T : class, IStorable, new()
            {
                var type = typeof(T);

                // Accept runtime proviced values and overwrite any configured values of the same name (runtime values ALWAYS take precedence)
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
                    throw new KerbalDataException("An error has occured while creating an instance of the repository of type " + Type.FullName + " See inner exception for details", ex);
                }
            }
        }
    }
}
