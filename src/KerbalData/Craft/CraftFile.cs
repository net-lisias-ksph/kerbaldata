// -----------------------------------------------------------------------
// <copyright file="CraftFile.cs" company="OpenSauceSolutions">
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
    /// Represents a loaded craft file
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartFile>))]
    public class CraftFile : StorableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CraftFile" /> class.
        /// </summary>	
        public CraftFile()
        {
        }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            // TODO: Implementation
        }
    }
}
