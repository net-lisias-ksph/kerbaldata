// -----------------------------------------------------------------------
// <copyright file="KspDataTests.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Tests
{
    using System;
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
    }
}
