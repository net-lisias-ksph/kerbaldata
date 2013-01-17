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

        [JsonProperty("pid")]
        public string Pid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sit")]
        public string Situation { get; set; }

        [JsonProperty("landed")]
        public bool Landed { get; set; }

        [JsonProperty("landedAt")]
        public string LandedAt { get; set; }

        [JsonProperty("splashed")]
        public bool Splashed { get; set; }

        [JsonProperty("met")]
        public decimal Met { get; set; }

        public TimeSpan MissionElapsedTime
        {
            get
            {
                return new TimeSpan((long)Met * 1000000L);
            }
            set
            {
                Met = value.Ticks / 1000000L;
            }
        }

        [JsonProperty("lct")]
        public decimal Lct { get; set; }

        [JsonProperty("root")]
        public int Root { get; set; }

        [JsonProperty("lat")]
        public decimal Latitude { get; set; }

        [JsonProperty("lon")]
        public decimal Longitude { get; set; }

        [JsonProperty("alt")]
        public decimal Altitude { get; set; }

        [JsonProperty("hgt")]
        public decimal Hgt { get; set; }

        [JsonProperty("nrm")]
        public decimal[] Nrm { get; set; }
        
        [JsonProperty("rot")]
        public decimal[] Rot { get; set; }

        [JsonProperty("CoM")]
        public float[] Com { get; set; }

        [JsonProperty("stg")]
        public int Stage { get; set; }

        [JsonProperty("prst")]
        public bool Prst { get; set; }

        [JsonProperty("eva")]
        public bool Eva { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }

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
