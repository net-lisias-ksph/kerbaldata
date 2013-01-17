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
    /// TODO: Class Summary
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Resource>))]
    public class Resource : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>	
        public Resource()
        {
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("maxAmount")]
        public decimal MaxAmount { get; set; }

        [JsonProperty("flowState")]
        public bool FlowState { get; set; }

        [JsonProperty("flowMode")]
        public string FlowMode { get; set; }

        public bool IsFull
        {
            get
            {
                return Amount.Equals(MaxAmount);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Amount.Equals(0);
            }
        }

        public void Fill()
        {
            Amount = MaxAmount;
        }

        public void Empty()
        {
            Amount = 0;
        }
    }
}
