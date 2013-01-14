// -----------------------------------------------------------------------
// <copyright file="KspFileConverter.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Draft implmentation of 0.18.x file text converter. This class handles the direct conversion of the KSP data file into the provided KSPDataContext. The context will be used to convert the data into other
    /// desired formats (initially JSON)
    /// </summary>
    public class KspFileConverter : IKspFileConverter
    {
        /// <summary>
        /// Parses the provided stream and populates the provided context with the de-serialized data.
        /// </summary>
        /// <param name="stream">data stream to use</param>
        /// <param name="context">data context to populate</param>
        public void Parse(StreamReader stream, KspDataContext context)
        {
            var lineList = stream.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ParseData(lineList, context.Data);
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
                if (!currentLine.Contains("=") && !currentLine.Contains("{") && !currentLine.Contains("}"))
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
                else if (currentLine.Contains("=")) // If the line is a value assignment (in ksp files "=" is only value assignment for fields. Child object names act as the field name for complex objects. 
                {
                    var comma = string.Empty;
                    if ((!(i + 2 > data.Count) && nextLine.Contains("=")) || (!nextLine.Contains("=") && !string.IsNullOrEmpty(nextLine)))
                    {
                        comma = ",";
                    }

                    var lineParts = currentLine.Split('=');
                    var leftSide = lineParts[0].Trim();
                    var rightSide = lineParts.Count() > 1 ? lineParts[1].Trim() : string.Empty;

                    // Checks for array strings and excludes special fields like "CDATA"
                    // TODO: Other types of filters to have?
                    // TODO: Better storage for complex strings with special values (perhaps URLencoding?)
                    if (rightSide.Contains(",") && !leftSide.ToUpper().Equals("CDATA"))
                    {
                        var rightValues = rightSide.Split(',');

                        var valueBuilder = new StringBuilder();
                        valueBuilder.Append("[");

                        for (var j = 0; j < rightValues.Count(); j++)
                        {
                            valueBuilder.Append(
                                "\"" + rightValues[j].Trim().Replace("'", "`").Replace("\"", "'") + "\""
                                + (j != rightValues.Count() - 1 ? "," : string.Empty));
                        }

                        valueBuilder.Append("]");

                        rightSide = valueBuilder.ToString();
                    }
                    else
                    {
                        // Converting quotes to not clash with how JSON stores quote
                        // TODO: better handling here, see previous note. 
                        rightSide = rightSide != string.Empty ? "\"" + rightSide.Trim().Replace("'", "`").Replace("\"", "'") + "\"" : "\"\"";
                    }

                    dataObj.Values.Add(new KspDataField { Name = leftSide, Value = rightSide });
                }
            }
        }
    }
}
