// -----------------------------------------------------------------------
// <copyright file="TestHelpers.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Tests
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public static class TestHelpers
    {
        private static string baseTestPath;

        public static void WriteToConsole(Exception ex)
        {
            Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
        }

        public static string LoadFile(string filePath)
        {
            using (var file = File.OpenText(filePath))
            {
                return file.ReadToEnd();
            }
        }

        public static string BaseTestPath()
        {
            if (string.IsNullOrEmpty(baseTestPath))
            {
                baseTestPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            }

            return baseTestPath;
        }

        public static string BaseDataPath()
        {
            return BaseTestPath() + @"\Data\";
        }

        public static string BaseDataTempPath()
        {
            return BaseTestPath() + @"\TestData\";
        }

    }

    public static class TestExtensions
    {
        /// <summary>
        /// Writes provided content to file, returns provided contennt string unchanged to allow simple chaining.
        /// </summary>
        /// <param name="str">location of the file reletaive to the current working directory</param>
        /// <param name="content">content you with to write to the file</param>
        /// <returns>value provided for content parameter</returns>
        public static string WriteToFile(this string str, string filePath)
        {
            using (var file = File.CreateText(filePath.Trim()))
            {
                file.Write(str);
                file.Flush();
            } 

            return str;
        }
    }
}
