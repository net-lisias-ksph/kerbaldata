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
    /// TODO: Class Summary
    /// </summary>
    [JsonObject]
    public class SaveFile : StorableObject
    {
        private bool isLoaded = false;

        private StorableObjects<CraftFile> craftInVab;
        private StorableObjects<CraftFile> craftInSph;

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

        [JsonIgnore]
        public StorableObjects<CraftFile> CraftInVab
        {
            get
            {
                LoadCraft();
                return craftInVab;
            }
            private set
            {
                craftInVab = value;
            }
        }

        [JsonIgnore]
        public StorableObjects<CraftFile> CraftInSph
        {
            get
            {
                LoadCraft();
                return craftInSph;
            }
            private set
            {
                craftInSph = value;
            }
        }

        private void LoadCraft()
        {
            if (!isLoaded)
            {
                CraftInVab = new StorableObjects<CraftFile>(RepoFactory.Create<CraftFile>(new object[] { Uri + @"\Ships\VAB\**\*.craft", null, ".craft", true }));
                CraftInSph = new StorableObjects<CraftFile>(RepoFactory.Create<CraftFile>(new object[] { Uri + @"\Ships\SPH\**\*.craft", null, ".craft", true }));
                isLoaded = true;
            }
        }
    }
}
