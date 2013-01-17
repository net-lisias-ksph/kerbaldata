// -----------------------------------------------------------------------
// <copyright file="StorableItemMetadata.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public class StorableItemMetadata<T> where T : class, IStorable, new()
    {
        public StorableItemMetadata()
        {
            Loaded = false;
        }

        public string Id { get; set; }
        public string Uri { get; set; }
        public bool Loaded { get; set; }
        public T Object { get; set; }
    }
}
