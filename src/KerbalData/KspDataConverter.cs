// -----------------------------------------------------------------------
// <copyright file="KspDataConverter.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public class KspDataConverter : IKspDataConverter
    {

        public string BuildJson(KspDataContext context)
        {
            return "{" + BuildJson(context.Data) + "}";
        }

        public string BuildKsp(string json)
        {
            JObject jobj;
            try
            {
                jobj = JObject.Parse(json);
            }
            catch (Exception ex)
            {
                throw new KerbalDataException("Unable to parse provided string as a JSON string, see the inner exception for details", ex);
            }

            var data = BuildKspData(jobj);
            return data;
        }

        private static string BuildKspData(JObject jobj, int tabCount = 0, string objectName = null)
        {
            var tabIndent = GetTabs(tabCount);
            var sb = new StringBuilder();

            foreach (var obj in jobj)
            {
                switch (obj.Value.Type)
                {
                    case JTokenType.String:
                        {
                            sb.AppendLine(
                                tabIndent
                                + (obj.Key.Contains("-KSPD-MULTI-")
                                    ? obj.Key.Substring(0, obj.Key.IndexOf("-KSPD-MULTI-"))
                                    : obj.Key)
                                + " = " + obj.Value.ToString().Replace("'", "\"").Replace("`", "'"));
                            break;
                        }

                    case JTokenType.Object:
                        {
                            sb.AppendLine(objectName == null ? tabIndent + obj.Key : tabIndent + objectName);
                            sb.AppendLine(tabIndent + "{" + Environment.NewLine + BuildKspData(obj.Value.ToObject<JObject>(), tabCount + 1) + tabIndent + "}");
                            break;
                        }

                    case JTokenType.Array:
                        {
                            var itemName = obj.Key.ToString();

                            foreach (var item in obj.Value)
                            {
                                if (item.Type.Equals(JTokenType.Object))
                                {
                                    sb.AppendLine(tabIndent + itemName);
                                    sb.AppendLine(tabIndent + "{" + Environment.NewLine + BuildKspData(item.ToObject<JObject>(), tabCount + 1) + tabIndent + "}");
                                }
                                else if (item.Type.Equals(JTokenType.String))
                                {
                                    sb.AppendLine(BuildArrayField(obj.Value, itemName, tabIndent));

                                    break;
                                }
                                else if (item.Type.Equals(JTokenType.Array))
                                {
                                    sb.AppendLine(BuildArrayField(item, itemName, tabIndent));
                                }
                            }

                            break;
                        }
                }
            }

            return sb.ToString();
        }

        private string BuildJson(KspDataObject root, int tabCount = 0)
        {
            var sb = new StringBuilder();

            var tabSpacer = GetTabs(tabCount);

            BuildProerties(root, sb, tabSpacer);

            var childNames = root.Children.Select(o => o.Name).Distinct();

            if (childNames.Count() > 0 && root.Values.Count > 0)
            {
                sb.Append("," + Environment.NewLine +  tabSpacer);
            }

            for (var n = 0; n < childNames.Count(); n++)
            {
                var comma = string.Empty;

                if (!(n + 2 > childNames.Count()))
                {
                    comma = ",";
                }

                var childName = childNames.ElementAt(n);
                var childObjects = root.Children.Where(o => o.Name == childName);
                var childSpacer = GetTabs(tabCount + 1);

                if (childObjects.Count() > 1)
                {
                    var collectionName = childName;
                    var collectionBuilder = new StringBuilder();
                    var collectionSpacer = string.Empty;

                    for (var c = 0; c < childObjects.Count(); c++)
                    {
                        var childObject = childObjects.ElementAt(c);

                        if (c == 0)
                        {
                            collectionBuilder.Append((n > 0 ? Environment.NewLine + GetTabs(tabCount) : string.Empty) + "\"" + collectionName + "\": [");
                        }
                        else
                        {
                            collectionSpacer = 
                                Environment.NewLine + GetTabs(tabCount);
                        }

                        var collectionComma = string.Empty;

                        if (!(c + 2 > childObjects.Count()))
                        {
                            collectionComma = ",";
                        }

                        collectionBuilder.Append(collectionSpacer + "{" + BuildJson(childObject, tabCount + 1) + "}" + collectionComma);
                    }

                    collectionBuilder.Append(collectionSpacer + "]" + comma);
                    sb.Append(collectionBuilder.ToString());
                }
                else if (childObjects.Count() == 1)
                {
                    var childObject = childObjects.ElementAt(0);

                    sb.Append((n != 0 ? Environment.NewLine + GetTabs(tabCount) : string.Empty) + "\"" + childName + "\": {" + BuildJson(childObject, tabCount + 1) + "}" + comma);
                }
            }

            return sb.ToString();
        }

        private static void BuildProerties(KspDataObject root, StringBuilder jsonStringBuilder, string spacer)
        {
            var fieldComma = string.Empty;
            var fieldNames = root.Values.Select(f => f.Name).Distinct();

            var tabSpacer = string.Empty;

            foreach (var fieldName in fieldNames)
            {
                if (!fieldName.Equals(fieldNames.ElementAt(0)))
                {
                    tabSpacer = spacer;
                }

                var fields = root.Values.Where(f => f.Name == fieldName);

                if (fields.Count() == 1)
                {
                    jsonStringBuilder.Append(fieldComma + tabSpacer + "\"" + fields.ElementAt(0).Name + "\"" + " : " + fields.ElementAt(0).Value);
                }
                else if (fields.Count() > 1)
                {
                    if (fields.ElementAt(0).Value.Contains("["))
                    {
                        var dataComma = string.Empty;
                        var fieldLine = fieldComma + tabSpacer + "\"" + fields.ElementAt(0).Name + "\"" + " : [";
                        for (var f = 0; f < fields.Count(); f++)
                        {

                            fieldLine += dataComma + fields.ElementAt(f).Value;
                            dataComma = ",";
                        }

                        fieldLine += "]";
                        jsonStringBuilder.Append(fieldLine);
                    }
                    else
                    {
                        for (var f = 0; f < fields.Count(); f++)
                        {
                            jsonStringBuilder.Append(
                                fieldComma + tabSpacer + "\"" + fields.ElementAt(f).Name + "-KSPD-MULTI-" + f + "\"" + " : " + fields.ElementAt(f).Value);
                        }
                    }
                }

                fieldComma = "," + Environment.NewLine;
            }
        }

        private static string GetTabs(int count)
        {
            var tabSpacer = string.Empty;
            for (var x = 0; x < count; x++)
            {
                tabSpacer += "\t";
            }

            return tabSpacer;
        }

        private static string BuildArrayField(JToken item, string itemName, string tabIndent)
        {
            var line = tabIndent + itemName + " = ";
            var comma = string.Empty;

            var isnumArray = IsArrayAllNumbers(item);

            foreach (var val in item)
            {
                line += comma + val.ToString().Replace("'", "\"").Replace("`", "'");
                comma = isnumArray ? "," : ", ";
            }

            return line;
        }

        private static bool IsArrayAllNumbers(JToken array)
        {
            foreach (var item in array)
            {
                if (!IsNumber(item.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsNumber(string str)
        {
            int intVar;
            double dblVar;
            float floatVar;
            long longVar;
            decimal decVar;

            if (int.TryParse(str, out intVar)
                || double.TryParse(str, out dblVar)
                || float.TryParse(str, out floatVar)
                || long.TryParse(str, out longVar)
                || decimal.TryParse(str, out decVar))
            {
                return true;
            }

            return false;
        }
    }
}
