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

    /// <summary>
    /// Top level data conversion/access class. 
    /// This core class is used by higher level clasess to load and convert data as needed. KspData and the underlying converters are 
    /// netural to context and only deal with data format conversion. Any vaild JSON or KSP data file can be converted even if the fields 
    /// in the data do not match a particular game or even are used at all. Note that when adding data and fields not supported by KSP you 
    /// may have unpredictable behavior up to and including deletion of your save file on attempted load.
    /// </summary>
    public static class KspData
    {
        /// <summary>
        /// Internal converter instance 
        /// </summary>
        private static KspToJson kspToJson = new KspToJson(); 

        /// <summary>
        /// Initializes a new instance of the <see cref="KspData" /> class.
        /// </summary>	
        static KspData()
        {
        }

        /// <summary>
        /// Loads a the KSP data file found at the provided path.
        /// </summary>
        /// <param name="path">path to KSP data file</param>
        /// <returns>de-serialized JSON object containing KSP data</returns>
        public static JObject LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new KerbalDataException("The KSP Data file cannot be found. Path: " + path);
            }

            var file = File.Open(path, FileMode.Open);

            JObject jobj;

            try
            {
                jobj = JObject.Parse(kspToJson.ToJson(file));
            }
            catch (Exception ex)
            {
                throw new KerbalDataException("An error has occured while attempting to load the KSP Data file. See inner exception for details. Path: " + path, ex);
            }

            return jobj;
        }

        /// <summary>
        /// Saves provided data to KSP data format at the provided path
        /// </summary>
        /// <param name="path">path to save file to</param>
        /// <param name="obj">JObject to use when serializing data</param>
        public static void SaveFile(string path, JToken obj)
        {
            Convert(obj).WriteToFile(path);
        }

        /// <summary>
        /// Converts a KSP data string to JSON
        /// </summary>
        /// <param name="kspDataString">KSP string to use</param>
        /// <returns>de-serialized JObject instance</returns>
        public static JObject Convert(string kspDataString)
        {
            JObject jobj;

            try
            {
                jobj = JObject.Parse(kspToJson.ToJson(kspDataString));
            }
            catch (Exception ex)
            {
                throw new KerbalDataException("An error has occured while attempting to load the KSP Data file. See inner exception for details.", ex);
            }

            return jobj;
        }

        /// <summary>
        /// Converts a JSON object to a KSP string
        /// </summary>
        /// <param name="jobj">object instance to serialize</param>
        /// <returns>serilaized KSP data string</returns>
        public static string Convert(JToken jobj)
        {
            return kspToJson.ToKspData(jobj.ToString());
        }
    }
}
