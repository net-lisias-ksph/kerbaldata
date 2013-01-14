// -----------------------------------------------------------------------
// <copyright file="ActionGroups.cs" company="OpenSauceSolutions">
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
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<ActionGroups>))]
    public class ActionGroups : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionGroups" /> class.
        /// </summary>	
        public ActionGroups()
        {
        }
    }
}
