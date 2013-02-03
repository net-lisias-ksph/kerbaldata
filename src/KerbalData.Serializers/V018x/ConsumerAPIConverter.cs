// -----------------------------------------------------------------------
// <copyright file="ConsumerAPIConverter.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serializers.V018x
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json.Linq;

    using Serialization;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public class ConsumerAPIConverter<T> : IKspConverter<T> where T : class, IKerbalDataObject, new()
    {
        private IKspConverter<JObject> converter = new JsonObjectConverter<JObject>();

        public IList<string> Versions
        {
            get { return converter.Versions; }
        }

        public bool SupportsVersion(string version)
        {
            return converter.SupportsVersion(version);
        }

        public T Convert(KspDataContext context)
        {
            return converter.Convert(context).ToObject<T>();
        }

        public KspDataContext Convert(T obj)
        {
            return converter.Convert(JObject.FromObject(obj));
        }
    }
}
