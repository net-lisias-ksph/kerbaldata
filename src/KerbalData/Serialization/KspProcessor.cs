// -----------------------------------------------------------------------
// <copyright file="KspProcessor.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public class KspProcessor<T> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KspProcessor" /> class.
        /// </summary>	
        internal KspProcessor(IKspSerializer serializer, IKspConverter<T> converter)
        {
            Serializer = serializer;
            Converter = converter;
        }

        public IKspSerializer Serializer { get; private set; }

        public IKspConverter<T> Converter { get; private set; }

        public T Process(Stream data)
        {
            var dc = new KspDataContext();
            Serializer.DeSerialize(dc, new StreamReader(data));

            var obj = Converter.Convert(dc);
            return obj;
        }

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

    /// <summary>
    /// Factory class for processor creation
    /// </summary>
    public static class KspProcessor
    {
        /// <summary>
        /// Creates a new instance of <see cref="KspProcessor{T}"/> based upon context and configuration. 
        /// </summary>
        /// <typeparam name="T">object type for processor to serialize to/from</typeparam>
        /// <param name="serializer">serializer instance to use, if none provided the factory will inject one</param>
        /// <param name="converter">converter instance to use, if non provided the factory will inject one</param>
        /// <returns>properly configured KspProcessor instance for use in serializing/deserailizing objects of the defined type</returns>
        public static KspProcessor<T> Create<T>(IKspSerializer serializer = null, IKspConverter<T> converter = null) where T : class, new()
        {
            var modelType = typeof(T);
            var serializerType = Type.GetType("KerbalData.Serializers.V018x.DataSerializer, KerbalData.Serializers");
            
            var jsonConverterType = Type.GetType("KerbalData.Serializers.V018x.JsonObjectConverter`1, KerbalData.Serializers");
            

            
            var objConverterType = Type.GetType("KerbalData.Serializers.V018x.ConsumerAPIConverter`1, KerbalData.Serializers");

            var conv = converter == null 
                ? modelType.FullName.Contains("JObject") 
                    ? Activator.CreateInstance(jsonConverterType.MakeGenericType(new Type[] { typeof(T) })) as IKspConverter<T>
                    : Activator.CreateInstance(objConverterType.MakeGenericType(new Type[] { typeof(T) })) as IKspConverter<T> 
                : converter;

            return new KspProcessor<T>(
                serializer == null
                ? Activator.CreateInstance(serializerType) as IKspSerializer
                : serializer,
                conv);
        }
    }
}
