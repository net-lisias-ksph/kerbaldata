// -----------------------------------------------------------------------
// <copyright file="VesselEvents.cs" company="OpenSauceSolutions">
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
    /// Model for vessel events
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<VesselEvents>))]
    public class VesselEvents : KerbalDataObject
    {
    }
}
