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
            //var obj = KspData.LoadKspFile(@"C:\dev\KspData\src\KerbalData.Tests\bin\Debug\Data\Parts\dockingPort1\part.cfg");


            //var vessel = obj["GAME"]["FLIGHTSTATE"]["VESSEL"].Where(v => v["pid"].ToString().Equals("f55d3f0601704035aa06e0a8174b723c")).FirstOrDefault();
            
            //var parts = vessel["PART"].Where(p => p["uid"].ToString().Equals("2910085908"));

            //Console.WriteLine();
            
            
            var kd = KerbalData.Create(@"C:\games\KSP_win_test");
            //var training = kd.TrainingScenarios["C_Orbit101"];
            //var scenario = kd.Scenarios["Impending Impact"];
            var save = kd.Saves["testing"];
            //var craft = kd.Saves["testing"].CraftInSph["Goose Mk1_4"];
            //var part = kd.Parts["fuelTank2-2"];
            //var part2 = kd.Parts["liquidEngine1"];
            //var craft2 = kd.CraftInVab["MechJeb Pod 2.0 Kit"];
            //var settings = kd.KspSettings["settings"];

            //part.Cost = 43434343;
            //part.Save();
            Console.WriteLine();
            /*
            var sf = kd.Saves["testing"];

            
            sf.Game.Title = "TESTINGTESTINGTESTINGTESTING";
            sf.Game.FlightState.ClearDebris();

            var sat = sf.Game.FlightState.Vessels.Where(v => v.Name.Contains("Beta Geo-Sat")).FirstOrDefault();
            sat.Orbit.Change(Body.Kerbol);

            sf.Save();*/

            /*
            sf.Game.Title = "WHAT WHO WHAT?";
            sf.Game.FlightState.Vessels[0].Parts[0]["name"] = "SUPERCOREWOOO!";

            sf.Save();*/
        }
    }
}
