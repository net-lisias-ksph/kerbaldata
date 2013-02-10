// -----------------------------------------------------------------------
// <copyright file="VesselActions.cs" company="OpenSauceSolutions">
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
    /// Vessel actions 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<VesselActions>))]
    public class VesselActions : KerbalDataObject
    {
    }
}
