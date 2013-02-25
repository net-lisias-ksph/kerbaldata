// -----------------------------------------------------------------------
// <copyright file="FlightStateTests.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Tests.SaveGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;
    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    [TestFixture]
    public class FlightStateTests : KerbalDataBaseTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlightStateTests" /> class.
        /// </summary>	
        public FlightStateTests()
        {
        }

        [Test]
        public void TestFillResources()
        {
            var save = Data.Saves["KspPersistentSfswMods"];
            save.Game.FlightState.FillResources();

            foreach (var resource in save.Game.FlightState.Vessels.SelectMany(vessel => vessel.Parts.Where(part => part.Resources != null).SelectMany(part => part.Resources)))
            {
                Assert.AreEqual(resource.MaxAmount, resource.Amount);
            }
        }

        [Test]
        public void TestEmptyResources()
        {
            var save = Data.Saves["KspPersistentSfswMods"];
            save.Game.FlightState.EmptyResources();

            foreach (var resource in save.Game.FlightState.Vessels.SelectMany(vessel => vessel.Parts.Where(part => part.Resources != null).SelectMany(part => part.Resources)))
            {
                Assert.AreEqual(0, resource.Amount);
            }
        }

        [Test]
        public void TestClearDebris()
        {
            var save = Data.Saves["KspPersistentSfswMods"];
            var count = save.Game.FlightState.Vessels.Count;
            save.Game.FlightState.ClearDebris();
            Assert.IsTrue(count > save.Game.FlightState.Vessels.Count);
        }
    }
}
