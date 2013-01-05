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
    /// TODO: Class Summary
    /// </summary>
    public static class KspData
    {
        private static KspToJson kspToJson = new KspToJson();

        /// <summary>
        /// Initializes a new instance of the <see cref="KspData" /> class.
        /// </summary>	
        static KspData()
        {
        }

        public static JObject LoadFile(string path)
        {
           path = Environment.CurrentDirectory + path;

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

        public static string Convert(JObject jobj)
        {
            return kspToJson.ToKspData(jobj.ToString());
        }
    }
}
