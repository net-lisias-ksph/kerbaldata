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
            /*
            var kd = new KerbalData(@"C:\games\KSP_win");
            var sf = kd.Saves["testing"];

            sf.Game.FlightState.FillResources();
            sf.Game.Title = "I AM IRON MAN";
            sf.Game.FlightState.ClearDebris();

            var sat = sf.Game.FlightState.Vessels.Where(v => v.Name.Contains("Beta Geo-Sat")).FirstOrDefault();
            sat.Orbit.Change(Body.Bop);

            sf.Save();

            sf.Game.Title = "WHAT WHO WHAT?";
            sf.Game.FlightState.Vessels[0].Parts[0]["name"] = "SUPERCOREWOOO!";

            sf.Save();*/
        }
    }
}
