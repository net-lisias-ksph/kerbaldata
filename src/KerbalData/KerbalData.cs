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
    /// Top level consumer API class used for accessing and loading KSP data. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// Classes starting with the word "Kerbal" are "top level" consumer API's. For most development, this is the easiest way to use KerbalData. The top level API provides the following features:
    /// <list type="bullet">
    ///   <item>KSP Install Aware</item>
    ///   <item>Specialized Models with extended properties to translate KSP data to standard formats (TimeSpan for game and mission time for example)</item>
    ///   <item>Utilizes configured repos allowing for easy integration with multiple data stores</item>
    ///   <item>Provides lazy loading of data</item>
    ///   <item>Maintains data used to initialize the object and state can be restored to time of load or last save</item>
    ///   <item>All operations with the top level API start with a KerbalData instance. This class will maintain all the references necessary. The constructor accepts the root path for a KSP install. Once initialized KerbalData will scan the KSP install and load initial meta-data (currently only names) but not the actual files into memory. KSP files are only serilized and loaded on initial access of pariticular named data. Once loaded the data will be maintained and no additional calls will be made to the data until Save() is called.</item>
    /// </list>      
    /// </para>
    /// </remarks>
    /// <threadsafety static="false" instance="false" />
    public class KerbalData
    {
        private string installPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalData" /> class.
        /// </summary>
        /// <example>
        /// <code language="cs" title="Starting Kerbal Data API">
        ///  var kd = new KerbalData(@"C:\KSP");
        /// </code>
        /// </example>
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
