// -----------------------------------------------------------------------
// <copyright file="LinqTest.cs" company="OpenSauceSolutions">
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

    using KerbalData;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    [TestClass]
    public class LinqTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinqTest" /> class.
        /// </summary>	
        public LinqTest()
        {
        }

        [TestMethod]
        public void TestGameObject()
        {
            var sf = SaveFile.CreateFromKspFile(@"Data\Saves\KspPersistentSfswMods.sfs");

            sf.Game.FlightState.FillResources();
            sf.Game.Title = "HELLO WORLD!";
            sf.Game.FlightState.ClearDebris();

            var sat = sf.Game.FlightState.Vessels.Where(v => v["name"].ToString().Contains("Beta Geo-Sat")).FirstOrDefault();
            sat.Orbit.Change(Body.Eeloo);

            sf.Save();
        }
    }
}
