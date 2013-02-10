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
    /// This permanat helper class is also consumed by the KspInstallFileRepo and may be used by other
    /// file system based repositories. 
    /// </summary>
    public static class KspData
    {
        
        public static T LoadKspFile<T>(string path, string configSectionName = "kerbalData") where T : class, new()
        {
            return LoadKspFile<T>(path, ProcessorRegistry.Create(configSectionName).Create<T>());
        }

        /*
        public static T LoadKspFile<T>(string path, IKspSerializer serializer, IKspConverter<T> converter) where T : class, new()
        {
            var processor = KspProcessor.Create<T>(serializer, converter);
            return LoadKspFile<T>(path, processor);
        }*/

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
        /// Loads the JSON file found at the provided path
        /// </summary>
        /// <param name="path">relative or absloute path</param>
        /// <returns>de-serialized JObject instance</returns>
        public static T LoadJsonFile<T>(string path) where T : class, new()
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
        /// <param name="path">path to save file to</param>
        /// <param name="obj">JObject to use when serializing data</param>
        public static void SaveFile<T>(string path, T obj, string configSectionName = "kerbalData") where T : class, new()
        {
            SaveFile<T>(path, obj, ProcessorRegistry.Create(configSectionName).Create<T>());

        }

        public static void SaveFile<T>(string path, T obj, KspProcessor<T> processor) where T : class, new()
        {
            Convert(obj, processor).WriteToFile(path);
        }

        public static T Convert<T>(string data, string configSectionName = "kerbalData") where T : class, new()
        {
            return Convert<T>(data, ProcessorRegistry.Create(configSectionName).Create<T>());
        }

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

        public static string Convert<T>(T obj, string configSectionName = "kerbalData") where T : class, new()
        {
            return Convert<T>(obj, ProcessorRegistry.Create(configSectionName).Create<T>());
        }

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
