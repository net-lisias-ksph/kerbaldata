// -----------------------------------------------------------------------
// <copyright file="KerbalDataManager.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Configuration;
    using Serialization;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public abstract class KerbalDataManager : IKerbalDataManager
    {
        private const string DefaultConfigSectionName = "kerbalData";

        private RepoFactory repoFactory;
        private ProcessorRegistry procRegistry;

        public KerbalDataManager() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataManager" /> class.
        /// </summary>	
        public KerbalDataManager(string location, ApiConfig configuration)
        {
            BaseUri = location;
            ApiConfig = configuration;

            Init();
        }

        public string BaseUri
        {
            get;
            protected set;
        }

        public ApiConfig ApiConfig
        {
            get;
            protected set;
        }

        public RepoFactory RepositoryFactory
        {
            get { return repoFactory; }
        }

        public ProcessorRegistry ProcRegistry
        {
            get { return procRegistry; }
        }

        public static T Create<T>(string baseUri, string configSectionName = "kerbalData") where T : class, IKerbalDataManager
        {
            return Create<T>(baseUri, ApiConfigManager.GetConfig(configSectionName));
        }

        public static T Create<T>(string baseUri, ApiConfig config) where T : class, IKerbalDataManager
        {
            var type = typeof(T);

            try
            {
                return Activator.CreateInstance(type, new object[] { baseUri, config }) as T;
            }
            catch (Exception ex)
            {
                throw new KerbalDataException("An error has occured while creating an instance of the repository of type " + type.FullName + " See inner exception for details", ex);
            }
        }

        /// <summary>
        /// Handles inital setup and population of data properties. 
        /// This is some of the "magic" that allows developers creating custom models to easily map
        /// StorableObjects instances with the correct repositories to custom properties without
        /// a ton of wireup code. 
        /// </summary>
        private void Init()
        {
            procRegistry = ProcessorRegistry.Create(ApiConfig);
            repoFactory = new RepoFactory(ApiConfig.Repositories, procRegistry);

            var managerType = this.GetType();
            var method = repoFactory.GetType().GetMethod("Create");

            foreach (var prop in managerType.GetProperties().Where(p => p.PropertyType.FullName.Contains("StorableObjects")))
            {
                var modelType = prop.PropertyType.GetGenericArguments()[0];
                var repo = 
                    method.MakeGenericMethod(new[] { modelType }).Invoke(
                        repoFactory, 
                        new object[] { new Dictionary<string, object>() { { "BaseUri", BaseUri } }, prop.Name });

                prop.SetMethod.Invoke(this, new object[] { Activator.CreateInstance(prop.PropertyType, new object[] { repo, this }) });
            }
        }
    }
}
