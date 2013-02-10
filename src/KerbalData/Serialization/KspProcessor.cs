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
        public KspProcessor(IKspSerializer serializer, IKspConverter<T> converter)
        {
            Serializer = serializer;
            Converter = converter;
        }

        public IKspSerializer Serializer { get; private set; }

        public IKspConverter<T> Converter { get; private set; }

        public T Process(Stream data)
        {
            var dc = new KspDataContext();
            Serializer.DeSerialize(dc, new StreamReader(data, true));

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

    /*
    /// <summary>
    /// Factory class for processor creation
    /// </summary>
    public static class KspProcessor
    {


        //private static List<ProcessorMetaData> processorConfigs = new List<ProcessorMetaData>();
        //private static List<ProcLookUp> lookups = new List<ProcLookUp>();

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

        public static KspProcessor<T> Create<T>(ProcessorConfig config) where T : class, new()
        {
            if (config == null)
            {
                throw new KerbalDataException("An error has occured while loading the configuration for the procssor named " + name + " check application configuration.");
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
                throw new KerbalDataException("There has been an error loading the converter of type: " + serializerType.FullName);
            }

            return Create<T>(serializer, converter);
        }

        public static KspProcessor<T> Create<T>(ApiConfig config, string configName = "kerbalData", bool addToManager = false) where T : class, new()
        {
            var registry = new ProcessorRegistry(config);

            if (addToManager)
            {
                registries[configName] = registry;
            }

            return Create<T>(registry.GetProcessorConfig<T>());
        }

        public static KspProcessor<T> Create<T>(string configSectionName = "kerbalData") where T : class, new()
        {
            if (!registries.ContainsKey(configSectionName))
            {
                registries[configSectionName] = new ProcessorRegistry(configSectionName);
            }

            return Create<T>(registries[configSectionName].GetProcessorConfig<T>());
        }

        public static void AddToRegistry(ApiConfig config, string configName = "kerbalData")
        {
            if (registries.ContainsKey(configName))
            {
                throw new KerbalDataException("Configuration named " + configName + " has already been registered";
            }

             registries[configName] = new ProcessorRegistry(config);
        }
    }*/
}
