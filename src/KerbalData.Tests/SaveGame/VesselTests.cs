// -----------------------------------------------------------------------
// <copyright file="VesselTests.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Tests.SaveGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Models;
    using NUnit.Framework;
    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    [TestFixture]
    public class VesselTests : KerbalDataBaseTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VesselTests" /> class.
        /// </summary>	
        public VesselTests()
        {
        }

        [Test]
        public void ChangeOrbitTest()
        {
            var save = Data.Saves["KspPersistentSfswMods"];
            var vessel = save.Game.FlightState.Vessels[0];

            var startSma = vessel.Orbit.Sma;
            vessel.Orbit.Change(Body.Kerbol);

            Assert.IsTrue(startSma < vessel.Orbit.Sma);
        }
    }
}
