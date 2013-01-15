// -----------------------------------------------------------------------
// <copyright file="VesselActions.cs" company="OpenSauceSolutions">
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
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<VesselActions>))]
    public class VesselActions : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VesselActions" /> class.
        /// </summary>	
        public VesselActions()
        {
        }
    }
}
