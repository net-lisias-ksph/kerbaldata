// -----------------------------------------------------------------------
// <copyright file="Part.cs" company="OpenSauceSolutions">
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
    /// Part data model
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Part>))]
    public class Part : KerbalDataObject 
    {
        /// <summary>
        /// Gets or sets name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or setss resource collection. - File Property: RESOURCE
        /// </summary>
        [JsonProperty("RESOURCE")]
        public IList<Resource> Resources { get; set; }

        /// <summary>
        /// Gets or sets events. - File Property: EVENTS
        /// </summary>
        [JsonProperty("EVENTS")]
        public VesselEvents Events { get; set; }

        /// <summary>
        /// Gets or sets actions. - File Property: ACTIONS
        /// </summary>
        [JsonProperty("ACTIONS")]
        public VesselActions Actions { get; set; }

        /// <summary>
        /// Gets or sets part module list. - File Property: MODULE
        /// </summary>
        [JsonProperty("MODULE")]
        public IList<Module> Modules { get; set; }

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
        /// Empties all resoures in this part
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
