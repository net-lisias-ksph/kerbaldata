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

        /// <summary>
        /// Gets the keys for unmapped properties stored by this object
        /// </summary>
        [JsonIgnore]
        public new KeyCollection Keys
        {
            get { return base.Keys; }
        }

        /// <summary>
        /// Gets the values collection
        /// </summary>
        [JsonIgnore]
        public new ValueCollection Values
        {
            get { return base.Values; }
        }

        /// <summary>
        /// Gets the comparer
        /// </summary>
        [JsonIgnore]
        public new IEqualityComparer<string> Comparer
        {
            get { return base.Comparer; }
        }

        /// <summary>
        /// Gets the count of unmapped elements
        /// </summary>
        [JsonIgnore]
        public new int Count
        {
            get { return base.Count; }
        }
    }
}
