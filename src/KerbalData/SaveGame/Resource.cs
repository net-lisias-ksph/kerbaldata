// -----------------------------------------------------------------------
// <copyright file="Resource.cs" company="OpenSauceSolutions">
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
    /// Resource model class
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Resource>))]
    public class Resource : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>	
        public Resource() { }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the current resource amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the maximum resource amount
        /// </summary>
        [JsonProperty("maxAmount")]
        public decimal MaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the flow state
        /// </summary>
        [JsonProperty("flowState")]
        public bool FlowState { get; set; }

        /// <summary>
        /// Gets or sets the flow mode
        /// </summary>
        [JsonProperty("flowMode")]
        public string FlowMode { get; set; }

        /// <summary>
        /// Gets the isfull flag
        /// </summary>
        public bool IsFull
        {
            get
            {
                return Amount.Equals(MaxAmount);
            }
        }

        /// <summary>
        /// Gets the isempty flag
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Amount.Equals(0);
            }
        }

        /// <summary>
        /// Fills the resource
        /// </summary>
        public void Fill()
        {
            Amount = MaxAmount;
        }

        /// <summary>
        /// Empties the resource
        /// </summary>
        public void Empty()
        {
            Amount = 0;
        }
    }
}
