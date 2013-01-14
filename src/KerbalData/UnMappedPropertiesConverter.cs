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
    /// TODO: Class Summary
    /// </summary>
    public class UnMappedPropertiesConverter<T> : CustomCreationConverter<T> where T : class, IKerbalDataObject, new()
    {
        public UnMappedPropertiesConverter() : base() { }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType);
        }

        
        public override T Create(Type objectType)
        {
            return new T();
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return ReadJson(JObject.Load(reader), objectType, existingValue, serializer);
        }

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

            writer.WriteEndObject();
        }

        private object ReadJson(JToken jobj, Type objectType, object existingValue, JsonSerializer serializer)// where T : class, IKerbalDataObject, new()
        {
            if (!ValidType(objectType))
            {
                ThrowTypeException();
            }

            var objProps = BuildRegisteredNames(objectType.GetProperties());

            var obj = objectType.GetConstructor(new Type[] { }).Invoke(new object[] { });

            foreach (var prop in objectType.GetProperties())
            {
                var name = GetJsonPropName(prop);

                var jProp = ((JObject)jobj).Properties().Where(p => p.Name.Equals(name)).FirstOrDefault();

                if (jProp != null)
                {
                    // Base Types
                    if (prop.PropertyType.Equals(typeof(string)))
                    {
                        prop.SetValue(obj, jProp.Value.ToString());
                    }
                    else if (prop.PropertyType.Equals(typeof(int)))
                    {
                        int val;
                        if (int.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(double)))
                    {
                        double val;
                        if (double.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(decimal)))
                    {
                        decimal val;
                        if (decimal.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(bool)))
                    {
                        bool val;
                        if (bool.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(byte)))
                    {
                        byte val;
                        if (byte.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(char)))
                    {
                        char val;
                        if (char.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(DateTime)))
                    {
                        DateTime val;
                        if (DateTime.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(long)))
                    {
                        long val;
                        if (long.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(short)))
                    {
                        short val;
                        if (short.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(Single)))
                    {
                        Single val;
                        if (Single.TryParse(jProp.Value.ToString(), out val))
                        {
                            prop.SetValue(obj, val);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(JArray))) // JSON.NET Types
                    {
                        prop.SetValue(obj, jProp.Value.ToObject<JArray>());
                    }
                    else if (prop.PropertyType.Equals(typeof(JObject)))
                    {
                        prop.SetValue(obj, jProp.Value.ToObject<JObject>());
                    }
                    else if (prop.PropertyType.Equals(typeof(JToken)))
                    {
                        prop.SetValue(obj, jProp.Value.ToObject<JToken>());
                    }
                    else if (prop.PropertyType.GetInterfaces().Where(i => i.Equals(typeof(IKerbalDataObject))).Count() > 0) // KerbalData Types
                    {
                        prop.SetValue(obj, ReadJson(jProp.Value, prop.PropertyType, null, serializer));
                    }
                    else if (prop.PropertyType.Name.Contains("IDictionary")) // Supported Collections: IDictionary<string, supportedtype>, IList<supportedtype>, Array of any supported type
                    {
                        var dictObjArgType = prop.PropertyType.GetGenericArguments()[1];

                        
                        //var dictType = prop.PropertyType.MakeGenericType(new [] { typeof(string), dictObjArgType });

                        var dictType = typeof(Dictionary<,>).MakeGenericType(new[] { typeof(string), dictObjArgType });

                        var dict = dictType.GetConstructor(new Type[] { }).Invoke(new object[] { });

                        if (dictObjArgType.GetInterfaces().Where(i => i.Equals(typeof(IKerbalDataObject))).Count() > 0)
                        {
                            foreach (JProperty jp in jProp.Value)
                            {
                                dictType.GetMethod("Add").Invoke(dict, new object[] { jp.Name, ReadJson(jp.Value, dictObjArgType, null, serializer) });
                            }
                        }
                        else if (dictObjArgType.Equals(typeof(JToken)))
                        {
                            foreach (JProperty jp in jProp.Value)
                            {
                                dictType.GetMethod("Add").Invoke(dict, new object[] { jp.Name, jp.Value });
                            }
                        }

                        prop.SetValue(obj, dict);
                    }
                    else if (prop.PropertyType.Name.Contains("IList"))
                    {
                        var garg = prop.PropertyType.GetGenericArguments()[0];
                        var listType = typeof(List<>).MakeGenericType(garg);

                        var list = listType.GetConstructor(new Type[] { }).Invoke(new object[] { });

                        if (garg.GetInterfaces().Where(i => i.Equals(typeof(IKerbalDataObject))).Count() > 0)
                        {
                            if (jProp.Value.Type.Equals(JTokenType.Array))
                            {
                                foreach (var arObj in (JArray)jProp.Value)
                                {
                                    listType.GetMethod("Add").Invoke(list, new object[] { ReadJson(arObj, garg, null, serializer) });
                                }
                            }
                            else
                            {
                                listType.GetMethod("Add").Invoke(list, new object[] { ReadJson(jProp.Value, garg, null, serializer) });
                            }
                        }
                        else
                        {
                            if (jProp.Value.Type.Equals(JTokenType.Array))
                            {
                                foreach (var arObj in (JArray)jProp.Value)
                                {
                                    listType.GetMethod("Add").Invoke(list, new object[] { arObj });
                                }
                            }
                            else
                            {
                                listType.GetMethod("Add").Invoke(list, new object[] { jProp.Value });
                            }
                        }

                        prop.SetValue(obj, list);
                    }
                    else if (prop.PropertyType.IsArray)
                    {
                        var jarray = ((JArray)jProp.Value);
                        var elementType = prop.PropertyType.GetElementType();

                        var array = Array.CreateInstance(elementType, jarray.Count);

                        for (var i = 0; i < jarray.Count; i++)
                        {
                            array.SetValue(ReadJson(jarray[i], elementType, null, serializer), i);
                        }
                    }
                    else if (prop.PropertyType.Equals(typeof(object)))
                    {
                        prop.SetValue(obj, jProp.Value.ToObject<object>());
                    }
                }
            }

            foreach (var pr in ((JObject)jobj).Properties())
            {
                if (objProps.Where(p => p.Equals(pr.Name)).Count() == 0)
                {
                    ((KerbalDataObject)obj).Add(pr.Name, pr.Value);
                }
            }
            
            return Convert.ChangeType(obj, objectType);
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

                list.AddRange(propInfo.CustomAttributes.Where(ca => ca.AttributeType.Equals(typeof(JsonPropertyAttribute))).Select(a => ((JsonPropertyAttribute)(a.Constructor.Invoke(a.ConstructorArguments.Select(ar => ar.Value).ToArray<object>()))).PropertyName).ToList<string>());
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
            var name = 
                propInfo.CustomAttributes.Where(
                ca => ca.AttributeType.Equals(typeof(JsonPropertyAttribute)))
                    .Select(
                    a => ((JsonPropertyAttribute)(a.Constructor.Invoke(a.ConstructorArguments.Select(ar => ar.Value).ToArray<object>()))).PropertyName
                ).ToList<string>().FirstOrDefault();

            name = string.IsNullOrEmpty(name) ? propInfo.Name : name;

            return name;
        }

        private void ThrowTypeException()
        {
            throw new KerbalDataException("ReadJSON can only work with instances of the following types: JObject, JArray, JToken or instances that inherit the following types KerbalJObject, IDictionary<string, VALIDOBJECT/TYPE>, IList<VALIDOBJECT/TYPE>");
        }
    }
}
