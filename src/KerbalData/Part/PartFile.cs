// -----------------------------------------------------------------------
// <copyright file="PartFile.cs" company="OpenSauceSolutions">
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

    /// <summary>
    /// Represents a loaded part file
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartFile>))]
    public class PartFile : StorableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartFile" /> class.
        /// </summary>	
        public PartFile()
        {
        }

        // Part Meta-data
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("vesselType")]
        public string VesselType { get; set; }

        [JsonProperty("module")]
        public string Module { get; set; }

        [JsonProperty("mesh")]
        public string Mesh { get; set; }

        [JsonProperty("rescaleFactor")]
        public int RescaleFactor { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("subcategory")]
        public string SubCategory { get; set; }

        // Standard Part Parameters
        [JsonProperty("mass")]
        public decimal Mass { get; set; }

        [JsonProperty("dragModelType")]
        public string DragModelType { get; set; }

        [JsonProperty("maximum_drag")]
        public decimal MaxDrag { get; set; }

        [JsonProperty("minimum_drag")]
        public decimal MinDrag { get; set; }

        [JsonProperty("angularDrag")]
        public int AngularDrag { get; set; }

        [JsonProperty("crashTolerance")]
        public int CrashTolerance { get; set; }

        [JsonProperty("breakingForce")]
        public int BreakingForce { get; set; }

        [JsonProperty("breakingTorque")]
        public int BreakingTorque { get; set; }

        [JsonProperty("maxTemp")]
        public int MaxTemp { get; set; }

        [JsonProperty("explosionPotential")]
        public int ExplosionPotential { get; set; }        

        [JsonProperty("MODULE")]
        public IList<PartModule> Modules { get; set; }

        [JsonProperty("RESOURCE")]
        public IList<PartResource> Resources { get; set; }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            // TODO: Implementation
        }
    }
}
