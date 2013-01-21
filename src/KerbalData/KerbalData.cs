// -----------------------------------------------------------------------
// <copyright file="KerbalData.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Top level consumer API class. For easiest access use this class to edit and manage KSP data. 
    /// </summary>
    public class KerbalData
    {
        private string installPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalData" /> class.
        /// </summary>	
        /// <param name="installPath">path of a valid KSP install currently only tested support of 0.18.x</param>
        public KerbalData(string installPath)
        {
            this.installPath = installPath.EndsWith("\\") ? installPath : installPath + "\\";
            Init();

        }

        /// <summary>
        /// Gets the saves stored in this installation
        /// </summary>
        public StorableObjects<SaveFile> Saves
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the scenarios stored in this installation
        /// </summary>
        public StorableObjects<SaveFile> Scenarios
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the training scenarios stored in this installation
        /// </summary>
        public StorableObjects<SaveFile> TrainingScenarios
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the parts stored in this installation
        /// </summary>
        public StorableObjects<PartFile> Parts
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the VAB craft stored in this installation
        /// </summary>
        public StorableObjects<CraftFile> CraftInVab
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the SPH craft stored in this installation
        /// </summary>
        public StorableObjects<CraftFile> CraftInSph
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the settings and configuration files for this installation
        /// </summary>
        public StorableObjects<ConfigFile> KspSettings
        {
            get;
            private set;
        }

        private void Init()
        {
            Saves = new StorableObjects<SaveFile>(RepoFactory.Create<SaveFile>(new[] { installPath + @"Saves\**\persistent.sfs", null, "persistent.sfs" }));
            Scenarios = new StorableObjects<SaveFile>(RepoFactory.Create<SaveFile>(new object[] { installPath + @"Saves\scenarios\*.sfs", null, ".sfs", true }));
            TrainingScenarios = new StorableObjects<SaveFile>(RepoFactory.Create<SaveFile>(new object[] { installPath + @"Saves\training\*.sfs", null, ".sfs", true }));
            Parts = new StorableObjects<PartFile>(RepoFactory.Create<PartFile>(new[] { installPath + @"Parts\**\part.cfg", null, "part.cfg" }));
            CraftInVab = new StorableObjects<CraftFile>(RepoFactory.Create<CraftFile>(new object[] { installPath + @"Ships\VAB\**\*.craft", null, ".craft", true }));
            CraftInSph = new StorableObjects<CraftFile>(RepoFactory.Create<CraftFile>(new object[] { installPath + @"Ships\SPH\**\*.craft", null, ".craft", true }));
            KspSettings = new StorableObjects<ConfigFile>(RepoFactory.Create<ConfigFile>(new object[] { installPath + @"*.cfg", null, ".cfg", true }));
        }

    }
}
