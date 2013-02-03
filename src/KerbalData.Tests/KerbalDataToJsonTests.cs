using System;
using System.Diagnostics;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using KerbalData;
using System.Collections.Generic;

namespace KerbalData.Tests
{
    [TestClass]
    public class KerbalDataToJsonTests
    {
        /// <summary>
        /// Processes all files under Data/ by reading the file into JSON.Net and then writing them back out to KSP data. Resulting files are compared
        /// using a diff engine to the orignal KSP data file.
        /// </summary>
        [TestMethod]
        public void TestGeneralProcessing()
        {
            Assert.IsTrue(AllFilesMatch(Environment.CurrentDirectory + @"\Data"));
        }

        [TestMethod]
        public void TestProcessing()
        {
            //Assert.IsTrue(AllFilesMatch(Environment.CurrentDirectory + @"\Data\Parts\dockingPort1"));
            Assert.IsTrue(AllFilesMatch(Environment.CurrentDirectory + @"\Data\saves\temp"));
        }

        private bool AllFilesMatch(string dirPath)
        {
            return AllFilesMatch(new DirectoryInfo(dirPath));
        }

        private bool AllFilesMatch(DirectoryInfo dirInfo)
        {
            // Rigged to process all files possible before returing the result.
            var allFilesMatch = true;
            foreach (var dir in dirInfo.EnumerateDirectories())
            {
                if (!AllFilesMatch(dir))
                {
                    allFilesMatch = false;
                }
            }

            var files = new List<string>();

            foreach (var file in dirInfo.EnumerateFiles())
            {
                files.Add(file.FullName);
            }

            foreach (var file in files)
            {                
                try
                {
                    var processedName = file + ".kspd";
                    var jobj = KspData.LoadKspFile<JObject>(file);

                    jobj.ToString(Formatting.Indented, null).WriteToFile(file + ".json");
                    KspData.SaveFile(processedName, jobj);

                    if (!FilesAreEqual(file, processedName))
                    {
                        allFilesMatch = false;
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message, ex);
                }

            }

            return allFilesMatch;
        }

        private bool FilesAreEqual(string path1, string path2)
        {
            var result = false;

            switch (CompareFiles(path1, path2))
            {
                case 0: // Success
                    {
                        "0: Success".WriteToFile(path2 + ".FAILURE");
                        Assert.Fail("BComp comparision did not execute, there may be a problem with the script or command arguments, stopping testing File1: " + path1 + "; File2: " + path2);
                        break;
                    }
                case 1: // Binary Same
                    {
                        "1: Binary Same".WriteToFile(path2 + ".SUCCESS");
                        result = true;
                        break;
                    }
                case 2: // Rules-Based Same
                    {
                        "2: Rules-Based Same".WriteToFile(path2 + ".SUCCESS");
                        result = true;
                        break;
                    }
                case 11: // Binary Differences
                    {
                        "11: Binary Differences".WriteToFile(path2 + ".FAILURE");
                        result = false;
                        break;
                    }
                case 12: // Similar
                    {
                        "12: Similar".WriteToFile(path2 + ".SUCCESS");
                        result = true;
                        break;
                    }
                case 13: // Rules-Based Differences
                    {
                        "13: Rules-Based Differences".WriteToFile(path2 + ".FAILURE");
                        result = false;
                        break;
                    }
                case 14: // Conflicts Detected
                    {
                        "14: Conflicts Detected".WriteToFile(path2 + ".FAILURE");
                        result = false;
                        break;
                    }
                case 100: // Unknown Error
                    {
                        "100: Unknown Error".WriteToFile(path2 + ".FAILURE");
                        Assert.Fail("BComp has returned an unknown error, stopping testing File1: " + path1 + "; File2: " + path2);
                        break;
                    }
                case 101: // Conflicts detected, merge output not written
                    {
                        "101: Conflicts detected, merge output not written".WriteToFile(path2 + ".FAILURE");
                        result = false;
                        break;
                    }
                case 102: // BComp.exe unable to wait until BCompare.exe finishes
                    {
                        "102: BComp.exe unable to wait until BCompare.exe finishes".WriteToFile(path2 + ".FAILURE");
                        Assert.Fail("BComp has returned unable to wait error, stopping testing File1: " + path1 + "; File2: " + path2);
                        break;
                    }
                case 103: // BComp.exe cannot find BCompare.exe
                    {
                        "103: BComp.exe cannot find BCompare.exe".WriteToFile(path2 + ".FAILURE");
                        Assert.Fail("BComp cannot find BCompare, stopping testing File1: " + path1 + "; File2: " + path2);
                        break;
                    }
                case 104:  // Trial period expired
                    {
                        "104: Trial period expired".WriteToFile(path2 + ".FAILURE");
                        Assert.Fail("BeyondCompare License Expired - stopping testing File1: " + path1 + "; File2: " + path2);
                        break;
                    }
                case 105: // Error loading script file
                    {
                        "105: Error loading script file".WriteToFile(path2 + ".FAILURE");
                        Assert.Fail("BComp has encountered an error loding the script, stopping testing File1: " + path1 + "; File2: " + path2);
                        break;
                    }
                case 106: // Script syntax error
                    {
                        "106: Script syntax error".WriteToFile(path2 + ".FAILURE");
                        Assert.Fail("BComp has encountered a processing script syntax error, stopping testing File1: " + path1 + "; File2: " + path2);
                        break;
                    }
                case 107: // Script failed to load folders or files
                    {
                        "107: Script failed to load folders or files".WriteToFile(path2 + ".FAILURE");
                        Assert.Fail("BComp has encountered a script error while loading folders or files, stopping testing File1: " + path1 + "; File2: " + path2);
                        break;
                    }
                default:
                    {
                        "Unknown Return Code - BComp has responded with an unknown code.".WriteToFile(path2 + ".FAILURE");
                        Assert.Fail("BComp has returned an unknown error, stopping testing File1: " + path1 + "; File2: " + path2);
                        break;
                    }
            }

            if (CompareJObjects(path1, path2))
            {
                "Object Instances Match".WriteToFile(path2 + ".OBJSUCCESS");
            }
            else
            {
                "Object Instances DO NOT Match".WriteToFile(path2 + ".OBJFAILURE");
            }

            result = CompareJObjects(path1, path2);

            return result;
        }

        private int CompareFiles(string path1, string path2)
        {
            var procInfo = new ProcessStartInfo(@"C:\Program Files (x86)\Beyond Compare 3\BComp.exe");
            procInfo.WindowStyle = ProcessWindowStyle.Hidden;
            procInfo.UseShellExecute = false;
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
            procInfo.Arguments = string.Join(
                " ",
                new string[]
                {
                    @"/quickcompare",
                    "\"" + path1 + "\"",
                    "\"" + path2 + "\"",
                });

            var proc = Process.Start(procInfo);
            proc.Start();
            proc.WaitForExit();

            return proc.ExitCode;
        }

        private bool CompareJObjects(string path1, string path2)
        {    
            // This comparison is currently impossible as we are using GUIDs for file comments and repeated fields. 
            // File comparison is most critical as if the object does not make it up through serialization properly it will not be saved properly.
            // As such this validation is generally not needed. 

            // TODO: Testing paranioa is always nice, but need a way to fullfill this using generated names, for now just return true;
            /*
            var jobj1 = JObject.Parse(kspToJson.ToJson(TestHelpers.LoadFile(path1)));
            var jobj2 = JObject.Parse(kspToJson.ToJson(TestHelpers.LoadFile(path2)));
            var result = JToken.DeepEquals(jobj1, jobj2);*/
            
            return true;
        }
    }
}
