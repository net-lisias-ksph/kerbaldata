// -----------------------------------------------------------------------
// <copyright file="SaveFile.cs" company="OpenSauceSolutions">
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
    using Newtonsoft.Json.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    /// 
    [JsonObject]
    public class SaveFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFile" /> class.
        /// </summary>	
        public SaveFile()
        {
        }

        [JsonProperty("GAME")]
        public Game Game { get; set; }

        public string DataPath { get; set; }

        /// <summary>
        /// Saves the current data state to the file specified. 
        /// </summary>
        /// <param name="filePath">defaults to path data is loaded from unless loaded by jobject where an execption is thrown if a path is not provided. The FilePath property can be used to determine if an instance was loaded from a file or a JObject</param>
        /// <param name="backupOrignal">defaults to true. creates a backup of the filePath used (if the file exists) before saving. Uses the format "filename.ext-BACKUP-yyyyMMdd_hhmmss"</param>
        public virtual void Save(string filePath = null, bool backupOrignal = true)
        {
            /*
            if (!IsDirty)
            {
                return;
            }*/

            var savePath = !string.IsNullOrEmpty(filePath) ? filePath : DataPath;

            if (string.IsNullOrEmpty(savePath))
            {
                throw new KerbalDataException("Loaded Game was not loaded from a file, a file path is required in order to save");
            }

            
            if (File.Exists(savePath) && backupOrignal)
            {
                File.Copy(savePath, savePath + "-BACKUP-" + DateTime.Now.ToString("yyyyMMdd_hhmmss"));
                //KspData.SaveFile(savePath + "-BACKUP-" + DateTime.Now.ToString("yyyyMMdd_hhmmss"), orignal);
            }

            KspData.SaveFile(savePath, JObject.FromObject(this));
        }

        public static SaveFile CreateFromKspFile(string dataPath)
        {
            var obj = KspData.LoadKspFile(dataPath).ToObject<SaveFile>();
            obj.DataPath = dataPath;
            return obj;
        }

        public static SaveFile CreateFromJsonFile(string dataPath)
        {
            var obj = KspData.LoadJsonFile(dataPath).ToObject<SaveFile>();
            obj.DataPath = dataPath;
            return obj;
        }

        public static SaveFile Create(JObject jobj)
        {
            return jobj.ToObject<SaveFile>();
        }

        public static bool TryCreateFromKspFile(string dataPath, out SaveFile jobj)
        {
            try
            {
                jobj = CreateFromKspFile(dataPath);
            }
            catch (Exception ex) // TODO: Better exception handling across codebase. 
            {
                jobj = null;
                return false;
            }

            return true;
        }

        public static bool TryCreateFromJsonFile(string dataPath, out SaveFile jobj)
        {
            try
            {
                jobj = CreateFromJsonFile(dataPath);
            }
            catch (Exception ex) // TODO: Better exception handling across codebase. 
            {
                jobj = null;
                return false;
            }

            return true;
        }
    }
}
