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
        public void TestLoad()
        {
            var tempName = Guid.NewGuid().ToString();
            Directory.CreateDirectory(TestHelpers.BaseDataTempPath() + @"saves\" + tempName);
            File.Copy(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\persistent.sfs",
                TestHelpers.BaseDataTempPath() + @"saves\" + tempName + @"\persistent.sfs");

            Assert.IsFalse(Data.Saves.ContainsId(tempName));
            Assert.IsFalse(Data.Saves.Names.Contains(tempName));

            var save = Data.Saves[tempName];

            Assert.IsNotNull(save);

            Assert.AreEqual(Data.Saves["KspPersistentSfswMods"].Game.Title, save.Game.Title);
            Assert.AreEqual(Data.Saves["KspPersistentSfswMods"].Game.FlightState.Vessels.Count, save.Game.FlightState.Vessels.Count);
            Assert.IsTrue(Data.Saves.ContainsId(tempName));
            Assert.IsTrue(Data.Saves.Names.Contains(tempName));
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
            Assert.AreEqual(save.Game.FlightState.Vessels.Count, savedSave01.Game.FlightState.Vessels.Count);

            Data.Saves["Test-Save-002"] = save;
            save.Save();

            var savedSave02 = KspData.LoadKspFile<SaveFile>(TestHelpers.BaseDataTempPath() + @"saves\Test-Save-002\persistent.sfs");
            Assert.IsNotNull(savedSave02);
            Assert.AreEqual(save.Game.FlightState.Vessels.Count, savedSave02.Game.FlightState.Vessels.Count);

            Data.Saves.Add((object)save, "Test-Save-003");
            Data.Saves["Test-Save-003"].Save();

            var savedSave03 = KspData.LoadKspFile<SaveFile>(TestHelpers.BaseDataTempPath() + @"saves\Test-Save-003\persistent.sfs");
            Assert.IsNotNull(savedSave03);
            Assert.AreEqual(save.Game.FlightState.Vessels.Count, savedSave03.Game.FlightState.Vessels.Count);
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

            Data.Saves.Delete(Data.Saves["freshgame"]);
            Assert.IsFalse(Directory.Exists(TestHelpers.BaseDataTempPath() + @"saves\freshgame\"));
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

        [Test]
        public void TestUpdate()
        {
            var freshgame = KspData.LoadKspFile<SaveFile>(TestHelpers.BaseDataTempPath() + @"saves\freshgame\persistent.sfs");

            Data.Saves["KspPersistentSfswMods"] = freshgame;
            Data.Saves["KspPersistentSfswMods"].Save();

            var newFreshGame = KspData.LoadKspFile<SaveFile>(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\persistent.sfs");

            Assert.AreEqual(freshgame.Game.Title, newFreshGame.Game.Title);
            Assert.AreEqual(freshgame.Game.FlightState, newFreshGame.Game.FlightState);
        }

        [Test]
        public void TestSaveAll()
        {
            var description = "HELLO WORLD!";
            Data.Saves["KspPersistentSfswMods"].Game.Description = description;
            Data.Saves["freshgame"].Game.Description = description;
            Data.Saves.Save();

            var save1 = KspData.LoadKspFile<SaveFile>(TestHelpers.BaseDataTempPath() + @"saves\KspPersistentSfswMods\persistent.sfs");
            var save2 = KspData.LoadKspFile<SaveFile>(TestHelpers.BaseDataTempPath() + @"saves\freshgame\persistent.sfs");

            Assert.AreEqual(description, save1.Game.Description);
            Assert.AreEqual(description, save2.Game.Description);
        }

        [Test]
        public void TestContainsId()
        {
            Assert.IsTrue(Data.Saves.ContainsId("KspPersistentSfswMods"));
            Assert.IsFalse(Data.Saves.ContainsId(Guid.NewGuid().ToString()));

            Assert.IsTrue(Data.Scenarios.ContainsId("Space Station 1"));
            Assert.IsFalse(Data.Scenarios.ContainsId(Guid.NewGuid().ToString()));

            Assert.IsTrue(Data.TrainingScenarios.ContainsId("B_flightBasics"));
            Assert.IsFalse(Data.TrainingScenarios.ContainsId(Guid.NewGuid().ToString()));

            Assert.IsTrue(Data.CraftInSph.ContainsId("Solar Cruiser Mk1"));
            Assert.IsFalse(Data.CraftInSph.ContainsId(Guid.NewGuid().ToString()));

            Assert.IsTrue(Data.CraftInVab.ContainsId("Two-Stage Lander"));
            Assert.IsFalse(Data.CraftInVab.ContainsId(Guid.NewGuid().ToString()));

            Assert.IsTrue(Data.Parts.ContainsId("Ailerons"));
            Assert.IsFalse(Data.Parts.ContainsId(Guid.NewGuid().ToString()));

            Assert.IsTrue(Data.KspSettings.ContainsId("settings"));
            Assert.IsFalse(Data.KspSettings.ContainsId(Guid.NewGuid().ToString()));

        }
    }
}
