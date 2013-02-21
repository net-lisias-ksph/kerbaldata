// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serialization.Serializers.V018x
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    using Serialization;

    /// <summary>
    /// Converts data context to <see cref="JObject"/> instances. Used as mid step to final object mapping and for saving data in Json format.
    /// </summary>
    public class JsonObjectConverter<T> : IKspConverter<T> where T : JObject
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
        /// Gets the list of versions supported by this converter
        /// </summary>
        public virtual IList<string> Versions
        {
            get { return versions;  }
        }

        /// <summary>
        /// Checks if this type supports a particular version
        /// </summary>
        /// <param name="version">version to check</param>
        /// <returns>true=supported;false=not supported</returns>
        public virtual bool SupportsVersion(string version)
        {
            return versions.Contains(version.Trim());
        }

        /// <summary>
        /// Converts a <see cref="KspDataContext"/> instance into a specific object type
        /// </summary>
        /// <param name="context">data to map</param>
        /// <returns>object constructed based on context</returns>
        public T Convert(KspDataContext context)
        {
            var obj = (T)BuildJson(context.Data);
            return obj;
        }


        /// <summary>
        /// Converts an object instance into a <see cref="KspDataContext"/> instance
        /// </summary>
        /// <param name="obj">object to convert</param>
        /// <returns>data context instance based on object data</returns>
        public KspDataContext Convert(T obj)
        {
            var context = new KspDataContext();

            BuildKspData((JObject)obj, context.Data);

            return context;
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
        /// Builds the child properties for the JObject
        /// </summary>
        /// <param name="root">data object to use</param>
        /// <param name="jobj">JObject instance to populate</param>
        private static void BuildProerties(KspDataObject root, JObject jobj)
        {
            var fieldNames = root.Values.Select(f => f.Name).Distinct();

            foreach (var fieldName in fieldNames)
            {
                var fields = root.Values.Where(f => f.Name == fieldName);

                if (fields.Count() == 1)
                {
                    var field = fields.ElementAt(0);
                    if (!field.Value.Contains(',') 
                        || fieldName.ToUpper().Equals("CDATA")
                        || fieldName.ToUpper().Equals("DESCRIPTION") 
                        || fieldName.ToUpper().Equals("TITLE"))
                    {
                        jobj[field.Name] = field.Value;
                    }
                    else
                    {
                        try
                        {
                            jobj[field.Name] = JArray.Parse(BuildArrayField(field.Value));
                        }
                        catch (Exception ex)
                        {
                            throw new KerbalDataException("An error has occured while converting de-serialized data to a JSON object", ex);
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
                        if (!field.Value.Contains(',') 
                            || fieldName.ToUpper().Equals("CDATA") 
                            || fieldName.ToUpper().Equals("DESCRIPTION")
                            || fieldName.ToUpper().Equals("TITLE"))
                        {
                            jobj[field.Name + "-KMULTI-" + Guid.NewGuid().ToString()] = field.Value;
                        }
                        else
                        {
                            try
                            {
                                jobj[field.Name + "-KMULTI-" + Guid.NewGuid().ToString()] = 
                                    JArray.Parse(BuildArrayField(field.Value));
                            }
                            catch (Exception ex)
                            {
                                throw new KerbalDataException("An error has occured while converting de-serialized data to a JSON object", ex);
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Primary internal method to build the KSP data string. Contains version specific rules and formatting
        /// </summary>
        /// <param name="jobj">JSon Object data to use.</param>
        /// <param name="root">data object to populate</param>
        private static void BuildKspData(JObject jobj, KspDataObject root)
        {
            foreach (var obj in jobj)
            {
                // THe "-KSPD-MULTI-" substring handles for the odd collection type where multiple fields of the same name exist in the same object in the KSP data. 
                // This is most often seen with the "sym" property but can be found elsewhere.
                // Ideas for a better way to handle this are welcome.
                var key = obj.Key.Contains("-KMULTI-")
                    ? obj.Key.Substring(0, obj.Key.IndexOf("-KMULTI-"))
                    : obj.Key;

                switch (obj.Value.Type)
                {
                    case JTokenType.String:
                        {
                            root.Values.Add(new KspDataField() { Name = key, Value = obj.Value.ToString() });
                            break;
                        }
                    case JTokenType.Object:
                        {
                            var childObj = new KspDataObject() { Name = key };

                            BuildKspData(obj.Value.ToObject<JObject>(), childObj);
                            root.Children.Add(childObj);
                            break;
                        }
                    case JTokenType.Array:
                        {
                            var itemName = key;

                            if (obj.Value.Count() > 0)
                            {
                                switch (obj.Value[0].Type)
                                {
                                    case JTokenType.Object:
                                        {
                                            foreach (var item in obj.Value)
                                            {
                                                var childObj = new KspDataObject() { Name = itemName };

                                                BuildKspData(item.ToObject<JObject>(), childObj);
                                                root.Children.Add(childObj);
                                            }
                                            break;
                                        }
                                    case JTokenType.String:
                                        {
                                            root.Values.Add(
                                                new KspDataField()
                                                {
                                                    Name = itemName,
                                                    Value = BuildArrayField(obj.Value)
                                                });
                                            break;
                                        }
                                    default:
                                        {
                                            root.Values.Add(
                                                new KspDataField()
                                                {
                                                    Name = itemName,
                                                    Value = BuildArrayField(obj.Value)
                                                });
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                }  
            }
        }

        /// <summary>
        /// Builds a KSP data array field
        /// </summary>
        /// <param name="item">JSON object to serialize</param>
        /// <returns>KSP data string fragment</returns>
        private static string BuildArrayField(JToken item)
        {
            var line = string.Empty; // tabIndent + itemName + " = ";
            var comma = string.Empty;

            var isnumArray = IsArrayAllNumbers(item);

            foreach (var val in item)
            {
                line += comma + val.ToString(); 
                comma = isnumArray ? "," : ", ";
            }

            return line;
        }

        /// <summary>
        /// Builds a KSP data array field
        /// </summary>
        /// <param name="item">JSON object to serialize</param>
        /// <returns>KSP data string fragment</returns>
        private static string BuildArrayField(string data)
        {
            var array = data.Split(',');

            var line = string.Empty;
            var comma = string.Empty;

            foreach (var val in array)
            {
                line += comma + "\"" + val.Trim().ToString() + "\"";
                comma = ",";
            }

            return "[" + line + "]";
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
