// -----------------------------------------------------------------------
// <copyright file="StorableObjectTests.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    using Models;
    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    [TestFixture]
    public class StorableObjectTests : BaseDataTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorableObjectTests" /> class.
        /// </summary>	
        public StorableObjectTests()
        {
        }

        [Test]
        public void SingleFileSaveTest()
        {
            var save = KspData.LoadKspFile(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\persistent.sfs", ProcessorRegistry.Create().Create<SaveFile>());

            save.Game.Description = "HELLO WORLD!";
            save.Save();

            var changed = KspData.LoadKspFile(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\persistent.sfs", ProcessorRegistry.Create().Create<SaveFile>());
            Assert.AreEqual(save.Game.Description, changed.Game.Description);

            changed.Save(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\newname.sfs");

            var newsave = KspData.LoadKspFile(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\newname.sfs", ProcessorRegistry.Create().Create<SaveFile>());
            Assert.AreEqual(save.Game.Description, newsave.Game.Description);
        }

        [Test]
        public void CloneTest()
        {
            var save = KspData.LoadKspFile(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\persistent.sfs", ProcessorRegistry.Create().Create<SaveFile>());
            var clone = save.Clone<SaveFile>();

            Assert.AreEqual(save.Game.Description, clone.Game.Description);
        }
    }
}
