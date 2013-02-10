// -----------------------------------------------------------------------
// <copyright file="CraftFile.cs" company="OpenSauceSolutions">
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
    /// Represents a loaded craft file
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<CraftFile>))]
    public class CraftFile : StorableObject
    {
        /// <summary>
        /// Gets or sets the ship name. - File Property: ship
        /// </summary>
        [JsonProperty("ship")]
        public string Ship { get; set; }

        /// <summary>
        /// Gets or sets the version. - File Property: version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the type. - File Property: type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the parts collection. - File Property: PART
        /// </summary>
        [JsonProperty("PART")]
        public IList<CraftPart> Parts { get; set; }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            // TODO: Implementation
        }
    }
}
