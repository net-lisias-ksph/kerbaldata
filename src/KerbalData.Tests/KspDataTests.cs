// -----------------------------------------------------------------------
// <copyright file="KspDataTests.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Tests
{
    using System;
    using System.IO;
    using System.Reflection;
    using NUnit.Framework;

    using Models;

    [TestFixture]
    public class KspDataTests : BaseDataTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KspDataTests" /> class.
        /// </summary>	
        public KspDataTests()
        {
        }

        [Test]
        public void SaveFileLoadTest()
        {
            var save =
                KspData.LoadKspFile<SaveFile>(TestHelpers.BaseDataPath() + @"saves\KspPersistentSfswMods\persistent.sfs");

            // Just a simple test to see if we can load a game
            Assert.IsNotNull(save);
            Assert.IsNotNull(save.DisplayName);
            Assert.IsNotNull(save.Game);
            Assert.IsNotNull(save.Game.FlightState);
            Assert.IsNotNull(save.Game.FlightState.Vessels);
            Assert.AreEqual(180, save.Game.FlightState.Vessels.Count);
        }


        [Test]
        public void PartFileLoadTest()
        {
            var part =
                KspData.LoadKspFile<PartFile>(TestHelpers.BaseDataPath() + @"Parts\mumech_MechJeb\part.cfg");

            // Just a simple test to see if we can load a game
            Assert.IsNotNull(part);
            Assert.IsNotNull(part.DisplayName);
        }


        [Test]
        public void CraftFileLoadTest()
        {
            var craft = KspData.LoadKspFile<CraftFile>(TestHelpers.BaseDataPath() + @"Ships\VAB\MultiSatMk1.craft");

            // Just a simple test to see if we can load a game
            Assert.IsNotNull(craft);
            Assert.IsNotNull(craft.DisplayName);
        }

        [Test]
        public void ConfigFileLoadTest()
        {
            var config =
                KspData.LoadKspFile<ConfigFile>(TestHelpers.BaseDataPath() + @"settings.cfg");

            // Just a simple test to see if we can load a game
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.DisplayName);
        }

        [Test]
        public void DataTypeConvertTest()
        {
            var data = (new StreamReader(File.Open(TestHelpers.BaseDataPath() + @"Ships\VAB\MultiSatMk1.craft", FileMode.Open))).ReadToEnd();

            var convertedCraft = KspData.Convert(data, ProcessorRegistry.Create().Create<CraftFile>()); // Bit ugly to test this use case, would never be this way in the real world as this is what happens internally
            var convertedCraft2 = KspData.Convert<CraftFile>(data);
            var craft = KspData.LoadKspFile<CraftFile>(TestHelpers.BaseDataPath() + @"Ships\VAB\MultiSatMk1.craft");

            Assert.NotNull(convertedCraft);
            Assert.NotNull(convertedCraft2);
            Assert.AreEqual(craft.Parts.Count, convertedCraft.Parts.Count);
            Assert.AreEqual(craft.Parts.Count, convertedCraft2.Parts.Count);
        }
    }
}
