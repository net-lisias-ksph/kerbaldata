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
    using NAnt.Core.Functions;

    /// <summary>
    /// Instances of this class contains reference and management of a KSP save file.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    [JsonObject]
    public class SaveFile : StorableObject
    {
        private bool isLoaded = false;

        /// <summary>
        /// Gets the KSP Game definition. Contains all de-serialized save data. - File Property: GAME
        /// </summary>
        [JsonProperty("GAME")]
        public Game Game 
        { 
            get;
            private set; 
        }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            Game = Original["GAME"].ToObject<Game>();
        }

        /// <summary>
        /// Gets or sets the craft in VAP collection
        /// </summary>
        [JsonIgnore]
        public StorableObjects<CraftFile> CraftInVab
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the craft in the space plane hanger collection 
        /// </summary>
        [JsonIgnore]
        public StorableObjects<CraftFile> CraftInSph
        {
            get;
            set;
        }
    }
}
