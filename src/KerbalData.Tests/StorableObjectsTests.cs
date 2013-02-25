// -----------------------------------------------------------------------
// <copyright file="StorableObjectsTests.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Models;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    [TestFixture]
    public class StorableObjectsTests : KerbalDataBaseTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorableObjectsTests" /> class.
        /// </summary>	
        public StorableObjectsTests()
        {
        }

        [Test]
        public void TestAdd()
        {
            
            var save =
                KspData.LoadKspFile(TestHelpers.BaseDataPath() + @"saves\KspPersistentSfswMods\persistent.sfs", Data.ProcRegistry.Create<SaveFile>());

            Data.Saves.Add(save, "Test-Save-001");
            Data.Saves["Test-Save-001"].Save();

            var savedSave01 = KspData.LoadKspFile<SaveFile>(TestHelpers.BaseDataTempPath() + @"saves\Test-Save-001\persistent.sfs");

            Assert.IsNotNull(savedSave01);

            Data.Saves["Test-Save-002"] = save;
            save.Save();

            var savedSave02 = KspData.LoadKspFile<SaveFile>(TestHelpers.BaseDataTempPath() + @"saves\Test-Save-002\persistent.sfs");

            Assert.IsNotNull(savedSave02);
            Assert.AreEqual(save.Game.FlightState.Vessels.Count, savedSave02.Game.FlightState.Vessels.Count);

        }

        [Test]
        public void TestRemove()
        {
            Data.Saves.Remove("KspPersistentSfswMods");

            Assert.AreEqual(1, Data.Saves.Names.Count());
        }

        [Test]
        public void TestDelete()
        {
            Data.Saves.Delete("KspPersistentSfswMods");
            Assert.IsFalse(Directory.Exists(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\"));
        }

        [Test]
        public void TestRefresh()
        {
            var tempName = Guid.NewGuid().ToString();
            Directory.CreateDirectory(TestHelpers.BaseDataTempPath() + @"saves\" + tempName);
            File.Copy(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\persistent.sfs",
                TestHelpers.BaseDataTempPath() + @"saves\" + tempName + @"\persistent.sfs");
            Data.Saves.Refresh();

            Assert.IsTrue(Data.Saves.Names.Contains(tempName));
        }
    }
}
