// -----------------------------------------------------------------------
// <copyright file="TrackingStationGameParameters.cs" company="OpenSauceSolutions">
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
    /// Model for tracking station params in the save
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<TrackingStationGameParameters>))]
    public class TrackingStationGameParameters : KerbalDataObject
    {
    }
}
