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

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataManager" /> class.
        /// </summary>
        /// <remarks>
        /// This constructor should never be used. It exists to support use of generics elsewhere in the code. TODO: This should be fixed. 
        /// </remarks>
        public KerbalDataManager() 
        {
            throw new KerbalDataException("The kerbal data manager should never be constructed with it's default constructor. This is implmented only to satisfy generic requirments in the code base. TODO: this should be fixed");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataManager" /> class.
        /// </summary>	
        /// <param name="location">base location/path of managed data</param>
        /// <param name="configuration">configuration to use</param>
        public KerbalDataManager(string location, ApiConfig configuration)
        {
            BaseUri = location;
            ApiConfig = configuration;

            Init();
        }

        /// <summary>
        /// Gets the root Uri of the data 
        /// </summary>
        public string BaseUri
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the configuration of the manager instance
        /// </summary>
        public ApiConfig ApiConfig
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the configured factory used by the manager instnace
        /// </summary>
        public RepoFactory RepositoryFactory
        {
            get { return repoFactory; }
        }

        /// <summary>
        /// Gets the configured processor registry used by the manager instance
        /// </summary>
        public ProcessorRegistry ProcRegistry
        {
            get { return procRegistry; }
        }

        /// <summary>
        /// Creates a properly conigured instance of the <see cref="KerbalDataManager"/> class
        /// </summary>
        /// <typeparam name="T">model type handled by the manager</typeparam>
        /// <param name="baseUri">root Uri for the manager</param>
        /// <param name="configSectionName">configuratiion section to use</param>
        /// <returns>configured instance</returns>
        public static T Create<T>(string baseUri, string configSectionName = "kerbalData") where T : class, IKerbalDataManager
        {
            return Create<T>(baseUri, ApiConfigManager.GetConfig(configSectionName));
        }

        /// <summary>
        /// Creates a properly conigured instance of the <see cref="KerbalDataManager"/> class
        /// </summary>
        /// <typeparam name="T">model type handled by the manager</typeparam>
        /// <param name="baseUri">root Uri for the manager</param>
        /// <param name="config">configuration data to use</param>
        /// <returns>configured instance</returns>
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
