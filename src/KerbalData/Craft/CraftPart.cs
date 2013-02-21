// -----------------------------------------------------------------------
// <copyright file="CraftPart.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Data model for part object in a craft file
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<CraftPart>))]
    public class CraftPart : KerbalDataObject
    {
        private string part, name;
        private VesselEvents events;
        private VesselActions actions;
        private IList<Module> modules;

        /// <summary>
        /// Gets or sets the part. - File Property: part
        /// </summary>
        [JsonProperty("part")]
        public string Part
        {
            get
            {
                return part;
            }
            set
            {
                part = value;
                OnPropertyChanged("Part", part);
            }
        }

        /// <summary>
        /// Gets or sets the part name. - File Property: partName
        /// </summary>
        [JsonProperty("partName")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name", name);
                DisplayName = name;
            }
        }

        /// <summary>
        /// Gets and sets events. - File Property: EVENTS
        /// </summary>
        [JsonProperty("EVENTS")]
        public VesselEvents Events
        {
            get
            {
                return events;
            }
            set
            {
                events = value;
                OnPropertyChanged("Events", events);
            }
        }

        /// <summary>
        /// Gets and sets actions. - File Property: ACTIONS
        /// </summary>
        [JsonProperty("ACTIONS")]
        public VesselActions Actions
        {
            get
            {
                return actions;
            }
            set
            {
                actions = value;
                OnPropertyChanged("Actions", actions);
            }
        }

        /// <summary>
        /// Gets and sets module collection. - File Property: MODULE
        /// </summary>
        [JsonProperty("MODULE")]
        public IList<Module> Modules
        {
            get
            {
                return modules;
            }
            set
            {
                modules = value;
                OnPropertyChanged("Modules", modules);
            }
        }
    }
}
