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
    /// TODO: Class Summary
    /// </summary>
    public class KspFileConverter : IKspFileConverter
    {
        public void Parse(StreamReader stream, KspDataContext context)
        {
            var lineList = stream.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ParseData(lineList, context.Data);
        }

        private static void ParseData(List<string> data, KspDataObject dataObj)
        {
            for (var i = 0; i < data.Count; i++)
            {
                var lastLine = i > 0 ? data[i - 1] : string.Empty;
                var currentLine = data[i];
                var nextLine = i + 1 < data.Count ? data[i + 1] : string.Empty;

                if (!currentLine.Contains("=") && !currentLine.Contains("{") && !currentLine.Contains("}"))
                {
                    var objectName = currentLine.Trim();
                    var objectData = new List<string>();
                    var depth = 0;

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

                    i += objectData.Count + 2;

                    if (objectData.Count > 0)
                    {
                        var child = new KspDataObject { Name = objectName }; 
                        ParseData(objectData, child);
                        dataObj.Children.Add(child);
                    }
                    else
                    {
                        dataObj.Children.Add(new KspDataObject { Name = objectName });
                    }
                }
                else if (currentLine.Contains("="))
                {
                    var comma = string.Empty;
                    if ((!(i + 2 > data.Count) && nextLine.Contains("=")) || (!nextLine.Contains("=") && !string.IsNullOrEmpty(nextLine)))
                    {
                        comma = ",";
                    }

                    var lineParts = currentLine.Split('=');
                    var leftSide = lineParts[0].Trim();
                    var rightSide = lineParts.Count() > 1 ? lineParts[1].Trim() : string.Empty;

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
                        rightSide = rightSide != string.Empty ? "\"" + rightSide.Trim().Replace("'", "`").Replace("\"", "'") + "\"" : "\"\"";
                    }

                    dataObj.Values.Add(new KspDataField { Name = leftSide, Value = rightSide });
                }
            }
        }
    }
}
