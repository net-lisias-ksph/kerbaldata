// -----------------------------------------------------------------------
// <copyright file="EditorGameParameters.cs" company="OpenSauceSolutions">
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
    /// Model for Editor prefs
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<EditorGameParameters>))]
    public class EditorGameParameters : KerbalDataObject
    {
    }
}
