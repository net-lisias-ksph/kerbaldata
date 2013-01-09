// -----------------------------------------------------------------------
// <copyright file="KerbalDataExtension.cs" company="OpenSauceSolutions">
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

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Extensions used and provided by KebralData
    /// </summary>
    public static class KerbalDataExtension
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

        /// <summary>
        /// Deletes children from the provided object
        /// </summary>
        /// <param name="array">object to run operation on</param>
        /// <param name="predicate">data filter</param>
        /// <returns>count of children removed that match the predicate</returns>
        public static int RemoveChildren(this JArray array, Func<JToken, bool> predicate)
        {
            return DeleteChildElements(array, predicate);
        }

        /// <summary>
        /// Deletes children from the provided object
        /// </summary>
        /// <param name="jobj">object to run operation on</param>
        /// <param name="predicate">data filter</param>
        /// <returns>count of children removed that match the predicate</returns>
        public static int RemoveChildren(this JObject jobj, Func<JToken, bool> predicate)
        {
            return DeleteChildElements(jobj, predicate);
        }

        /// <summary>
        /// Deletes children from the provided object
        /// </summary>
        /// <param name="token">object to run operation on</param>
        /// <param name="predicate">data filter</param>
        /// <returns>count of children removed that match the predicate</returns>
        public static int RemoveChildren(this JToken token, Func<JToken, bool> predicate)
        {
            return DeleteChildElements(token, predicate);
        }

        private static int DeleteChildElements(this JToken token, Func<JToken, bool> predicate)
        {
            var count = 0;
            var objects = token.Where(predicate).ToList<dynamic>();
            
            for (var i = 0; i < objects.Count(); i++)
            {
                objects[i].Remove();
                count++;
            }

            return count;
        }
    }
}
