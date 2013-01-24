// -----------------------------------------------------------------------
// <copyright file="UnMappedPropertiesConverter.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// JSON.NET implmentation class for de-serializing to classes while storing unmapped properties to the the <see cref="IKerbalDataObject"/> implmentation.
    /// </summary>
    public class UnMappedPropertiesConverter<T> : CustomCreationConverter<T> where T : class, IKerbalDataObject, new()
    {
        // TODO: This class takes over much of what JSON.NET does for us. While this makes for good code to start breaking dependency on JSON.NET analysis should be done to see if 
        // this was even neccassary to achieve what I needed. 

        /// <summary>
        /// Initializes a new instance of the <see cref="UnMappedPropertiesConverter{T}" /> class.
        /// </summary>	
        public UnMappedPropertiesConverter() : base() { }

        /// <summary>
        /// Gets the canread flag
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }
        
        /// <summary>
        /// Gets the can write flag
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Tells if the provided type can be converted
        /// </summary>
        /// <param name="objectType">type to convert</param>
        /// <returns>true=can convert;false=cannot convert</returns>
        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType);
        }

        /// <summary>
        /// Creates a new instance of the generic type parameter
        /// </summary>
        /// <param name="objectType">compataible type</param>
        /// <returns>new instance of type</returns>
        public override T Create(Type objectType)
        {
            return new T();
        }

        /// <summary>
        /// Reads provided json and creates new object instance
        /// </summary>
        /// <param name="reader">json reader with data</param>
        /// <param name="objectType">type to build</param>
        /// <param name="existingValue">existing value of object if any</param>
        /// <param name="serializer">serializer to use</param>
        /// <returns>de-serialized object instance</returns>
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return BuildObject(objectType, JObject.Load(reader));
        }

        /// <summary>
        /// Writes json data based upon provided object instance
        /// </summary>
        /// <param name="writer">json writer instance to use</param>
        /// <param name="value">value data</param>
        /// <param name="serializer">serializer to use</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializer.TypeNameHandling = TypeNameHandling.None;

            writer.WriteStartObject();

            // Unmapped properties 
            foreach (var prop in ((T)value).Where(p => p.Value.Type == JTokenType.Property))
            {
                writer.WritePropertyName(prop.Key);
                WriteToken(writer, prop.Value);
            }

            // Unmapped properties with object or array values
            foreach (var prop in ((T)value).Where(p => p.Value.Type != JTokenType.Property))
            {
                writer.WritePropertyName(prop.Key);
                WriteToken(writer, prop.Value);
            }

#if NET45
            // Mapped properties
            var props = typeof(T).GetProperties();
            for (var i = 0; i < props.Count(); i++)
            {
                if (props[i].CustomAttributes.Where(ca => ca.AttributeType.Equals(typeof(JsonPropertyAttribute))).Count() > 0)
                {
                    var val = props[i].GetValue(value);

                    if (val != null)
                    {
                        var name = GetJsonPropName(props[i]);

                        writer.WritePropertyName(name);
                        WriteToken(writer, JToken.FromObject(val, serializer));
                    }
                }
            }
#elif NET40 || NET35 || MONO210
            // Mapped Properties
            var props = typeof(T).GetProperties();
            for (var i = 0; i < props.Count(); i++)
            {
                if (props[i].GetCustomAttributes(typeof(JsonPropertyAttribute), true).Count() > 0)
                {
                    var val = props[i].GetValue(value, null);

                    if (val != null)
                    {
                        var name = GetJsonPropName(props[i]);

                        writer.WritePropertyName(name);
                        WriteToken(writer, JToken.FromObject(val, serializer));
                    }
                }
            }            
