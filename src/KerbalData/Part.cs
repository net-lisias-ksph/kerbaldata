// -----------------------------------------------------------------------
// <copyright file="Part.cs" company="OpenSauceSolutions">
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
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Part>))]
    public class Part : KerbalDataObject 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Part" /> class.
        /// </summary>	
        public Part()
        {
        }

        [JsonProperty("RESOURCE")]
        public IList<Resource> Resources { get; set; }

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
