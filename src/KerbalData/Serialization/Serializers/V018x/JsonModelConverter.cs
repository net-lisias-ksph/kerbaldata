// -----------------------------------------------------------------------
// <copyright file="ConsumerAPIConverter.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serialization.Serializers.V018x
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json.Linq;

    using Serialization;

    /// <summary>
    /// Converts Json.Net objets into strongly typed object instances. Wrapse <see cref="JsonObjectConverter{JObject}"/>
    /// </summary>
    public class JsonModelConverter<T> : IKspConverter<T> where T : class, new()
    {
        private IKspConverter<JObject> converter = new JsonObjectConverter<JObject>();

        /// <summary>
        /// Gets the list of versions supported by this converter
        /// </summary>
        public IList<string> Versions
        {
            get { return converter.Versions; }
        }

        /// <summary>
        /// Checks if this type supports a particular version
        /// </summary>
        /// <param name="version">version to check</param>
        /// <returns>true=supported;false=not supported</returns>
        public bool SupportsVersion(string version)
        {
            return converter.SupportsVersion(version);
        }

        /// <summary>
        /// Converts a <see cref="KspDataContext"/> instance into a specific object type
        /// </summary>
        /// <param name="context">data to map</param>
        /// <returns>object constructed based on context</returns>
        public T Convert(KspDataContext context)
        {
            return converter.Convert(context).ToObject<T>();
        }

        /// <summary>
        /// Converts an object instance into a <see cref="KspDataContext"/> instance
        /// </summary>
        /// <param name="obj">object to convert</param>
        /// <returns>data context instance based on object data</returns>
        public KspDataContext Convert(T obj)
        {
            return converter.Convert(JObject.FromObject(obj));
        }
    }
}
