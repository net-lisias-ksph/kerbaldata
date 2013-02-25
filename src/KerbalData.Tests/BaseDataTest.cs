// -----------------------------------------------------------------------
// <copyright file="BaseDataTest.cs" company="OpenSauceSolutions">
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
    /// TODO: Class Summary
    /// </summary>
    public class BaseDataTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataTest" /> class.
        /// </summary>	
        public BaseDataTest()
        {
        }

        [SetUp]
        protected virtual void TestInit()
        {
            ResetTempData();
        }

        protected void ResetTempData()
        {
            if (Directory.Exists(TestHelpers.BaseDataTempPath()))
            {
                //Delete Temp   
                var downloadedMessageInfo = new DirectoryInfo(TestHelpers.BaseDataTempPath());

                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            else
            {
                Directory.CreateDirectory(TestHelpers.BaseDataTempPath());
            }

            // Copy Original data
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(TestHelpers.BaseDataPath(), "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(TestHelpers.BaseDataPath(), TestHelpers.BaseDataTempPath()));

            //Copy all the files
            foreach (string newPath in Directory.GetFiles(TestHelpers.BaseDataPath(), "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(TestHelpers.BaseDataPath(), TestHelpers.BaseDataTempPath()));

        }

    }
}
