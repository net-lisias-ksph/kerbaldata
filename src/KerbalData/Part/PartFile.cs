// -----------------------------------------------------------------------
// <copyright file="PartFile.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
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
        /// Gets or sets the name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the title. - File Property: title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author. - File Property: author
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer. - File Property: manufacturer
        /// </summary>
        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the description. - File Property: description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the vessel type. - File Property: vesselType
        /// </summary>
        [JsonProperty("vesselType")]
        public string VesselType { get; set; }

        /// <summary>
        /// Gets or sets the module. - File Property: module
        /// </summary>
        [JsonProperty("module")]
        public string Module { get; set; }

        /// <summary>
        /// Gets or sets the mesh file name. - File Property: mesh
        /// </summary>
        [JsonProperty("mesh")]
        public string Mesh { get; set; }

        /// <summary>
        /// Gets or sets the rescale factor. - File Property: rescaleFactor
        /// </summary>
        [JsonProperty("rescaleFactor")]
        public int RescaleFactor { get; set; }

        /// <summary>
        /// Gets or sets the cost. - File Property: cost
        /// </summary>
        [JsonProperty("cost")]
        public int Cost { get; set; }

        /// <summary>
        /// Gets or sets the category. - File Property: category
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the sub category. - File Property: subcategory
        /// </summary>
        [JsonProperty("subcategory")]
        public string SubCategory { get; set; }

        // Standard Part Parameters
        /// <summary>
        /// Gets or sets the mass. - File Property: mass
        /// </summary>
        [JsonProperty("mass")]
        public decimal Mass { get; set; }

        /// <summary>
        /// Gets or sets the drag model type. - File Property: dragModelType
        /// </summary>
        [JsonProperty("dragModelType")]
        public string DragModelType { get; set; }

        /// <summary>
        /// Gets or sets maximum drag. - File Property: maximum_drag
        /// </summary>
        [JsonProperty("maximum_drag")]
        public decimal MaxDrag { get; set; }

        /// <summary>
        /// Gets or set minimum drag. - File Property: minimum_drag
        /// </summary>
        [JsonProperty("minimum_drag")]
        public decimal MinDrag { get; set; }

        /// <summary>
        /// Gets or sets angular drag. - File Property: angularDrag
        /// </summary>
        [JsonProperty("angularDrag")]
        public int AngularDrag { get; set; }

        /// <summary>
        /// Gets or sets the crash tolerance. - File Property: crashTolerance
        /// </summary>
        [JsonProperty("crashTolerance")]
        public int CrashTolerance { get; set; }

        /// <summary>
        /// Gets or sets the breaking force. - File Property: breakingForce
        /// </summary>
        [JsonProperty("breakingForce")]
        public int BreakingForce { get; set; }

        /// <summary>
        /// Gets or sets the breaking torque. - File Property: breakingTorque
        /// </summary>
        [JsonProperty("breakingTorque")]
        public int BreakingTorque { get; set; }

        /// <summary>
        /// Gets or sets the maximum temp. - File Property: maxTemp
        /// </summary>
        [JsonProperty("maxTemp")]
        public int MaxTemp { get; set; }

        /// <summary>
        /// Gets or sets the explosion potential. - File Property: explosionPotential
        /// </summary>
        [JsonProperty("explosionPotential")]
        public int ExplosionPotential { get; set; }        

        /// <summary>
        /// Gets or sets the module collection. - File Property: MODULE
        /// </summary>
        [JsonProperty("MODULE")]
        public IList<PartModule> Modules { get; set; }

        /// <summary>
        /// Gets or sets the resource collection. - File Property: RESOURCE
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
