// -----------------------------------------------------------------------
// <copyright file="Vessel.cs" company="OpenSauceSolutions">
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
    /// 
    //[JsonObject]
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Vessel>))]
    public class Vessel : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vessel" /> class.
        /// </summary>	
        public Vessel() : base()
        {

        }

        [JsonProperty("ORBIT")]
        public Orbit Orbit { get; set; }

        [JsonProperty("PART")]
        public IList<Part> Parts { get; set; }

        [JsonProperty("ACTIONGROUPS")]
        public ActionGroups ActionGroups { get; set; }

        public int FillResources()
        {
            if (Parts == null)
            {
                return 0;
            }

            var count = 0;

            foreach (var res in Parts)
            {
                res.FillResources();
                count++;
            }

            return count;
        }

        public int EmptyResources()
        {
            if (Parts == null)
            {
                return 0;
            }

            var count = 0;

            foreach (var res in Parts)
            {
                res.EmptyResources();
                count++;
            }

            return count;
        }

    }
}
