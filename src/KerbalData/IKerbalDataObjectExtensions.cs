// -----------------------------------------------------------------------
// <copyright file="IKerbalDataObjectExtensions.cs" company="OpenSauceSolutions">
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
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Serialization;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public static class IKerbalDataObjectExtensions
    {
        /*
        public static string ToKspString<T>(this T obj, IKspSerializer serializer = null, IKspConverter<T> converter = null) where T : class, IKerbalDataObject, new()
        {
            return (new StreamReader(KspProcessor.Create<T>(serializer, converter).Process(obj))).ReadToEnd();
        }

        public static Stream ToStream<T>(this T obj, IKspSerializer serializer = null, IKspConverter<T> converter = null) where T : class, IKerbalDataObject, new()
        {
            return KspProcessor.Create<T>(serializer, converter).Process(obj);
        }

        public static string ToKspString(this JObject obj, IKspSerializer serializer = null, IKspConverter<JObject> converter = null)
        {
            return (new StreamReader(KspProcessor.Create<JObject>(serializer, converter).Process(obj))).ReadToEnd();
        }

        public static Stream ToKspStream(this JObject obj, IKspSerializer serializer = null, IKspConverter<JObject> converter = null)
        {
            return KspProcessor.Create<JObject>(serializer, converter).Process(obj);
        }

        public static T ToDataObject<T>(this Stream stream, IKspSerializer serializer = null, IKspConverter<T> converter = null) where T : class, new()
        {
            return KspProcessor.Create<T>(serializer, converter).Process(stream);
        }*/

        /*
        public static string FromStream<T>(this T obj) where T : class, IKerbalDataObject, new()
        {
            return (new StreamReader(KspProcessor.Create<T>().Process(obj))).ReadToEnd();
        }

        public static string ToJsonString<T>(this T obj) where T : class, IKerbalDataObject, new()
        {
            return null;
            //return (new StreamReader(KspProcessor.Create<T>().Process(obj))).ReadToEnd();
        }*/

    }
}
