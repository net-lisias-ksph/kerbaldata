// -----------------------------------------------------------------------
// <copyright file="EditorGameParameters.cs" company="OpenSauceSolutions">
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
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<EditorGameParameters>))]
    public class EditorGameParameters : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorGameParameters" /> class.
        /// </summary>	
        public EditorGameParameters()
        {
        }
    }
}
