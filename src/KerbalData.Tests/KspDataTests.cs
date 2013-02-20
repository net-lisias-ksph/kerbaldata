// -----------------------------------------------------------------------
// <copyright file="KspDataTests.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Models;

    [TestClass]
    public class KspDataTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KspDataTests" /> class.
        /// </summary>	
        public KspDataTests()
        {
        }

        [TestMethod]
        [DeploymentItem(@"Data\Saves\KspPersistentSfswMods.sfs")]
        public void SaveFileLoadTest()
        {
            var save = 
                KspData.LoadKspFile<SaveFile>(
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) 
                + @"\KspPersistentSfswMods.sfs");

            // Just a simple test to see if we can load a game
            Assert.IsNotNull(save);
            Assert.IsNotNull(save.DisplayName);
            Assert.IsNotNull(save.Game);
            Assert.IsNotNull(save.Game.FlightState);
            Assert.IsNotNull(save.Game.FlightState.Vessels);
            Assert.AreEqual(180, save.Game.FlightState.Vessels.Count);
        }


        [TestMethod]
        [DeploymentItem(@"Data\Parts\mumech_MechJeb\part.cfg")]
        public void PartFileLoadTest()
        {
            var part =
                KspData.LoadKspFile<PartFile>(
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                + @"\part.cfg");

            // Just a simple test to see if we can load a game
            Assert.IsNotNull(part);
            Assert.IsNotNull(part.DisplayName);
        }


        [TestMethod]
        [DeploymentItem(@"Data\Ships\Custom\VAB\MultiSatMk1.craft")]
        public void CraftFileLoadTest()
        {
            var craft =
                KspData.LoadKspFile<CraftFile>(
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                + @"\MultiSatMk1.craft");

            // Just a simple test to see if we can load a game
            Assert.IsNotNull(craft);
            Assert.IsNotNull(craft.DisplayName);
        }

        [TestMethod]
        [DeploymentItem(@"Data\Config\settings.cfg")]
        public void ConfigFileLoadTest()
        {
            var config =
                KspData.LoadKspFile<ConfigFile>(
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                + @"\MultiSatMk1.craft");

            // Just a simple test to see if we can load a game
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.DisplayName);
        }



    }
}
