// -----------------------------------------------------------------------
// <copyright file="KerbalDataTests.cs" company="OpenSauceSolutions">
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

    [TestFixture]
    public class KerbalDataTests : KerbalDataBaseTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataTests" /> class.
        /// </summary>	
        public KerbalDataTests()
        {
        }

        [Test]
        public void InstanceCreationTest()
        {
            Assert.IsNotNull(Data);
            Assert.IsNotNull(Data.Saves);
            Assert.AreEqual(2, Data.Saves.Count);
            Assert.IsNotNull(Data.Saves["KspPersistentSfswMods"]);

            Assert.IsNotNull(Data.Scenarios);
            Assert.AreEqual(4, Data.Scenarios.Count);
            Assert.IsNotNull(Data.Scenarios["Impending Impact"]);

            Assert.IsNotNull(Data.TrainingScenarios);
            Assert.AreEqual(4, Data.TrainingScenarios.Count);
            Assert.IsNotNull(Data.TrainingScenarios["C_Orbit101"]);

            Assert.IsNotNull(Data.CraftInSph);
            Assert.AreEqual(20, Data.CraftInSph.Count);
            Assert.IsNotNull(Data.CraftInSph["Hopper Mk1"]);

            Assert.IsNotNull(Data.CraftInVab);
            Assert.AreEqual(19, Data.CraftInVab.Count); // There are 176 part folders but only 175 .cfg files.
            Assert.IsNotNull(Data.CraftInVab["Mun Sat Const Mk1.5"]);

            Assert.IsNotNull(Data.Parts);
            Assert.AreEqual(175, Data.Parts.Count);
            Assert.IsNotNull(Data.Parts["EngineerChip"]);

            Assert.IsNotNull(Data.KspSettings);
            Assert.AreEqual(1, Data.KspSettings.Count);
            Assert.IsNotNull(Data.KspSettings["settings"]);
        }

        [Test]
        public void SaveChangeTest()
        {
            Assert.IsNotNull(Data);

            var part = Data.Parts["solarPanels4"];
            part.Author = "HELLO WORLD!";

            part.Save();

            var kd = KerbalData.Create(TestHelpers.BaseDataTempPath());
            var changedPart = kd.Parts["solarPanels4"];
            Assert.AreEqual("HELLO WORLD!", changedPart.Author);
        }

        [Test]
        public void CopyTest()
        {
            var craft = Data.CraftInSph["Rocket-power VTOL"];
            craft.Save("Rocket-power VTOL Copy");

            var newCraft = Data.CraftInSph["Rocket-power VTOL Copy"];
            Assert.IsNotNull(newCraft);
            Assert.AreEqual(craft.Parts.Count, newCraft.Parts.Count);

            var kd = KerbalData.Create(TestHelpers.BaseDataTempPath());
            var newCraftExt = kd.CraftInSph["Rocket-power VTOL Copy"];
            Assert.AreEqual(craft.Parts.Count, newCraftExt.Parts.Count);
        }
    }
}
