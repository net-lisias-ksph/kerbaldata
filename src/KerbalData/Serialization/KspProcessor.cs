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
    /// The Processor is responsible for managing the serialization/de-serialization process for object data.
    /// </summary>
    /// <typeparam name="T">model type to process</typeparam>
    public class KspProcessor<T> where T : class, new()
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="KspProcessor{T}" /> class.
        /// </summary>	
        /// <param name="serializer">serializer instance to use</param>
        /// <param name="converter">object converter to use</param>
        public KspProcessor(IKspSerializer serializer, IKspConverter<T> converter)
        {
            Serializer = serializer;
            Converter = converter;
        }

        /// <summary>
        /// Gets the serializer instance used for processing
        /// </summary>
        public IKspSerializer Serializer { get; private set; }

        /// <summary>
        /// Gets the converter instance used for processing
        /// </summary>
        public IKspConverter<T> Converter { get; private set; }

        /// <summary>
        /// Processes data for de-serialization into a typed object instance
        /// </summary>
        /// <param name="data">data stream to de-serialize</param>
        /// <returns>de-serialized object instance</returns>
        public T Process(Stream data)
        {
            var dc = new KspDataContext();
            Serializer.DeSerialize(dc, new StreamReader(data, true));

            var obj = Converter.Convert(dc);
            return obj;
        }

        /// <summary>
        /// Processes an object for serilaization. 
        /// </summary>
        /// <param name="obj">object to serialize</param>
        /// <returns>serialized object data in KSP formatted stream</returns>
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
}
