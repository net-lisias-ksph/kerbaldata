// -----------------------------------------------------------------------
// <copyright file="Part.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Part data model
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Part>))]
    public class Part : KerbalDataObject 
    {
        private string name;
        private IList<Resource> resources;
        private VesselEvents events;
        private VesselActions actions;
        private IList<Module> modules;

        /// <summary>
        /// Gets or sets name. - File Property: name
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
        /// Gets or sets resource collection. - File Property: RESOURCE
        /// </summary>
        [JsonProperty("RESOURCE")]
        public IList<Resource> Resources
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
        /// Gets or sets events. - File Property: EVENTS
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
        /// Gets or sets actions. - File Property: ACTIONS
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
        /// Gets or sets part module list. - File Property: MODULE
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
                OnPropertyChanged("Module", modules);
            }
        }

        /// <summary>
        /// Fills all resources in this part.
        /// </summary>
        /// <returns>number of resource objects filled</returns>
        public int FillResources()
        {
            if (Resources == null)
            {
                return 0;
            }

            var count = 0;

            foreach (var res in Resources)
            {
                res.Fill();
                count++;
            }

            return count;
        }

        /// <summary>
        /// Empties all resources in this part
        /// </summary>
        /// <returns>number of resource objects emptied</returns>
        public int EmptyResources()
        {
            if (Resources == null)
            {
                return 0;
            }

            var count = 0;

            foreach (var res in Resources)
            {
                res.Empty();
                count++;
            }

            return count;
        }
    }
}
