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
    /// TODO: Class Summary
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

        public string ToKspData(string json)
        {
            return dataConverter.BuildKsp(json);
        }

        public string ToJson(Stream stream)
        {
            var context = new KspDataContext();
            fileConverter.Parse(new StreamReader(stream), context);

            return dataConverter.BuildJson(context);
        }

        public string ToJson(string ksp)
        {
            return ToJson(new MemoryStream(Encoding.ASCII.GetBytes(ksp)));
        }


    }
}
