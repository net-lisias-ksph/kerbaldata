// -----------------------------------------------------------------------
// <copyright file="Crew.cs" company="OpenSauceSolutions">
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
    /// Crew data 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Crew>))]
    public class Crew : KerbalDataObject 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Crew" /> class.
        /// </summary>	
        public Crew()
        {
        }

        /// <summary>
        /// Gets or sets the crew memeber's name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the bravery level
        /// </summary>
        [JsonProperty("brave")]
        public decimal Brave { get; set; }


        /// <summary>
        /// Gets or sets the dumbness level
        /// </summary>
        [JsonProperty("dumb")]
        public decimal Dumb { get; set; }

        /// <summary>
        /// Gets or sets the BadS level
        /// </summary>
        [JsonProperty("badS")]
        public bool BadS { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        [JsonProperty("state")]
        public int State { get; set; }

        /// <summary>
        /// Gets or sets the ToD
        /// </summary>
        [JsonProperty("ToD")]
        public decimal ToD { get; set; }

        /// <summary>
        /// Gets or sets the index
        /// </summary>
        [JsonProperty("idx")]
        public decimal Index { get; set; }
    }
}
