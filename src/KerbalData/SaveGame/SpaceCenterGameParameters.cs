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
    /// TODO: Class Summary
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<SpaceCenterGameParameters>))]
    public class SpaceCenterGameParameters : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceCenterGameParameters" /> class.
        /// </summary>	
        public SpaceCenterGameParameters()
        {
        }
    }
}
