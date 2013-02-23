// -----------------------------------------------------------------------
// <copyright file="ProcessorRegistry.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Configuration;
    using Serialization;

    /// <summary>
    /// The registry is used to retrieve/create the correct type of processor for the desired model and underlying data
    /// </summary>
    public class ProcessorRegistry
    {
        private readonly List<ProcessorMetaData> processorConfigs = new List<ProcessorMetaData>();
        private readonly List<ProcLookUp> lookups = new List<ProcLookUp>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorRegistry" /> class.
        /// </summary>
        /// <param name="configSectionName">configuration section name to load</param>
        public ProcessorRegistry(string configSectionName = null)
        {
            Init(ApiConfigManager.GetConfig(configSectionName));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorRegistry" /> class.
        /// </summary>
        /// <param name="config">configuration data to use</param>
        public ProcessorRegistry(ApiConfig config)
        {
            Init(config);
        }

        /// <summary>
        /// Looks up and creates a new KspProcessor instance for the requested type
        /// </summary>
        /// <typeparam name="T">model type to lookup</typeparam>
        /// <returns>configured <see cref="KspProcessor"/> instance</returns>
        public KspProcessor<T> Create<T>() where T : class, new()
        {
            var config = GetConfig<T>();
            if (config == null)
            {
                throw new KerbalDataException("An error has occured while loading the configuration for the procssor for type " + typeof(T).FullName + " check application configuration.");
            }

            var serializerType = Type.GetType(config.Serializer.Type);
            var converterType = Type.GetType(config.Converter.Type);

            var serializer = Activator.CreateInstance(serializerType) as IKspSerializer;

            if (serializer == null)
            {
                throw new KerbalDataException("There has been an error loading the serializer of type: " + serializerType.FullName);
            }

            var converter = Activator.CreateInstance(converterType.MakeGenericType(new Type[] { typeof(T) })) as IKspConverter<T>;

            if (converter == null)
            {
                throw new KerbalDataException("There has been an error loading the converter of type: " + converterType.FullName);
            }

            return Create<T>(serializer, converter);
        }

        /// <summary>
        /// Gets the processor configuration that is used for a particular model type
        /// </summary>
        /// <typeparam name="T">model type to lookup</typeparam>
        /// <returns>configuration used by the registry when creating the processor instance</returns>
        public ProcessorConfig GetConfig<T>()
        {
            var type = typeof(T);
            var configs = new List<ProcessorMetaData>();

            // First a quick search of the lookup cache before running through a logic search of configured processors. 
            var cached = lookups.Where(l => l.Type == type).ToList();

            if (cached.Count == 1)
            {
                return cached[0].Config;
            }

            configs = processorConfigs.Where(c => c.Model == type).ToList();

            if (configs.Count == 0)
            {
                var types = type.GetNestedTypes();

                foreach (var t in types)
                {
                    configs = processorConfigs.Where(c => c.Model == t).ToList();

                    if (configs.Count == 1)
                    {
                        lookups.Add(new ProcLookUp() { Type = type, Config = configs[0] });
                        return configs[0];
                    }
                }

                var interfaces = type.GetInterfaces();

                foreach (var i in interfaces)
                {
                    configs = processorConfigs.Where(c => c.Model == i).ToList();

                    if (configs.Count == 1)
                    {
                        lookups.Add(new ProcLookUp() { Type = type, Config = configs[0] });
                        return configs[0];
                    }
                }
            }

            // At this point the configuration is ambiguous. Lets see if we can find a default processor configuration. 
            configs = processorConfigs.Where(c => c.Model == null).ToList();

            if (configs.Count == 1)
            {
                lookups.Add(new ProcLookUp() { Type = type, Config = configs[0] });
                return configs[0];
            }

            throw new KerbalDataException("Ambiguous configuration information. Cannot find a matching processor configuration using model type " + type.FullName);
        }

        /// <summary>
        /// Creates a properly configured <seealso cref="ProcessorRegistry"/> instance 
        /// </summary>
        /// <param name="config">configuration data to use</param>
        /// <returns>registry instance</returns>
        public static ProcessorRegistry Create(ApiConfig config)
        {
            return new ProcessorRegistry(config);
        }

        /// <summary>
        /// Creates a properly configured <seealso cref="ProcessorRegistry"/> instance 
        /// </summary>
        /// <param name="configSectionName">configuration section to lookup and load</param>
        /// <returns>registry instance</returns>
        public static ProcessorRegistry Create(string configSectionName = "kerbalData")
        {
            return new ProcessorRegistry(configSectionName);
        }

        private void Init(ApiConfig config)
        {
            if (config == null)
            {
                throw new KerbalDataException("There has been an error attempting to load KerbalData configuration. Ensure that a configuration section exists with the following configuration: <section name=\"kerbalData\" type=\"KerbalData.Configuration.ApiConfig, KerbalData\"/>");
            }

            if (config.Processors.Count <= 0)
            {
                throw new KerbalDataException("No processor configurations were found in the application config. Ensure that at least one processor configuration exisits");
            }

            foreach (var proc in config.Processors)
            {
                processorConfigs.Add(new ProcessorMetaData(proc));
            }
        }

        private KspProcessor<T> Create<T>(IKspSerializer serializer, IKspConverter<T> converter) where T : class, new()
        {
            if (serializer == null || converter == null)
            {
                throw new KerbalDataException("Both a serializer and a converter instance are required to serialize/deserialize KSP data");
            }

            return new KspProcessor<T>(serializer, converter);
        }

        private class ProcLookUp
        {
            public Type Type { get; set; }
            public ProcessorConfig Config { get; set; }
        }

    }
}
