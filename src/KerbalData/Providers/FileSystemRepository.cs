// -----------------------------------------------------------------------
// <copyright file="FileSystemProvider.cs" company="OpenSauceSolutions">
// � 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Providers
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
    public class FileSystemRepository<T> : IKerbalDataRepo<T> where T : class, IStorable, new()
    {
        private enum FileMode
        {
            Flat,
            DirPerFile
        }

        private const string UriParamName = "BaseUri";
        private const string IncludeParamName = "Include";
        private const string ExcludeParamName = "Exclude";
        private const string FileNameParamName = "FileName";
        private const string FileModeParamName = "FileMode";
        private const string BackupParamName = "Backup";

        private const char PathSeparator = ';';

        private readonly string[] includes;
        private readonly string[] excludes;
        private FileMode mode;
        private bool backup = false;

        private string fileName;

        private ProcessorRegistry registry; 

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemRepository" /> class.
        /// </summary>	
        public FileSystemRepository(ProcessorRegistry registry, IDictionary<string, object> parameters)
        {
            if (parameters == null 
                || !parameters.ContainsKey(IncludeParamName)
                || !parameters.ContainsKey(UriParamName)
                || !parameters.ContainsKey(FileNameParamName)
                || !parameters.ContainsKey(FileModeParamName))
            {
                throw new KerbalDataException("The KspInstallFileRepo requires a search filter at minimum");
            }

            this.registry = registry;

            BaseUri = GetParameter(UriParamName, parameters).ToString();

            includes = GetParameter(IncludeParamName, parameters).ToString().Split(PathSeparator).Select(i => BaseUri +  i).ToArray();

            excludes = GetParameter(ExcludeParamName, parameters) != null 
                ? GetParameter(ExcludeParamName, parameters).ToString().Split(PathSeparator).Select(i => BaseUri + i).ToArray() : null;

            fileName = GetParameter(FileNameParamName, parameters).ToString();

            mode = GetParameter(FileModeParamName, parameters) != null
                ? (FileMode)Enum.Parse(typeof(FileMode), GetParameter(FileModeParamName, parameters).ToString()) : FileMode.Flat;

            backup = GetParameter(BackupParamName, parameters) != null 
                ? (bool)GetParameter(BackupParamName, parameters) : true;
        }

        public string BaseUri
        {
            get; private set;
        }

        public bool BackupBeforeSave
        {
            get { return backup; } 
        }

        public IList<StorableItemMetadata<T>> GetIds()
        {
            var result = new List<StorableItemMetadata<T>>();
            var files = GetFiles();

            foreach (var name in files.FileNames)
            {
                var file = new FileInfo(name);

                if (mode == FileMode.DirPerFile)
                {
                    result.Add(
                        new StorableItemMetadata<T>
                        {
                            Id = (new DirectoryInfo(PathFunctions.GetDirectoryName(name))).Name,
                            Loaded = false,
                            Object = null,
                            Uri = name
                        });
                }
                else
                {
                    result.Add(
                        new StorableItemMetadata<T>
                        {
                            Id = PathFunctions.GetFileNameWithoutExtension(name),
                            Loaded = false,
                            Object = null,
                            Uri = name
                        });
                }
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
                T obj = KspData.LoadKspFile<T>(path, registry.Create<T>());

                if (obj != null)
                {
                    result.Add(obj);
                    data.Add(JObject.FromObject(obj));
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

            T obj = KspData.LoadKspFile<T>(GetFileInfo(id).FullName, registry.Create<T>());
            (obj as StorableObject).Uri = GetFileInfo(id).DirectoryName;
            data = JObject.FromObject(obj);

            return obj;
        }

        public bool Put(string id, T obj)
        {
            var savePath = GetFileInfo(id).FullName;

            if (string.IsNullOrEmpty(savePath))
            {
                throw new KerbalDataException("Loaded Game was not loaded from a file, a file path is required in order to save");
            }

            if (savePath != null && backup)
            {
                File.Copy(savePath, savePath + "-BACKUP-" + DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            }
            else if (savePath == null)
            {
                if (mode == FileMode.DirPerFile)
                {
                    var files = GetFiles();
                    var saveDir = files.BaseDirectory + "\\" + id;
                    Directory.CreateDirectory(saveDir);
                    savePath = saveDir + "\\" + fileName;
                }
                else
                {
                    var files = GetFiles();
                    savePath = files.BaseDirectory + "\\" + id + fileName;                   
                }
            }
     
            KspData.SaveFile(savePath, obj, registry.Create<T>());

            return true;
        }

        public bool Delete(string id)
        {
            if (NameExists(id))
            {
                File.Delete(GetFileInfo(id).FullName);
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

        private FileInfo GetFileInfo(string name)
        {
            var files = GetFiles();

            foreach (var fileName in files.FileNames)
            {
                if (mode == FileMode.DirPerFile)
                {
                    if ((new DirectoryInfo(PathFunctions.GetDirectoryName(fileName)).Name.Equals(name)))
                    {
                        return new FileInfo(fileName);
                    }
                }
                else
                {
                    if (PathFunctions.GetFileNameWithoutExtension(fileName).Equals(name))
                    {
                        return new FileInfo(fileName);
                    }
                }
            }

            return null;
        }

        private bool NameExists(string name)
        {
            return GetFileInfo(name) != null ? true : false;
        }

        private object GetParameter(string name, IDictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(name))
            {
                // All parameters in this repo are parsed as strings
                return parameters[name];
            }

            return null;
        }
    }
}