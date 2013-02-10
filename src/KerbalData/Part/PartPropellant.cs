// -----------------------------------------------------------------------
// <copyright file="PartPropellant.cs" company="OpenSauceSolutions">
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
    /// Data model for propellent data found in part file. 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartPropellant>))]
    public class PartPropellant : KerbalDataObject
    {
        private string name;

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
            }
        }
    }
}
