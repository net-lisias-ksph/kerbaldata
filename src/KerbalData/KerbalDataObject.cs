// -----------------------------------------------------------------------
// <copyright file="KerbalDataObject.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    [JsonObject]
    public class KerbalDataObject : Dictionary<string, JToken>, IKerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataObject" /> class.
        /// </summary>	
        public KerbalDataObject() : base()
        {
        }

        [JsonIgnore]
        public new KeyCollection Keys
        {
            get { return base.Keys; }
        }

        [JsonIgnore]
        public new ValueCollection Values
        {
            get { return base.Values; }
        }

        [JsonIgnore]
        public new IEqualityComparer<string> Comparer
        {
            get { return base.Comparer; }
        }

        [JsonIgnore]
        public new int Count
        {
            get { return base.Count; }
        }
    }
}
