// -----------------------------------------------------------------------
// <copyright file="KspData.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.IO;
    using System.Text;

    using Newtonsoft.Json.Linq;

    using Configuration;
    using Providers;
    using Serialization;

    /// <summary>
    /// Single file loading helper method class. Can be used to load/save and convert individual data items/files
    /// <remarks>
    /// <para>
    /// This permanent helper class is also consumed by the <seealso cref="FileSystemRepository{T}"/> and may be used by other file system based repositories. 
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>Multiple methods in this class have parameters for either a configuration section name to automatically configure a <see cref="ProcessorRegistry"/> for use in serialization. The other accepts an instance of <see cref="KspProcessor{T}"/> to use for serialization</para>
    /// <code language="xml" region="en-us" title="Example Configuration">
    /// <![CDATA[
    /// <configuration>
    ///   <configSections>
    ///     <section name="kerbalData" type="KerbalData.Configuration.ApiConfig, KerbalData"/>
    ///   </configSections>
    ///   <kerbalData>
    ///     <processors>
    ///       <processor index="0">
    ///         <serializer type="KerbalData.Serialization.Serializers.V018x.DataSerializer, KerbalData"/>
    ///         <converter type="KerbalData.Serialization.Serializers.V018x.JsonModelConverter`1, KerbalData"/>
    ///       </processor>
    ///       <processor index="1" modelType="Newtonsoft.Json.Linq.JObject, Newtonsoft.Json">
    ///         <serializer type="KerbalData.Serialization.Serializers.V018x.DataSerializer, KerbalData"/>
    ///         <converter type="KerbalData.Serialization.Serializers.V018x.JsonObjectConverter`1, KerbalData"/>
    ///       </processor>      
    ///     </processors>
    ///   </kerbalData>
    /// </configuration>]]>
    /// </code>
    /// </example>
    /// </summary>
    public static class KspData
    {
        private static readonly ProcessorRegistry DefaultRegistry;

        private static readonly ApiConfig DefaultConfig = new ApiConfig()
            {
                Processors = new ProcessorsConfig()
                    {
                        new ProcessorConfig()
                            {
                                Index = 0,
                                Serializer = new SerializerConfig()
                                    {
                                        Type =
                                            "KerbalData.Serialization.Serializers.V018x.DataSerializer, KerbalData"
                                    },
                                Converter = new ConverterConfig()
                                    {
                                        Type =
                                            "KerbalData.Serialization.Serializers.V018x.JsonModelConverter`1, KerbalData"
                                    }
                            },
                        new ProcessorConfig()
                            {
                                Index = 1,
                                ModelType = "Newtonsoft.Json.Linq.JObject, Newtonsoft.Json",
                                Serializer = new SerializerConfig()
                                    {
                                        Type =
                                            "KerbalData.Serialization.Serializers.V018x.DataSerializer, KerbalData"
                                    },
                                Converter = new ConverterConfig()
                                    {
                                        Type =
                                            "KerbalData.Serialization.Serializers.V018x.JsonObjectConverter`1, KerbalData"
                                    }
                            }
                    }
            };

        static KspData()
        {
            DefaultRegistry = ProcessorRegistry.Create(DefaultConfig);

        }

        /// <summary>
        /// Loads and de-serializes a KSP data file into the requested type
        /// </summary>
        /// <typeparam name="T">model type to de-serialize to</typeparam>
        /// <param name="path">full path of file location</param>
        /// <param name="configSectionName">configuration section name to use for processor lookup</param>
        /// <returns>de-serialized object instance</returns>
        /// <example>
        /// <para>
        /// A null configuration causes the class to load embedded defaults. This is good for most common uses and is the recommended way for loading individual files. A type parameter must be supplied to the method so the correct type can be serialized. KerbalData is not yet able to determine the type from the data loaded. The developer must provide this information.
        /// </para>
        /// <code language="c#" region="en-us" title="Using Section Name Overload DEFAULT">
        /// var data = KspData.LoadKspFile&lt;SaveFile&gt;(@"C:\my\file\location\filename.ext");
        /// </code>
        /// <para>
        /// If a configuration section name is provided, KspData will load the named .NET configuration and use it's data in creating the <see cref="ProcessorRegistry"/> used in serialization. The following example is the same configuration that is used by default. 
        /// </para>
        /// <code language="c#" region="en-us" title="Using Section Name Overload w/ Custom Section">
        /// var data = KspData.LoadKspFile&lt;SaveFile&gt;@"C:\my\file\location\filename.ext", "kerbalData");
        /// </code>
        /// </example>
        public static T LoadKspFile<T>(string path, string configSectionName = null) where T : class, new()
        {
            return !string.IsNullOrEmpty(configSectionName)
                       ? LoadKspFile<T>(path, ProcessorRegistry.Create(configSectionName).Create<T>())
                       : LoadKspFile<T>(path, DefaultRegistry.Create<T>());
        }

        /// <summary>
        /// Loads and de-serializes a KSP data files into the requested type
        /// </summary>
        /// <typeparam name="T">model type to de-serialize to</typeparam>
        /// <param name="path">full path of file location</param>
        /// <param name="processor">processor instance to use for de-serialization</param>
        /// <returns>de-serialized object instance</returns>
        /// <example>
        /// <para>To load a file a path and a <see cref="KspProcessor{T}"/> instance are required. A <see cref="KspProcessor{T}"/> instance can be acquired from the <see cref="ProcessorRegistry"/> class.</para>
        /// <code language="c#" region="en-us" title="Using Section Name Overload w/ Custom Section">
        /// // Creating a registry using the config named "kerbalData" see the documentation on the ProcessorRegistry for more details on the Create(string) and Create(ApiConfig) methods.
        /// var registry = ProcessorRegistry.Create("kerbalData");
        /// 
        /// // When the file is loaded the Create method is called on the registry instance and the desired serialization type is provided to the registry to acquire the right instance. 
        /// var data = KspData.LoadKspFile&lt;SaveFile&gt;(@"C:\my\file\location\filename.sfs", registry.Create&lt;SaveFile&gt;());
        /// </code>
        /// </example>
        public static T LoadKspFile<T>(string path, KspProcessor<T> processor) where T : class, new()
        {
            if (!File.Exists(path))
            {
                throw new KerbalDataException("The KSP Data file cannot be found. Path: " + path);
            }

            T obj;

            using (var file = File.Open(path, FileMode.Open))
            {
                try
                {
                    obj = processor.Process(file);
                }
                catch (Exception ex)
                {
                    throw new KerbalDataException("An error has occurred while attempting to load the KSP Data file. See inner exception for details. Path: " + path, ex);
                }

                file.Close();
            }

            return obj;
        }

        /// <summary>
        /// Loads a JSON formatting file into JObject
        /// </summary>
        /// <param name="path">full path of file location</param>
        /// <returns>Json.NET JObject instance</returns>
        public static JObject LoadJsonFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new KerbalDataException("The KSP Data file cannot be found. Path: " + path);
            }

            JObject obj;

            using (var file = File.Open(path, FileMode.Open))
            {
                try
                {
                    obj = JObject.Parse((new StreamReader(file)).ReadToEnd());
                }
                catch (Exception ex)
           
                {
                    throw new KerbalDataException("An error has occurred while attempting to load the KSP Data file. See inner exception for details. Path: " + path, ex);
                }

                file.Close();
            }

            return obj;
        }

        /// <summary>
        /// Loads a JSON formatting file into requested type
        /// </summary>
        /// <typeparam name="T">model to map to</typeparam>
        /// <param name="path">full path of file location</param>
        /// <returns>object instance</returns>
        public static T LoadJsonFile<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new KerbalDataException("The KSP Data file cannot be found. Path: " + path);
            }

            T obj;

            using (var file = File.Open(path, FileMode.Open))
            {
                try
                {
                    obj = JObject.Parse((new StreamReader(file)).ReadToEnd()).ToObject<T>();
                }
                catch (Exception ex)
                {
                    throw new KerbalDataException("An error has occurred while attempting to load the KSP Data file. See inner exception for details. Path: " + path, ex);
                }

                file.Close();
            }

            return obj;
        }

        /// <summary>
        /// Saves provided data to KSP data format at the provided path
        /// </summary>
        /// <typeparam name="T">model type to save</typeparam>
        /// <param name="path">path to save file to</param>
        /// <param name="obj">object to use when serializing data</param>
        /// <param name="configSectionName">configuration section name to use for processor lookup</param>
        public static void SaveFile<T>(string path, T obj, string configSectionName = null) where T : class, new()
        {
            SaveFile<T>(path, obj,
                        !string.IsNullOrEmpty(configSectionName)
                            ? ProcessorRegistry.Create(configSectionName).Create<T>()
                            : DefaultRegistry.Create<T>());
        }

        /// <summary>
        /// Saves provided data to KSP data format at the provided path
        /// </summary>
        /// <typeparam name="T">model type to save</typeparam>
        /// <param name="path">path to save file to</param>
        /// <param name="obj">object data to use</param>
        /// <param name="processor">processor to use for serialization</param>
        public static void SaveFile<T>(string path, T obj, KspProcessor<T> processor) where T : class, new()
        {
            Convert(obj, processor).WriteToFile(path);
        }

        /// <summary>
        /// Converts and KSP formatting string into a de-serialized object
        /// </summary>
        /// <typeparam name="T">model type to convert to</typeparam>
        /// <param name="data">data to parse</param>
        /// <param name="configSectionName">configuration section to use for processor lookup</param>
        /// <returns>de-serialized object instance</returns>
        public static T Convert<T>(string data, string configSectionName = null) where T : class, new()
        {
            return Convert<T>(data,
                        !string.IsNullOrEmpty(configSectionName)
                            ? ProcessorRegistry.Create(configSectionName).Create<T>()
                            : DefaultRegistry.Create<T>());
        }

        /// <summary>
        /// Converts and KSP formatting string into a de-serialized object
        /// </summary>
        /// <typeparam name="T">model type to convert to</typeparam>
        /// <param name="data">data to parse</param>
        /// <param name="processor">processor to use for de-serialization</param>
        /// <returns>de-serialized object instance</returns>
        public static T Convert<T>(string data, KspProcessor<T> processor) where T : class, new()
        {
            T obj;

            try
            {
                obj = processor.Process(new MemoryStream(Encoding.UTF8.GetBytes(data)));
            }
            catch (Exception ex)
            {
                throw new KerbalDataException("An error has occurred while attempting to load the KSP Data file. See inner exception for details.", ex);
            }

            return obj;
        }

        /// <summary>
        /// Converts an object instance to a KSP data string
        /// </summary>
        /// <typeparam name="T">model type to convert to</typeparam>
        /// <param name="obj">object data</param>
        /// <param name="configSectionName">configuration section to use for processor lookup</param>
        /// <returns>KSP data as a string</returns>
        public static string Convert<T>(T obj, string configSectionName = null) where T : class, new()
        {
            return Convert<T>(obj,
                        !string.IsNullOrEmpty(configSectionName)
                            ? ProcessorRegistry.Create(configSectionName).Create<T>()
                            : DefaultRegistry.Create<T>());
        }

        /// <summary>
        /// Converts an object instance to a KSP data string
        /// </summary>
        /// <typeparam name="T">model type to convert to</typeparam>
        /// <param name="obj">object data</param>
        /// <param name="processor">processor to use for de-serialization</param>
        /// <returns>KSP data as a string</returns>
        public static string Convert<T>(T obj, KspProcessor<T> processor) where T : class, new()
        {
            string kspData;
            try
            {
                var stream = processor.Process(obj);
                kspData = (new StreamReader(stream)).ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new KerbalDataException("An error has occurred while attempting to load or read the KSP Data file. See inner exception for details.", ex);
            }

            return kspData;
        }
    }
}
