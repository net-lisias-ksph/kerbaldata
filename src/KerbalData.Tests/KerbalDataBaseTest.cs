// -----------------------------------------------------------------------
// <copyright file="KerbalDataBaseTest.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    /// <summary>
    /// Base test class for all tests of KerbalData
    /// </summary>
    public abstract class KerbalDataBaseTest : BaseDataTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataBaseTest" /> class.
        /// </summary>	
        protected KerbalDataBaseTest()
        {
        }

        [SetUp]
        protected override void TestInit()
        {
            Data = null;
            ResetTempData();
            Data = KerbalData.Create(TestHelpers.BaseDataTempPath());
        }

        protected KerbalData Data { get; set; }


    }
}