#endif

            writer.WriteEndObject();
        }

        private object BuildObject(Type type, JToken value)
        {
            if (!ValidType(type))
            {
                ThrowTypeException();
            }

            // Mapped properties
            var obj = type.GetConstructor(new Type[] { }).Invoke(new object[] { });

            foreach (var prop in type.GetProperties())
            {
                var name = GetJsonPropName(prop);

                var jProp = ((JObject)value).Properties().Where(p => p.Name.Equals(name)).FirstOrDefault();

                if (jProp != null)
                {
#if NET45
                    prop.SetValue(obj, ConvertToken(prop.PropertyType, jProp.Value));
#elif NET40 || NET35 || MONO210
                    prop.SetValue(obj, ConvertToken(prop.PropertyType, jProp.Value), BindingFlags.SetProperty, null, null, null);
#endif
                }
            }

            // Unmapped properties
            var objProps = BuildRegisteredNames(type.GetProperties());

            foreach (var pr in ((JObject)value).Properties())
            {
                if (objProps.Where(p => p.Equals(pr.Name)).Count() == 0)
                {
                    ((IDictionary<string, JToken>)obj).Add(pr.Name, pr.Value);
                }
            }

            return Convert.ChangeType(obj, type);
        }

        private object ConvertToken(Type type, JToken value)
        {
            // Base Types
            if (type.Equals(typeof(string)))
            {
                return value.ToString();
            }
            else if (type.Equals(typeof(int)))
            {
                int val;
                if (int.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(double)))
            {
                double val;
                if (double.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(decimal)))
            {
                decimal val;
                if (decimal.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(bool)))
            {
                bool val;
                if (bool.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(byte)))
            {
                byte val;
                if (byte.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(char)))
            {
                char val;
                if (char.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(DateTime)))
            {
                DateTime val;
                if (DateTime.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(long)))
            {
                long val;
                if (long.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(short)))
            {
                short val;
                if (short.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(Single)))
            {
                Single val;
                if (Single.TryParse(value.ToString(), out val))
                {
                    return val;
                }
            }
            else if (type.Equals(typeof(JArray))) // JSON.NET Types
            {
                return value.ToObject<JArray>();
            }
            else if (type.Equals(typeof(JObject)))
            {
                return value.ToObject<JObject>();
            }
            else if (type.Equals(typeof(JToken)))
            {
                return value.ToObject<JToken>();
            }
            else if (type.GetInterfaces().Where(i => i.Equals(typeof(IKerbalDataObject))).Count() > 0) // KerbalData Types
            {
                return BuildObject(type, value);
            }
            else if (type.Name.Contains("IDictionary")) // Supported Collections: IDictionary<string, supportedtype>, IList<supportedtype>, Array of any supported type
            {
                var dictObjArgType = type.GetGenericArguments()[1];

                var dictType = typeof(Dictionary<,>).MakeGenericType(new[] { typeof(string), dictObjArgType });

                var dict = dictType.GetConstructor(new Type[] { }).Invoke(new object[] { });

                if (dictObjArgType.GetInterfaces().Where(i => i.Equals(typeof(IKerbalDataObject))).Count() > 0)
                {
                    foreach (JProperty jp in value)
                    {
                        dictType.GetMethod("Add").Invoke(dict, new object[] { jp.Name, BuildObject(dictObjArgType, jp.Value) });
                    }
                }
                else if (dictObjArgType.Equals(typeof(JToken)))
                {
                    foreach (JProperty jp in value)
                    {
                        dictType.GetMethod("Add").Invoke(dict, new object[] { jp.Name, jp.Value });
                    }
                }

                return dict;
            }
            else if (type.Name.Contains("IList"))
            {
                var garg = type.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(garg);

                var list = listType.GetConstructor(new Type[] { }).Invoke(new object[] { });

                if (garg.GetInterfaces().Where(i => i.Equals(typeof(IKerbalDataObject))).Count() > 0)
                {
                    if (value.Type.Equals(JTokenType.Array))
                    {
                        foreach (var arObj in (JArray)value)
                        {
                            listType.GetMethod("Add").Invoke(list, new object[] {BuildObject(garg, arObj) });
                        }
                    }
                    else
                    {
                        listType.GetMethod("Add").Invoke(list, new object[] { BuildObject(garg, value) });
                    }
                }
                else
                {
                    if (value.Type.Equals(JTokenType.Array))
                    {
                        foreach (var arObj in (JArray)value)
                        {
                            listType.GetMethod("Add").Invoke(list, new object[] { arObj });
                        }
                    }
                    else
                    {
                        listType.GetMethod("Add").Invoke(list, new object[] { value });
                    }
                }

                return list;
            }
            else if (type.IsArray)
            {
                var jarray = ((JArray)value);
                var elementType = type.GetElementType();

                var array = Array.CreateInstance(elementType, jarray.Count);

                for (var i = 0; i < jarray.Count; i++)
                {
                    array.SetValue(ConvertToken(elementType, jarray[i]), i);
                }

                return array;
            }
            else if (type.Equals(typeof(object)))
            {
                return value.ToObject<object>();
            }
 
            return null;
        }

        private bool ValidType(Type type)
        {
            // TODO Incoming type validation
            return true;
        }

        private IList<string> BuildRegisteredNames(PropertyInfo[] infoArray)
        {
            var list = new List<string>();

            for (var i = 0; i < infoArray.Count(); i++)
            {
                var propInfo = infoArray[i];
#if NET45
                list.AddRange(propInfo.CustomAttributes.Where(ca => ca.AttributeType.Equals(typeof(JsonPropertyAttribute))).Select(a => ((JsonPropertyAttribute)(a.Constructor.Invoke(a.ConstructorArguments.Select(ar => ar.Value).ToArray<object>()))).PropertyName).ToList<string>());
#elif NET40 || NET35 || MONO210
                list.AddRange(((JsonPropertyAttribute[])propInfo.GetCustomAttributes(typeof(JsonPropertyAttribute), true)).Select(p => p.PropertyName));            
#endif
            }

            return list;
        }

        private void WriteToken(JsonWriter writer, JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Array:
                    {
                        writer.WriteStartArray();

                        foreach (var item in (JArray)token)
                        {
                            WriteToken(writer, item);
                        }

                        writer.WriteEndArray();
                        break;
                    }
                case JTokenType.Object:
                    {
                        writer.WriteStartObject();

                        foreach (var item in ((JObject)token).Children())
                        {
                            WriteToken(writer, item);
                        }

                        writer.WriteEndObject();
                        break;
                    }
                case JTokenType.Property:
                    {
                        var prop = token as JProperty;
                        writer.WritePropertyName(prop.Name);

                        WriteToken(writer, prop.Value);
                        break; 
                    }
                default:
                    writer.WriteValue(token.ToString());
                    break;
            }
        }

        private string GetJsonPropName(PropertyInfo propInfo)
        {
#if NET45
            var name = 
                propInfo.CustomAttributes.Where(
                ca => ca.AttributeType.Equals(typeof(JsonPropertyAttribute)))
                    .Select(
                    a => ((JsonPropertyAttribute)(a.Constructor.Invoke(a.ConstructorArguments.Select(ar => ar.Value).ToArray<object>()))).PropertyName
                ).ToList<string>().FirstOrDefault();
#elif NET40 || NET35 || MONO210
            var prop = ((JsonPropertyAttribute[])propInfo.GetCustomAttributes(typeof(JsonPropertyAttribute), true)).FirstOrDefault();
            var name = prop != null ? prop.PropertyName : null;
#endif
            name = string.IsNullOrEmpty(name) ? propInfo.Name : name;

            return name;
        }

        private void ThrowTypeException()
        {
            throw new KerbalDataException("ReadJSON can only work with instances of the following types: JObject, JArray, JToken or instances that inherit the following types KerbalJObject, IDictionary<string, VALIDOBJECT/TYPE>, IList<VALIDOBJECT/TYPE>");
        }
    }
}
