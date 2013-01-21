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
            const string fileConverterName = "KerbalData.Serializers.V018x.KspFileConverter, KerbalData.Serializers";
            const string dataConverterName = "KerbalData.Serializers.V018x.KspDataConverter, KerbalData.Serializers";
            // TODO: Configuration and loading framework for Converters. Doing it this way for the time being so I can start
            // Pulling out JSON.NET dependencies on the core framework and to prevent adding new dependcies just for data converson
            // needs
            fileConverter = Activator.CreateInstance(Type.GetType(fileConverterName)) as IKspFileConverter;
            dataConverter = Activator.CreateInstance(Type.GetType(dataConverterName)) as IKspDataConverter;
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
