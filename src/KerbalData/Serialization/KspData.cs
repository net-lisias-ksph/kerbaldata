// -----------------------------------------------------------------------
// <copyright file="KspData.cs" company="OpenSauceSolutions">
// � 2013 OpenSauce Solutions
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
        public static T LoadKspFile<T>(string path, IKspSerializer serializer = null, IKspConverter<T> converter = null) where T : class, new()
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
                    var processor = KspProcessor.Create<T>(serializer, converter);
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
        public static void SaveFile<T>(string path, T obj) where T : class, new()
        {
            Convert(obj).WriteToFile(path);
        }

        /// <summary>
        /// Converts a KSP data string to JSON
        /// </summary>
        /// <param name="kspDataString">KSP string to use</param>
        /// <returns>de-serialized JObject instance</returns>
        public static T Convert<T>(string kspDataString, IKspSerializer serializer = null, IKspConverter<T> converter = null) where T : class, new()
        {
            T obj;

            try
            {
                var processor = KspProcessor.Create<T>(serializer, converter);
                obj = processor.Process(new MemoryStream(Encoding.ASCII.GetBytes(kspDataString)));
            }
            catch (Exception ex)
            {
                throw new KerbalDataException("An error has occured while attempting to load the KSP Data file. See inner exception for details.", ex);
            }

            return obj;
        }

        /// <summary>
        /// Converts an object instance to a KSP string
        /// </summary>
        /// <param name="jobj">object instance to serialize</param>
        /// <returns>serilaized KSP data string</returns>
        public static string Convert<T>(T obj, IKspSerializer serializer = null, IKspConverter<T> converter = null) where T : class, new()
        {
           // return kspToJson.ToKspData(jobj.ToString());

            //JObject jobj;
            string kspData;
            try
            {
                var processor = KspProcessor.Create<T>(serializer, converter);
                var stream = processor.Process(obj);
                kspData = (new StreamReader(stream)).ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new KerbalDataException("An error has occured while attempting to load the KSP Data file. See inner exception for details.", ex);
            }

            return kspData;
        }
    }
}
