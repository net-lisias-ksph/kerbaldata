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

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer
        /// </summary>
        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the vessel type
        /// </summary>
        [JsonProperty("vesselType")]
        public string VesselType { get; set; }

        /// <summary>
        /// Gets or sets the module
        /// </summary>
        [JsonProperty("module")]
        public string Module { get; set; }

        /// <summary>
        /// Gets or sets the mesh file name
        /// </summary>
        [JsonProperty("mesh")]
        public string Mesh { get; set; }

        /// <summary>
        /// Gets or sets the rescale factor
        /// </summary>
        [JsonProperty("rescaleFactor")]
        public int RescaleFactor { get; set; }

        /// <summary>
        /// Gets or sets the cost
        /// </summary>
        [JsonProperty("cost")]
        public int Cost { get; set; }

        /// <summary>
        /// Gets or sets the category
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the sub category
        /// </summary>
        [JsonProperty("subcategory")]
        public string SubCategory { get; set; }

        // Standard Part Parameters
        /// <summary>
        /// Gets or sets the mass
        /// </summary>
        [JsonProperty("mass")]
        public decimal Mass { get; set; }

        /// <summary>
        /// Gets or sets the drag model type
        /// </summary>
        [JsonProperty("dragModelType")]
        public string DragModelType { get; set; }

        /// <summary>
        /// Gets or sets maximum drag
        /// </summary>
        [JsonProperty("maximum_drag")]
        public decimal MaxDrag { get; set; }

        /// <summary>
        /// Gets or set minimum drag
        /// </summary>
        [JsonProperty("minimum_drag")]
        public decimal MinDrag { get; set; }

        /// <summary>
        /// Gets or sets angular drag
        /// </summary>
        [JsonProperty("angularDrag")]
        public int AngularDrag { get; set; }

        /// <summary>
        /// Gets or sets the crash tolerance
        /// </summary>
        [JsonProperty("crashTolerance")]
        public int CrashTolerance { get; set; }

        /// <summary>
        /// Gets or sets the breaking force
        /// </summary>
        [JsonProperty("breakingForce")]
        public int BreakingForce { get; set; }

        /// <summary>
        /// Gets or sets the breaking torque 
        /// </summary>
        [JsonProperty("breakingTorque")]
        public int BreakingTorque { get; set; }

        /// <summary>
        /// Gets or sets the maximum temp
        /// </summary>
        [JsonProperty("maxTemp")]
        public int MaxTemp { get; set; }

        /// <summary>
        /// Gets or sets the explosion potential
        /// </summary>
        [JsonProperty("explosionPotential")]
        public int ExplosionPotential { get; set; }        

        /// <summary>
        /// Gets or sets the module collection
        /// </summary>
        [JsonProperty("MODULE")]
        public IList<PartModule> Modules { get; set; }

        /// <summary>
        /// Gets or sets the resource collection
        /// </summary>
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
