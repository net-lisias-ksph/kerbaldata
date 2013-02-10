// -----------------------------------------------------------------------
// <copyright file="FlightGameParameters.cs" company="OpenSauceSolutions">
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
    /// Model container for game flight options
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<FlightGameParameters>))]
    public class FlightGameParameters : KerbalDataObject
    {
    }
}
