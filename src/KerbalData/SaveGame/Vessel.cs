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
    /// Vessel data model
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Vessel>))]
    public class Vessel : KerbalDataObject
    {
        /// <summary>
        /// Gets or sets the Pid. - File Property: GAME
        /// </summary>
        [JsonProperty("pid")]
        public string Pid { get; set; }

        /// <summary>
        /// Gets or sets the name. - File Property: GAME
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type. - File Property: GAME
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the situation. - File Property: GAME
        /// </summary>
        [JsonProperty("sit")]
        public string Situation { get; set; }

        /// <summary>
        /// Gets or sets the landed flag. - File Property: GAME
        /// </summary>
        [JsonProperty("landed")]
        public bool Landed { get; set; }

        /// <summary>
        /// Gets or sets the landed at value. - File Property: GAME
        /// </summary>
        [JsonProperty("landedAt")]
        public string LandedAt { get; set; }

        /// <summary>
        /// Gets or sets the splashed flag. - File Property: GAME
        /// </summary>
        [JsonProperty("splashed")]
        public bool Splashed { get; set; }

        /// <summary>
        /// Gets or sets the Met. - File Property: GAME
        /// </summary>
        [JsonProperty("met")]
        public decimal Met { get; set; }

        /// <summary>
        /// Gets or sets Mission Elapsed time
        /// </summary>
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

        /// <summary>
        /// Gets or sets lct. - File Property: lct
        /// </summary>
        [JsonProperty("lct")]
        public decimal Lct { get; set; }

        /// <summary>
        /// Gets or sets root. - File Property: root
        /// </summary>
        [JsonProperty("root")]
        public int Root { get; set; }

        /// <summary>
        /// Gets or sets latitude. - File Property: lat
        /// </summary>
        [JsonProperty("lat")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets longitude. - File Property: lon
        /// </summary>
        [JsonProperty("lon")]
        public decimal Longitude { get; set; }

        /// <summary>
        /// Gets or sets altitude. - File Property: alt
        /// </summary>
        [JsonProperty("alt")]
        public decimal Altitude { get; set; }

        /// <summary>
        /// Gets or sets hgt. - File Property: hgt
        /// </summary>
        [JsonProperty("hgt")]
        public decimal Hgt { get; set; }

        /// <summary>
        /// Gets or sets nrm. - File Property: nrm
        /// </summary>
        [JsonProperty("nrm")]
        public decimal[] Nrm { get; set; }
        
        /// <summary>
        /// Gets or sets Rot. - File Property: rot
        /// </summary>
        [JsonProperty("rot")]
        public decimal[] Rot { get; set; }

        /// <summary>
        /// Gets or sets Com. - File Property: CoM
        /// </summary>
        [JsonProperty("CoM")]
        public float[] Com { get; set; }

        /// <summary>
        /// Gets or sets stage. - File Property: stg
        /// </summary>
        [JsonProperty("stg")]
        public int Stage { get; set; }

        /// <summary>
        /// Gets or sets prst. - File Property: Prst
        /// </summary>
        [JsonProperty("prst")]
        public bool Prst { get; set; }

        /// <summary>
        /// Gets or sets eva. - File Property: Eva
        /// </summary>
        [JsonProperty("eva")]
        public bool Eva { get; set; }

        /// <summary>
        /// Gets or sets ref. - File Property: ref
        /// </summary>
        [JsonProperty("ref")]
        public string Ref { get; set; }

        /// <summary>
        /// Gets or sets orbit. - File Property: ORBIT
        /// </summary>
        [JsonProperty("ORBIT")]
        public Orbit Orbit { get; set; }

        /// <summary>
        /// Gets or sets part collection. - File Property: PART
        /// </summary>
        [JsonProperty("PART")]
        public IList<Part> Parts { get; set; }

        /// <summary>
        /// Gets or sets vessel action groups. - File Property: ACTIONGROUPS
        /// </summary>
        [JsonProperty("ACTIONGROUPS")]
        public ActionGroups ActionGroups { get; set; }

        /// <summary>
        /// Fills all resource parts in this vessel
        /// </summary>
        /// <returns>number of resource parts filed</returns>
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

        /// <summary>
        /// Emppties all resources in this vessel
        /// </summary>
        /// <returns>number of parts emptied</returns>
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
