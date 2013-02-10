// -----------------------------------------------------------------------
// <copyright file="DataSerializer.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serialization.Serializers.V018x
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Serialization;

    using Serialization;

    /// <summary>
    /// Serializer implmentation tested to support 0.18-0.18.2 KSP data formats
    /// </summary>
    public class DataSerializer : IKspSerializer
    {
        private readonly List<string> versions = 
            new List<string>() 
            { 
                "0.18", 
                "0.18.0", 
                "0.18.1", 
                "0.18.2" 
            };

        /// <summary>
        /// Gets a flag indicating if this type can serialize data
        /// </summary>
        public bool CanSerialize
        {
            get 
            { 
                return true;
            }
        }

        /// <summary>
        /// Gets a flag indicating if this type can de-serialize data
        /// </summary>
        public bool CanDeSerialize
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a list of supported KSP versions
        /// </summary>
        public IList<string> Versions
        {
            get
            {
                return versions;
            }
        }

        /// <summary>
        /// Checks if this type supports a particular version
        /// </summary>
        /// <param name="version">version to check</param>
        /// <returns>true=supported;false=not supported</returns>
        public bool SupportsVersion(string version)
        {
            return versions.Contains(version.Trim());
        }

        /// <summary>
        /// Serializes data 
        /// </summary>
        /// <param name="context">context to serialize data from</param>
        /// <param name="writer">stream writer to serialize to</param>
        public void Serialize(KspDataContext context, StreamWriter writer)
        {
            BuildKspData(context.Data, writer);
        }

        /// <summary>
        /// De-Serializes data
        /// </summary>
        /// <param name="context">context to de-serialize to</param>
        /// <param name="reader">stream reader to read from</param>
        public void DeSerialize(KspDataContext context, StreamReader reader)
        {
            var lineList = reader.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ParseData(lineList, context.Data);
        }

        /// <summary>
        /// Primary internal method to build the KSP data string. Contains version specific rules and formatting
        /// </summary>
        /// <param name="jobj">JSon Object data to use.</param>
        /// <param name="tabCount">starting point for indented tabs (method will furhter indent children through recursive calls</param>
        /// <param name="objectName">name of data object for header</param>
        /// <returns>KSP data string</returns>
        private static void BuildKspData(KspDataObject dataObj, StreamWriter writer, int tabCount = 0)
        {
            var tabIndent = GetTabs(tabCount);

            // Write out any property values
            foreach (var prop in dataObj.Values)
            {
                var key = prop.Name;

                if (key.Contains("KCOM-"))
                {
                    writer.WriteLine("// " + prop.Value.ToString().Trim());
                }
                else
                {
                    writer.WriteLine(tabIndent + key + " = " + prop.Value.ToString().Trim());
                }
            }

            // Write out any object children using recursive calls. 
            foreach (var obj in dataObj.Children)
            {
                writer.WriteLine(tabIndent + obj.Name);
                writer.WriteLine(tabIndent + '{' + Environment.NewLine );

                BuildKspData(obj, writer, tabCount + 1);

                writer.WriteLine(Environment.NewLine + tabIndent + '}');
            }
        }

        /// <summary>
        /// Internal parsing method, recursviely calls itself as needed
        /// </summary>
        /// <param name="data">data to parse</param>
        /// <param name="dataObj">data object to populate</param>
        private static void ParseData(List<string> data, KspDataObject dataObj)
        {
            for (var i = 0; i < data.Count; i++)
            {
                // I came across a type of reader that does this for me, need to test it out. For now this works well. 
                var lastLine = i > 0 ? data[i - 1] : string.Empty;
                var currentLine = data[i];
                var nextLine = i + 1 < data.Count ? data[i + 1] : string.Empty;

                // Check if the line is a header line. All lines in a KSP data file contain one of the characters below
                // with the exception of the header name for a new object. 
                if (currentLine.Trim().StartsWith("//"))
                {
                    dataObj.Values.Add(new KspDataField { Name = "KCOM-" + Guid.NewGuid().ToString(), Value = currentLine.Replace("// ", "") });
                }
                else if (!currentLine.Contains('=') && !currentLine.Contains('{') && !currentLine.Contains('}') && !String.IsNullOrEmpty(currentLine.Trim()))
                {
                    var objectName = currentLine.Trim();
                    var objectData = new List<string>();

                    // Tracking value to find the end of an object. 
                    var depth = 0;

                    // Loop through the data looking for the end of the object
                    for (var x = i + 2; x < data.Count - 1; x++)
                    {
                        if (data[x].Trim().Equals("}"))
                        {
                            if (depth == 0)
                            {
                                break;
                            }

                            depth--;
                        }
                        else if (data[x].Trim().Equals("{"))
                        {
                            depth++;
                        }

                        objectData.Add(data[x]);
                    }

                    // Push the primay loop counter up to account for the object data we just pulled out
                    i += objectData.Count + 2;

                    if (objectData.Count > 0)
                    {
                        var child = new KspDataObject { Name = objectName };

                        // Recursive call to de-serialize child
                        ParseData(objectData, child);
                        dataObj.Children.Add(child);
                    }
                    else
                    {
                        // In this case we have an object with no children and no properrties. 
                        // This is another thing we do to try and remain compatble. There are a number of empty objects that are stored in the 
                        // KSP data file. To remain as close to 100% we even track these empty objects to make sure they are shown to the consumer and 
                        // written back on save. 
                        dataObj.Children.Add(new KspDataObject { Name = objectName });
                    }
                }
                else if (currentLine.Contains('=')) // If the line is a value assignment (in ksp files "=" is only value assignment for fields. Child object names act as the field name for complex objects. 
                {
                    var comma = string.Empty;
                    if ((!(i + 2 > data.Count) && nextLine.Contains('=')) || (!nextLine.Contains('=') && !string.IsNullOrEmpty(nextLine)))
                    {
                        comma = ",";
                    }

                    var lineParts = currentLine.Split('=');
                    var leftSide = lineParts[0].Trim();
                    var rightSide = lineParts.Count() > 1 ? lineParts[1].Trim() : string.Empty;

                    dataObj.Values.Add(new KspDataField { Name = leftSide, Value = rightSide });
                }
            }
        }

        /// <summary>
        /// Creates a tab substring 
        /// </summary>
        /// <param name="count">number of consecutive tabs to add</param>
        /// <returns>tab string</returns>
        private static string GetTabs(int count)
        {
            var tabSpacer = string.Empty;
            for (var x = 0; x < count; x++)
            {
                tabSpacer += '\t';
            }

            return tabSpacer;
        }
    }
}
