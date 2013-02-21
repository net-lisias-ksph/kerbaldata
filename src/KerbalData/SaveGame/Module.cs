// -----------------------------------------------------------------------
// <copyright file="Module.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    // TODO: Possible merge with PartModule. 

    /// <summary>
    /// Model container for a save data module instance
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Module>))]
    public class Module : KerbalDataObject
    {
        private string name;
        private IList<PartResource> resources;
        private VesselEvents events;
        private VesselActions actions;

        /// <summary>
        /// Gets or sets the name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
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
        /// Gets or sets the resource list. - File Property: RESOURCE
        /// </summary>
        [JsonProperty("RESOURCE")]
        public IList<PartResource> Resources
        {
            get
            {
                return resources;
            }
            set
            {
                resources = value;
                OnPropertyChanged("Resources", resources);
            }
        }

        /// <summary>
        /// Gets or sets module events. - File Property: EVENTS
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
        /// Gets or sets module actions. - File Property: ACTIONS
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
    }
}
