// -----------------------------------------------------------------------
// <copyright file="SpaceCenterGameParameters.cs" company="OpenSauceSolutions">
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
    /// Model for Space Center params in a save file
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<SpaceCenterGameParameters>))]
    public class SpaceCenterGameParameters : KerbalDataObject
    {
    }
}
