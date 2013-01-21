// -----------------------------------------------------------------------
// <copyright file="PartFile.cs" company="OpenSauceSolutions">
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
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartFile>))]
    public class PartFile : StorableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartFile" /> class.
        /// </summary>	
        public PartFile()
        {
        }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            //Game = Original["GAME"].ToObject<Game>();
        }
    }
}
