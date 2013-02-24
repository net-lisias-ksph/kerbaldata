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
    public class KerbalDataTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataTests" /> class.
        /// </summary>	
        public KerbalDataTests()
        {
        }

        [Test]
        public void TestCreation()
        {
            var kd = KerbalData.Create(TestHelpers.BaseDataPath());

            Assert.IsNotNull(kd);
            Assert.IsNotNull(kd.Saves);
            Assert.AreEqual(2, kd.Saves.Count);
            Assert.IsNotNull(kd.Saves["KspPersistentSfswMods"]);

            Assert.IsNotNull(kd.Scenarios);
            Assert.AreEqual(4, kd.Scenarios.Count);
            Assert.IsNotNull(kd.Scenarios["Impending Impact"]);

            Assert.IsNotNull(kd.TrainingScenarios);
            Assert.AreEqual(4, kd.TrainingScenarios.Count);
            Assert.IsNotNull(kd.TrainingScenarios["C_Orbit101"]);

            Assert.IsNotNull(kd.CraftInSph);
            Assert.AreEqual(20, kd.CraftInSph.Count);
            Assert.IsNotNull(kd.CraftInSph["Hopper Mk1"]);

            Assert.IsNotNull(kd.CraftInVab);
            Assert.AreEqual(19, kd.CraftInVab.Count); // There are 176 part folders but only 175 .cfg files.
            Assert.IsNotNull(kd.CraftInVab["Mun Sat Const Mk1.5"]);

            Assert.IsNotNull(kd.Parts);
            Assert.AreEqual(175, kd.Parts.Count);
            Assert.IsNotNull(kd.Parts["EngineerChip"]);

            Assert.IsNotNull(kd.KspSettings);
            Assert.AreEqual(1, kd.KspSettings.Count);
            Assert.IsNotNull(kd.KspSettings["settings"]);

        }
    }
}
