// -----------------------------------------------------------------------
// <copyright file="ManagerTests.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------


// TODO: Real testing
// I have never been a fan of TDD however do see value in proper unit testing though I tend to evolve them on a case-by-case basis. These tests are by no means proper unit testing, rather they are simple functional tests. 
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
    public class ManagerTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerTests" /> class.
        /// </summary>	
        public ManagerTests()
        {
        }

        [TestMethod]
        public void GeneralTests()
        {
            var kdm = new KerbalDataManager();
            var gdm = kdm.Games["testing"];

            var vessels = gdm.Vessels;

            var count = vessels.RefuelCraft();

            gdm.SaveData();
            
        }
    }
}
