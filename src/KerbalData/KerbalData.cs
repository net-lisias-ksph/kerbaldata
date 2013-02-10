// -----------------------------------------------------------------------
// <copyright file="KerbalData.cs" company="OpenSauceSolutions">
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

    using Models;

    /// <summary>
    /// Top level consumer API class used for accessing and loading KSP data. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// Classes starting with the word "Kerbal" are "top level" consumer API's. For most development, this is the easiest way to use KerbalData. The top level API provides the following features:
    /// <list type="bullet">
    ///   <item>KSP Install Aware</item>
    ///   <item>Specialized Models with extended properties to translate KSP data to standard formats (TimeSpan for game and mission time for example)</item>
    ///   <item>Utilizes configured repos allowing for easy integration with multiple data stores</item>
    ///   <item>Provides lazy loading of data</item>
    ///   <item>Maintains data used to initialize the object and state can be restored to time of load or last save</item>
    ///   <item>All operations with the top level API start with a KerbalData instance. This class will maintain all the references necessary. The constructor accepts the root path for a KSP install. Once initialized KerbalData will scan the KSP install and load initial meta-data (currently only names) but not the actual files into memory. KSP files are only serilized and loaded on initial access of pariticular named data. Once loaded the data will be maintained and no additional calls will be made to the data until Save() is called.</item>
    /// </list>      
    /// </para>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public class KerbalData : KerbalDataManager
    {
        private const string defaultRepoType = "KerbalData.Providers.FileSystemRepository`1, KerbalData";

        private readonly string installPath;
        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalData" /> class.
        /// </summary>
        /// <param name="installPath">path of a valid KSP install currently only tested support of 0.18.x</param>
        public KerbalData(string installPath, ApiConfig configuration)
            : base(installPath.EndsWith("\\") ? installPath : installPath + "\\", configuration)
        { }

        /// <summary>
        /// Gets the saves stored in this installation
        /// </summary>
        public StorableObjects<SaveFile> Saves
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the scenarios stored in this installation
        /// </summary>
        public StorableObjects<SaveFile> Scenarios
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the training scenarios stored in this installation
        /// </summary>
        public StorableObjects<SaveFile> TrainingScenarios
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the parts stored in this installation
        /// </summary>
        public StorableObjects<PartFile> Parts
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the VAB craft stored in this installation
        /// </summary>
        public StorableObjects<CraftFile> CraftInVab
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the SPH craft stored in this installation
        /// </summary>
        public StorableObjects<CraftFile> CraftInSph
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the settings and configuration files for this installation
        /// </summary>
        public StorableObjects<ConfigFile> KspSettings
        {
            get;
            protected set;
        }

        /// <summary>
        /// Allows easy creation of KerbalData model instance with configuration. 
        /// </summary>
        /// <remarks>
        /// This method has a hard wired default configuration should no configuration be available. 
        /// The types used in the hard-wired configuration are static and will always be part of the KerbalData API
        /// and mainly represent the inital serializer and converters created for the API and serve as a starting point
        /// for all future configuratiions. 
        /// 
        /// TODO: Add sample .NET config to show current default config
        /// 
        /// If another configuration section name is provided it will be used. 
        /// 
        /// This is the reccomeded way to qucickly start using the API with minimal configuration/setup.
        /// </remarks>
        /// <example>
        /// <code language="cs" title="Starting Kerbal Data API">
        ///  var kd = KerbalData.Create(@"C:\KSP");
        /// </code>
        /// </example>
        /// <param name="installPath">root path of KSP installation</param>
        /// <param name="configSectionName">application confiiguration section name</param>
        /// <returns>properly cofigured KerbalData instance</returns>
        public static KerbalData Create(string installPath, string configSectionName = "kerbalData")
        {
            var config = ApiConfigManager.GetConfig(configSectionName);

            if (config == null)
            {
                config = new ApiConfig()
                {
                    Processors = new ProcessorsConfig()
                {
                    { new ProcessorConfig()
                        {
                            Index = 0,
                            Serializer = new SerializerConfig()
                            {
                                Type = "KerbalData.Serialization.Serializers.V018x.DataSerializer, KerbalData"
                            },
                            Converter = new ConverterConfig()
                            {
                                Type = "KerbalData.Serialization.Serializers.V018x.JsonModelConverter`1, KerbalData"
                            }
                        } },
                    { new ProcessorConfig()
                        {
                            Index = 1,
                            ModelType = "Newtonsoft.Json.Linq.JObject, Newtonsoft.Json",
                            Serializer = new SerializerConfig()
                            {
                                Type = "KerbalData.Serialization.Serializers.V018x.DataSerializer, KerbalData"
                            },
                            Converter = new ConverterConfig()
                            {
                                Type = "KerbalData.Serialization.Serializers.V018x.JsonObjectConverter`1, KerbalData"
                            }
                        } }
                },
                    Repositories = new RepositoriesConfig()
                {
                    { new RepositoryConfig()
                        {
                            Index = 0,
                            Type = "KerbalData.Providers.FileSystemRepository`1, KerbalData",
                            Name = "Saves",
                            Parameters = new RepoParametersConfig()
                            {
                                { new RepoParameterConfig() { Key ="Include", Value = @"Saves\**\persistent.sfs" } },
                                { new RepoParameterConfig() { Key ="FileName", Value = "persistent.sfs" } },
                                { new RepoParameterConfig() { Key ="FileMode", Value = "DirPerFile" } },
                            }
                        } },
                    { new RepositoryConfig()
                        {
                            Index = 1,
                            Type = "KerbalData.Providers.FileSystemRepository`1, KerbalData",
                            Name = "Scenarios",
                            Parameters = new RepoParametersConfig()
                            {
                                { new RepoParameterConfig() { Key ="Include", Value = @"Saves\scenarios\*.sfs" } },
                                { new RepoParameterConfig() { Key ="FileName", Value = ".sfs" } },
                                { new RepoParameterConfig() { Key ="FileMode", Value = "Flat" } },
                            }
                        } },
                    { new RepositoryConfig()
                        {
                            Index = 2,
                            Type = "KerbalData.Providers.FileSystemRepository`1, KerbalData",
                            Name = "TrainingScenarios",
                            Parameters = new RepoParametersConfig()
                            {
                                { new RepoParameterConfig() { Key ="Include", Value = @"Saves\training\*.sfs" } },
                                { new RepoParameterConfig() { Key ="FileName", Value = ".sfs" } },
                                { new RepoParameterConfig() { Key ="FileMode", Value = "Flat" } },
                            }
                        } },
                    { new RepositoryConfig()
                        {
                            Index = 3,
                            Type = "KerbalData.Providers.FileSystemRepository`1, KerbalData",
                            Name = "Parts",
                            Parameters = new RepoParametersConfig()
                            {
                                { new RepoParameterConfig() { Key ="Include", Value = @"Parts\**\part.cfg" } },
                                { new RepoParameterConfig() { Key ="FileName", Value = "part.cfg" } },
                                { new RepoParameterConfig() { Key ="FileMode", Value = "DirPerFile" } },
                            }
                        } },
                    { new RepositoryConfig()
                        {
                            Index = 4,
                            Type = "KerbalData.Providers.FileSystemRepository`1, KerbalData",
                            Name = "CraftInVab",
                            Parameters = new RepoParametersConfig()
                            {
                                { new RepoParameterConfig() { Key ="Include", Value = @"Ships\VAB\**\*.craft" } },
                                { new RepoParameterConfig() { Key ="FileName", Value = ".craft" } },
                                { new RepoParameterConfig() { Key ="FileMode", Value = "Flat" } },
                            }
                        } },
                    { new RepositoryConfig()
                        {
                            Index = 5,
                            Type = "KerbalData.Providers.FileSystemRepository`1, KerbalData",
                            Name = "CraftInSph",
                            Parameters = new RepoParametersConfig()
                            {
                                { new RepoParameterConfig() { Key ="Include", Value = @"Ships\SPH\**\*.craft" } },
                                { new RepoParameterConfig() { Key ="FileName", Value = ".craft" } },
                                { new RepoParameterConfig() { Key ="FileMode", Value = "Flat" } },
                            }
                        } },
                    { new RepositoryConfig()
                        {
                            Index = 6,
                            Type = "KerbalData.Providers.FileSystemRepository`1, KerbalData",
                            Name = "KspSettings",
                            Parameters = new RepoParametersConfig()
                            {
                                { new RepoParameterConfig() { Key ="Include", Value = @"*.cfg" } },
                                { new RepoParameterConfig() { Key ="FileName", Value = ".cfg" } },
                                { new RepoParameterConfig() { Key ="FileMode", Value = "Flat" } },
                            }
                        } }
                    }
                };
            }

            return Create(installPath, config);
        }

        /// <summary>
        /// Creates a KerbalData instance based upon the provided configuration. 
        /// </summary>
        /// <remarks>
        /// Unlike the Create() method that accepts a configuration name. This version of the method will not
        /// use a default configuration if the configuration property is null or not avaialble. This method allows
        /// developers to programmatily build or source configuration data from alternate stores.
        /// </remarks>
        /// <param name="installPath">KSP game installation root path</param>
        /// <param name="configuration">configuration to use</param>
        /// <returns>properly configured KerbalData instance.</returns>
        public static KerbalData Create(string installPath, ApiConfig configuration)
        {
            return KerbalDataManager.Create<KerbalData>(installPath, configuration);
        }
    }
}
