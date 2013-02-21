// -----------------------------------------------------------------------
// <copyright file="CraftFile.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Represents a loaded craft file
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<CraftFile>))]
    public class CraftFile : StorableObject
    {
        private string ship, version, type;
        private IList<CraftPart> parts;

        /// <summary>
        /// Gets or sets the ship name. - File Property: ship
        /// </summary>
        [JsonProperty("ship")]
        public string Ship
        {
            get
            {
                return ship;
            }
            set
            {
                ship = value;
                OnPropertyChanged("Ship", ship);
                DisplayName = ship;
            }
        }

        /// <summary>
        /// Gets or sets the version. - File Property: version
        /// </summary>
        [JsonProperty("version")]
        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                OnPropertyChanged("Version", version);
            }
        }

        /// <summary>
        /// Gets or sets the type. - File Property: type
        /// </summary>
        [JsonProperty("type")]
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged("Type", type);
            }
        }

        /// <summary>
        /// Gets or sets the parts collection. - File Property: PART
        /// </summary>
        [JsonProperty("PART")]
        public IList<CraftPart> Parts
        {
            get
            {
                return parts;
            }
            set
            {
                parts = value;
                OnPropertyChanged("Parts", parts);
            }
        }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            // TODO: Implementation
        }
    }
}
