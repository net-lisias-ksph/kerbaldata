// -----------------------------------------------------------------------
// <copyright file="SaveFile.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Instances of this class contains reference and management of a KSP save file.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    [JsonObject]
    public class SaveFile : StorableObject
    {
        private Game game;

        /// <summary>
        /// Gets the KSP Game definition. Contains all de-serialized save data. - File Property: GAME
        /// </summary>
        [JsonProperty("GAME")]
        public Game Game 
        {
            get
            {
                return game;
            }
            set
            {
                game = value;
                OnPropertyChanged("Game", game);
                DisplayName = game.DisplayName;
            }

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

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            // TODO: IMPL
        }

        protected override void Init()
        {
            var vabUri = (Uri.EndsWith("\\") ? Uri : Uri + "\\") + "Ships\\VAB\\";
            CraftInVab = 
                new StorableObjects<CraftFile>(
                    DataManager.RepositoryFactory.Create<CraftFile>(
                    new Dictionary<string, object>() { { "BaseUri", vabUri }, { "Include", vabUri + "**\\*.craft" } }, 
                    "CraftInVab-Save"), DataManager);

            var sphUri = (Uri.EndsWith("\\") ? Uri : Uri + "\\") + "Ships\\SPH\\";
            CraftInSph =
                new StorableObjects<CraftFile>(
                    DataManager.RepositoryFactory.Create<CraftFile>(
                    new Dictionary<string, object>() { { "BaseUri", sphUri }, { "Include", sphUri + "**\\*.craft" } },
                    "CraftInSph-Save"), DataManager);
        }

    }
}
