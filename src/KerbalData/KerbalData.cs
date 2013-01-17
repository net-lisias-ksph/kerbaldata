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
    /// TODO: Class Summary
    /// </summary>
    public class KerbalData
    {
        private string installPath;
        private IKerbalDataRepo<SaveFile> saveRepo;
       
        public KerbalData(string installPath)
        {
            this.installPath = installPath.EndsWith("\\") ? installPath : installPath + "\\";
            Init();

        }

        public StorableObjects<SaveFile> Saves
        {
            get;
            private set;
        }

        private void Init()
        {
            
            Saves = new StorableObjects<SaveFile>(RepoFactory.Create<SaveFile>(new[] { installPath + @"Saves\**\persistent.sfs" } ));

            // Parts = new StorableObjects<SaveFile>(new KspInstallFileRepo<SaveFile>(installPath + @"\Saves"));

            // Ships = new StorableObjects<SaveFile>(new KspInstallFileRepo<SaveFile>(installPath + @"\Saves"));

            // Configuration = new StorableObjects<SaveFile>(new KspInstallFileRepo<SaveFile>(installPath + @"\Saves"));
        }

    }
}
