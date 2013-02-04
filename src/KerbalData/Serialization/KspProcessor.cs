// -----------------------------------------------------------------------
// <copyright file="KspProcessor.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Configuration;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public class KspProcessor<T> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KspProcessor" /> class.
        /// </summary>	
        internal KspProcessor(IKspSerializer serializer, IKspConverter<T> converter)
        {
            Serializer = serializer;
            Converter = converter;
        }

        public IKspSerializer Serializer { get; private set; }

        public IKspConverter<T> Converter { get; private set; }

        public T Process(Stream data)
        {
            var dc = new KspDataContext();
            Serializer.DeSerialize(dc, new StreamReader(data));

            var obj = Converter.Convert(dc);
            return obj;
        }

        public Stream Process(T obj)
        {
            var dc = Converter.Convert(obj);

            var stream = new MemoryStream();
            var sw = new StreamWriter(stream);
            Serializer.Serialize(dc, sw);
            sw.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
            
        }
    }

    /// <summary>
    /// Factory class for processor creation
    /// </summary>
    public static class KspProcessor
    {
        private static ApiConfig config;
        private static List<ProcessorMetaData> processorConfigs = new List<ProcessorMetaData>();
        private static List<ProcLookUp> lookups = new List<ProcLookUp>();

        /// <summary>
        /// Creates a new instance of <see cref="KspProcessor{T}"/> based upon context and configuration. 
        /// </summary>
        /// <typeparam name="T">object type for processor to serialize to/from</typeparam>
        /// <param name="serializer">serializer instance to use, if none provided the factory will inject one</param>
        /// <param name="converter">converter instance to use, if non provided the factory will inject one</param>
        /// <returns>properly configured KspProcessor instance for use in serializing/deserailizing objects of the defined type</returns>
        public static KspProcessor<T> Create<T>(IKspSerializer serializer, IKspConverter<T> converter) where T : class, new()
        {
            if (serializer == null || converter == null)
            {
                throw new KerbalDataException("Both a serializer and a converter instance are required to serialize/deserialize KSP data");
            }

            return new KspProcessor<T>(serializer, converter);
        }

        public static KspProcessor<T> Create<T>(string name = null) where T : class, new()
        {
            InitConfig();

            var processorConfig = GetConfig<T>(name);

            if (processorConfig == null)
            {
                throw new KerbalDataException("An error has occured while loading the configuration for the procssor named " + name + " check application configuration.");
            }

            var serializerType = Type.GetType(processorConfig.Serializer.Type);
            var converterType = Type.GetType(processorConfig.Converter.Type);

            var serializer = Activator.CreateInstance(serializerType) as IKspSerializer;

            if (serializer == null)
            {
                throw new KerbalDataException("There has been an error loading the serializer of type: " + serializerType.FullName);
            }

            var converter = Activator.CreateInstance(converterType.MakeGenericType(new Type[] { typeof(T) })) as IKspConverter<T>;

            if (converter == null)
            {
                throw new KerbalDataException("There has been an error loading the converter of type: " + serializerType.FullName);
            }

            return Create<T>(serializer, converter);
        }

        private static void InitConfig()
        {
            if (config == null || processorConfigs.Count <= 0)
            {
                config = ConfigurationManager.GetSection("kerbalData") as ApiConfig;

                if (config == null)
                {
                    throw new KerbalDataException("There has been an error attempting to load KerbalData configuration. Ensure that a config section exists with the following config: <section name=\"kerbalData\" type=\"KerbalData.Configuration.ApiConfig, KerbalData\"/>");     
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
        }

        private static ProcessorConfig GetConfig<T>(string name = null)
        {
            var type = typeof(T);
            List<ProcessorMetaData> configs = new List<ProcessorMetaData>();

            // First a quick search of the lookup cache before running through a logic search of configured processors. 
            var cached = lookups.Where(l => l.Name == name && l.Type == type).ToList();

            if (cached.Count == 1)
            {
                return cached[0].Config;
            }

            // If no cached lookup is found, run through the lookup process on config information
            // First we look for a named configuration
            if (name != null)
            {
                configs = processorConfigs.Where(c => c.Name == name).ToList();

                // If there are no results we have a bad config
                if (configs.Count() == 0)
                {
                    throw new KerbalDataException("The processor configuration named " + name + " cannot be located, check application config");
                }
                else if (configs.Count() == 1)
                {
                    // If we find a single config this is the one we want
                    lookups.Add(new ProcLookUp() { Name = name, Type = type, Config = configs[0] });
                    return configs[0];
                }
            }

            // Now search by model
            // If the configs have no valid values we populate any matching models.
            if (configs.Count() == 0)
            {
                configs = processorConfigs.Where(c => string.IsNullOrEmpty(c.Name) && c.Model == type).ToList();

                if (configs.Count == 0)
                {
                    var types = type.GetNestedTypes();

                    foreach (var t in types)
                    {
                        configs = processorConfigs.Where(c => string.IsNullOrEmpty(c.Name) && c.Model == t).ToList();

                        if (configs.Count == 1)
                        {
                            lookups.Add(new ProcLookUp() { Type = type, Config = configs[0] });
                            return configs[0];
                        }
                    }

                    var interfaces = type.GetInterfaces();

                    foreach (var i in interfaces)
                    {
                        configs = processorConfigs.Where(c => string.IsNullOrEmpty(c.Name) && c.Model == i).ToList();

                        if (configs.Count == 1)
                        {
                            lookups.Add(new ProcLookUp() { Type = type, Config = configs[0] });
                            return configs[0];
                        }
                    }
                }
            }
            else
            {
                // If we have some configs we want to filter the matching set. 
                var namedConfigs = configs.Where(c => c.Model == type).ToList();

                if (namedConfigs.Count == 0)
                {
                    var types = type.GetNestedTypes();

                    foreach (var t in types)
                    {
                        namedConfigs = configs.Where(c => c.Model == t).ToList();

                        if (namedConfigs.Count == 1)
                        {
                            lookups.Add(new ProcLookUp() { Name = name, Type = type, Config = namedConfigs[0] });
                            return namedConfigs[0];
                        }
                    }

                    var interfaces = type.GetInterfaces();

                    foreach (var i in interfaces)
                    {
                        namedConfigs = configs.Where(c => c.Model == i).ToList();

                        if (namedConfigs.Count == 1)
                        {
                            lookups.Add(new ProcLookUp() { Name = name, Type = type, Config = namedConfigs[0] });
                            return namedConfigs[0];
                        }
                    }
                }

                configs = namedConfigs;
            }

            if (configs.Count == 1)
            {
                lookups.Add(new ProcLookUp() { Name = name, Type = type, Config = configs[0] });
                return configs[0];
            }

            // At this point the configuration is ambigious. Lets see if we can find a default processor config. 
            configs = processorConfigs.Where(c => string.IsNullOrEmpty(c.Name) && c.Model == null).ToList();

            if (configs.Count == 1)
            {
                lookups.Add(new ProcLookUp() { Name = name, Type = type, Config = configs[0] });
                return configs[0];
            }

            throw new KerbalDataException("Ambigious configration information. Cannot find a matching processor config of " + name == null ? "NULL" : name + " and using model type " + type.FullName);
        }

        private class ProcLookUp
        {
            public string Name { get; set; }
            public Type Type { get; set; }
            public ProcessorConfig Config { get; set; }
        }
    }
}
