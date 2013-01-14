// -----------------------------------------------------------------------
// <copyright file="KspToJson.cs" company="OpenSauceSolutions">
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

    /// <summary>
    /// Primarey converter class, holds core references to file and data conveter. 
    /// 1-to-1 mapping between a single file/data converter pair and a KspToJson instance. 
    /// TODO: Additional constructors to facilitate configuration injection
    /// </summary>
    public class KspToJson
    {
        private IKspFileConverter fileConverter;
        private IKspDataConverter dataConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="KspToJson" /> class.
        /// </summary>	
        public KspToJson()
        {
            fileConverter = new KspFileConverter();
            dataConverter = new KspDataConverter();
        }

        /// <summary>
        /// Converts a KSP string to a JSON string
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public string ToKspData(string json)
        {
            return dataConverter.BuildKsp(json);
        }

        /// <summary>
        /// Converts a stream containing KSP data to a JSON string
        /// </summary>
        /// <param name="stream">stream data to convert</param>
        /// <returns>JSON string</returns>
        public string ToJson(Stream stream)
        {
            var context = new KspDataContext();
            fileConverter.Parse(new StreamReader(stream), context);

            return dataConverter.BuildJson(context);
        }

        /// <summary>
        /// Converts a JSON string to a KSP string
        /// </summary>
        /// <param name="ksp">KSP data string to convert</param>
        /// <returns>JSON String</returns>
        public string ToJson(string ksp)
        {
            return ToJson(new MemoryStream(Encoding.ASCII.GetBytes(ksp)));
        }
    }
}
