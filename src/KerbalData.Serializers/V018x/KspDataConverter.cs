// -----------------------------------------------------------------------
// <copyright file="KspDataConverter.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serializers.V018x
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Draft implmentation of KSP 0.18.x data converter. Currently uses a generic name. Once full pattern is implmented generic implmentation names will be dropped in favor of version specific converter names. 
    /// </summary>
    public class KspDataConverter : IKspDataConverter
    {
        /// <summary>
        /// Builds a json string 
        /// </summary>	
        /// <param name="context">context data to use</param>
        /// <returns>de-serialized JSON string</returns>
        public string BuildJson(KspDataContext context)
        {
            //var json = "{" + BuildJson(context.Data) + "}";
            var jobj = BuildJson(context.Data);
            var json = jobj.ToString(Formatting.Indented, new JsonConverter[] { });//.WriteToFile("jsonoutput.txt");
            return json;
        }
            
        /// <summary>
        /// Builds a KSP data string
        /// </summary>
        /// <param name="json">JSON string to convert</param>
        /// <returns>KSP data string based on JSON data</returns>
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

        /// <summary>
        /// Primary internal method to build the KSP data string. Contains version specific rules and formatting
        /// </summary>
        /// <param name="jobj">JSon Object data to use.</param>
        /// <param name="tabCount">starting point for indented tabs (method will furhter indent children through recursive calls</param>
        /// <param name="objectName">name of data object for header</param>
        /// <returns>KSP data string</returns>
        private static string BuildKspData(JObject jobj, int tabCount = 0, string objectName = null)
        {
            var tabIndent = GetTabs(tabCount);
            var sb = new StringBuilder();

            foreach (var obj in jobj)
            {
                // THe "-KSPD-MULTI-" substring handles for the odd collection type where multiple fields of the same name exist in the same object in the KSP data. 
                // This is most often seen with the "sym" property but can be found elsewhere.
                // Ideas for a better way to handle this are welcome.
                var key = obj.Key.Contains("-KMULTI-")
                    ? obj.Key.Substring(0, obj.Key.IndexOf("-KMULTI-"))
                    : obj.Key;

                if (key.Contains("KCOM-"))
                {
                    sb.AppendLine("// " + obj.Value.ToString());
                }
                else if (key.ToUpper().Equals("CDATA"))
                {
                    sb.AppendLine(tabIndent + key + " = " + Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(obj.Value.ToString())));
                }
                else
                {
                    switch (obj.Value.Type)
                    {
                        case JTokenType.String:
                            {
                                sb.AppendLine(tabIndent + key + " = " + obj.Value.ToString().Replace("\\\"", "\""));
                                break;
                            }

                        case JTokenType.Object:
                            {
                                sb.AppendLine(objectName == null ? tabIndent + key : tabIndent + objectName);

                                // Recursive call
                                sb.AppendLine(tabIndent + "{" + Environment.NewLine + BuildKspData(obj.Value.ToObject<JObject>(), tabCount + 1) + tabIndent + "}");
                                break;
                            }

                        case JTokenType.Array:
                            {
                                var itemName = key;

                                foreach (var item in obj.Value)
                                {
                                    if (item.Type.Equals(JTokenType.Object))
                                    {
                                        sb.AppendLine(tabIndent + itemName);

                                        // Recursive call
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
            }

            return sb.ToString();
        }

        /// <summary>
        /// Internal class that handles converting KSP data to JSON. Recursively calls itself to handle nested elements. Contains version specific rules and code.
        /// </summary>
        /// <param name="root">root data object (data context is stripped)</param>
        /// <param name="tabCount">starting tab index for proper formatting</param>
        /// <returns>JSON string</returns>
        private JObject BuildJson(KspDataObject root)
        {
            var jobj = new JObject();

            BuildProerties(root, jobj);

            var childNames = root.Children.Select(o => o.Name).Distinct();

            // Loop through child names rather than the full collection to allow for array building logic
            for (var n = 0; n < childNames.Count(); n++)
            {
                var childName = childNames.ElementAt(n);
                var childObjects = root.Children.Where(o => o.Name == childName);

                if (childObjects.Count() > 1) // Handle collection of children
                {
                    var collectionName = childName;
                    var array = new object[childObjects.Count()];

                    for (var c = 0; c < childObjects.Count(); c++)
                    {
                        array[c] = BuildJson(childObjects.ElementAt(c));
                    }

                    jobj[collectionName] = JArray.FromObject(array);
                }
                else if (childObjects.Count() == 1) // Handle uniquely named children
                {
                    var childObject = childObjects.ElementAt(0);

                    // Recursive call to build child
                    jobj[childName] = BuildJson(childObject);
                }
            }

            return jobj;
        }

        /// <summary>
        /// Builds the child properties for the JSon string
        /// </summary>
        /// <param name="root">data object to use</param>
        /// <param name="jsonStringBuilder">string builder instance to use</param>
        /// <param name="spacer">pre-calulated tab prefix spacer</param>
        //private static void BuildProerties(KspDataObject root, StringBuilder sb, string spacer)
        private static void BuildProerties(KspDataObject root, JObject jobj)
        {
            var fieldNames = root.Values.Select(f => f.Name).Distinct();

            foreach (var fieldName in fieldNames)
            {
                var fields = root.Values.Where(f => f.Name == fieldName);

                if (fields.Count() == 1)
                {
                    var field = fields.ElementAt(0);
                    if (!field.Value.Contains("["))
                    {
                        jobj[field.Name] = field.Value;
                    }
                    else
                    {
                        try
                        {
                            jobj[field.Name] = JArray.Parse(field.Value);
                        }
                        catch (Exception ex)
                        {
                            var test = ex.Message;
                        }

                    }
                }
                else if (fields.Count() > 1)
                {
                    // Special handling for the strange case of multiple properties of the same name as children of the same object. JSON does not support this so we tag the name with the
                    // substring "-KSPD-MULTI-".
                    // TODO: Wrapper classes need to somehow handle for this and display data in a more reasonable manner rather than relying on consumers to use the "special" substring.
                    for (var f = 0; f < fields.Count(); f++)
                    {
                        var field = fields.ElementAt(f);
                        if (!field.Value.Contains("["))
                        {
                            jobj[field.Name + "-KMULTI-" + Guid.NewGuid().ToString()] = field.Value;
                        }
                        else
                        {
                            try
                            {
                                jobj[field.Name + "-KMULTI-" + Guid.NewGuid().ToString()] = JArray.Parse(field.Value);
                            }
                            catch (Exception ex)
                            {
                                var test = ex.Message;
                            }

                        }
                    }
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
                tabSpacer += "\t";
            }

            return tabSpacer;
        }

        /// <summary>
        /// Builds a KSP data array field
        /// </summary>
        /// <param name="item">JSON object to serialize</param>
        /// <param name="itemName">name of array object</param>
        /// <param name="tabIndent">starting tab indent</param>
        /// <returns>KSP data string fragment</returns>
        private static string BuildArrayField(JToken item, string itemName, string tabIndent)
        {
            var line = tabIndent + itemName + " = ";
            var comma = string.Empty;

            var isnumArray = IsArrayAllNumbers(item);

            foreach (var val in item)
            {
                // Using a special char here to represent quotes. This is somewhat destructive to the orginal string to a certain degree. 
                // TODO: Better string handling here. May require some REGEX checks (regex would make parts of this entire class shorter)
                line += comma + val.ToString();// .Replace("'", "\"").Replace("`", "'"); 
                comma = isnumArray ? "," : ", ";
            }

            return line;
        }

        /// <summary>
        /// Tells if the provided JSON array is all number values or not
        /// This is used to adhere to a small formatting rule used in KSP data. All number arrays are written differently than mixed arrays. 
        /// I have not tested if the difference matters or not, I am going to contiune to strive for byte-to-byte compatibility as much as possible so as not
        /// to scare end users with radically changed save files at minimum and to potentially keep things from breaking should something simple actually be important.
        /// </summary>
        /// <param name="array">array to validate</param>
        /// <returns>true=all numbers;false=not numbers</returns>
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

        /// <summary>
        /// Tells if a string represents a number value
        /// </summary>
        /// <param name="str">string to validate</param>
        /// <returns>true=is number; false=not nimber</returns>
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
