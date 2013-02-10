// -----------------------------------------------------------------------
// <copyright file="KspData.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json.Linq;

    using Serialization;

    /// <summary>
    /// Single file loading helper method class. Can be used to load/save and convert individual data items/files
    /// This permenant helper class is also consumed by the <seealso cref="FileSystemRepository{T}"/> and may be used by other
    /// file system based repositories. 
    /// </summary>
    public static class KspData
    {
        /// <summary>
        /// Loads and de-serializes a KSP data file into the requested type
        /// </summary>
        /// <typeparam name="T">model type to de-serialize to</typeparam>
        /// <param name="path">full path of file location</param>
        /// <param name="configSectionName">configuration section name to use for processor lookup</param>
        /// <returns>de-serialized object instance</returns>
        public static T LoadKspFile<T>(string path, string configSectionName = "kerbalData") where T : class, new()
        {
            return LoadKspFile<T>(path, ProcessorRegistry.Create(configSectionName).Create<T>());
        }

        /// <summary>
        /// Loads and de-serializes a KSP data files into the requested type
        /// </summary>
        /// <typeparam name="T">model type to de-serialize to</typeparam>
        /// <param name="path">full path of file location</param>
        /// <param name="processor">processor instance to use for de-serialization</param>
        /// <returns>de-serialized object instance</returns>
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
                    throw new KerbalDataException("An error has occured while attempting to load the KSP Data file. See inner exception for details. Path: " + path, ex);
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
                    throw new KerbalDataException("An error has occured while attempting to load the KSP Data file. See inner exception for details. Path: " + path, ex);
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
                    throw new KerbalDataException("An error has occured while attempting to load the KSP Data file. See inner exception for details. Path: " + path, ex);
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
        /// <param name="configSectionName">config section name to use for processor lookup</param>
        public static void SaveFile<T>(string path, T obj, string configSectionName = "kerbalData") where T : class, new()
        {
            SaveFile<T>(path, obj, ProcessorRegistry.Create(configSectionName).Create<T>());

        }

        /// <summary>
        /// Saves provided data to KSP data format at the provided path
        /// </summary>
        /// <typeparam name="T">model type to sae</typeparam>
        /// <param name="path">path to save gile to</param>
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
        public static T Convert<T>(string data, string configSectionName = "kerbalData") where T : class, new()
        {
            return Convert<T>(data, ProcessorRegistry.Create(configSectionName).Create<T>());
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
                throw new KerbalDataException("An error has occured while attempting to load the KSP Data file. See inner exception for details.", ex);
            }

            return obj;
        }

        /// <summary>
        /// Converts an object instannce to a KSP data string
        /// </summary>
        /// <typeparam name="T">model type to convert to</typeparam>
        /// <param name="obj">object data</param>
        /// <param name="configSectionName">configuration section to use for processor lookup</param>
        /// <returns>KSP data as a string</returns>
        public static string Convert<T>(T obj, string configSectionName = "kerbalData") where T : class, new()
        {
            return Convert<T>(obj, ProcessorRegistry.Create(configSectionName).Create<T>());
        }

        /// <summary>
        /// Converts an object instannce to a KSP data string
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
                throw new KerbalDataException("An error has occured while attempting to load or read the KSP Data file. See inner exception for details.", ex);
            }

            return kspData;
        }
    }
}
