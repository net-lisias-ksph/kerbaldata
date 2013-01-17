// -----------------------------------------------------------------------
// <copyright file="SaveFile.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    [JsonObject]
    public class SaveFile : StorableObject
    {
        public SaveFile()
        {

        }

        /// <summary>
        /// Gets the KSP Game definition. Contains all de-serialized save data. 
        /// </summary>
        [JsonProperty("GAME")]
        public Game Game 
        { get; private set; }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            Game = Original["GAME"].ToObject<Game>();
        }
    }
}
