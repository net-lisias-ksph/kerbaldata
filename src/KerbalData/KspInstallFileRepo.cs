// -----------------------------------------------------------------------
// <copyright file="KspInstallFileRepo.cs" company="OpenSauceSolutions">
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
    using NAnt.Core.Functions;
    using NAnt.Core.Types;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Exposes collection of files defined by Ant style include/exclude filters as a data set that acts as the primary store for the files. 
    /// This implmentation allows consumers complete abstraction from the underlying data store. 
    /// 
    /// TODO: Implmentations for HTTP publishing, SQL database, file store with JSON objects, others?
    /// </summary>
    public class KspInstallFileRepo<T> : IKerbalDataRepo<T> where T : class, IStorable, new()
    {
        private string[] includes;
        private string[] excludes;

        /// <summary>
        /// Initializes a new instance of the <see cref="KspInstallFileRepo" /> class.
        /// </summary>	
        public KspInstallFileRepo(object[] parameters)
        {
            if (parameters == null || parameters.Count() == 0)
            {
                throw new KerbalDataException("The KspInstallFileRepo requires a search filter at minimum");
            }

            var include = parameters[0].ToString();
            var exclude = parameters.Count() == 2 ? parameters[1].ToString() : string.Empty;

            BaseUri = include + exclude != null ? " EXCLUDE: " + exclude : string.Empty;
            includes = include.Split(';');

            if (!string.IsNullOrEmpty(exclude))
            {
                excludes = exclude.Split(';');
            }
        }

        public string BaseUri
        {
            get; private set;
        }

        public bool BackupBeforeSave
        {
            get { return true; } // Hard coded for now. While the API is still in flux we want to always force a backup to keep people playing with the API from making thier lives diffacult accidently. T
            // TODO Make configurabe
        }

        public IList<StorableItemMetadata<T>> GetIds()
        {
            var result = new List<StorableItemMetadata<T>>();
            var files = GetFiles();

            foreach (var name in files.FileNames)
            {
                var file = new FileInfo(name);
                result.Add(
                    new StorableItemMetadata<T>
                    {
                        Id = (new DirectoryInfo(PathFunctions.GetDirectoryName(name))).Name,
                        Loaded = false,
                        Object = null,
                        Uri = name
                    });
            }

            return result;
        }

        public IList<T> Get()
        {
            IList<JObject> list;
            return Get(out list);
        }

        public IList<T> Get(out IList<JObject> data)
        {
            data = new List<JObject>();
            var result = new List<T>();
            var files = GetFiles();

            foreach (var path in files.FileNames)
            {
                var jobj = KspData.LoadKspFile(path);

                if (jobj != null)
                {
                    result.Add(jobj.ToObject<T>());
                    data.Add(jobj);
                }
            }

            return result;
        }

        public IList<T> Get(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public IList<T> Get(Func<T, bool> func, out IList<JObject> data)
        {
            throw new NotImplementedException();
        }

        public T Get(string id)
        {
            JObject jobj;
            return Get(id, out jobj);
        }

        public T Get(string id, out JObject data)
        {
            data = null;
            if (!NameExists(id))
            {
                return null;
            }

            var jobj = KspData.LoadKspFile(GetFileInfo(id));
            data = jobj;

            return jobj != null ? jobj.ToObject<T>() : null;
        }

        public bool Put(string id, T obj)
        {
            var savePath = GetFileInfo(id);

            if (string.IsNullOrEmpty(savePath))
            {
                throw new KerbalDataException("Loaded Game was not loaded from a file, a file path is required in order to save");
            }

            if (NameExists(id) && BackupBeforeSave)
            {
                File.Copy(savePath, savePath + "-BACKUP-" + DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            }

            var data = JObject.FromObject(obj);
            KspData.SaveFile(savePath, data);

            return true;
        }

        public bool Delete(string id)
        {
            if (NameExists(id))
            {
                File.Delete(GetFileInfo(id));
                return true;
            }

            return false;
        }

        private FileSet GetFiles()
        {
            var files = new FileSet();
            files.Includes.AddRange(includes);

            if (excludes != null && excludes.Count() > 0)
            {
                files.Excludes.AddRange(excludes);
            }

            return files;
        }

        private string GetFileInfo(string name)
        {
            var files = GetFiles();

            foreach (var fileName in files.FileNames)
            {
                if ((new DirectoryInfo(PathFunctions.GetDirectoryName(fileName)).Name.Equals(name)))
                {
                    return fileName;
                }
            }

            return files.BaseDirectory + name;
        }

        private bool NameExists(string name)
        {
            return GetFileInfo(name) != null ? true : false;
        }
    }
}
