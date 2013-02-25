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

    using NAnt.Core.Functions;
    using NAnt.Core.Types;

    /// <summary>
    /// Exposes collection of files defined by Ant style include/exclude filters as a data set that acts as the primary store for the files. 
    /// This implementation allows consumers complete abstraction from the underlying data store. 
    /// 
    /// TODO: Implementations for HTTP publishing, SQL database, file store with JSON objects, others?
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
        private readonly FileMode mode;
        private readonly bool backup;

        private readonly  string fileName;
        private readonly ProcessorRegistry registry; 

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemRepository{T}" /> class.
        /// </summary>	
        /// <remarks>
        /// Required constructor signature for all classes implementing <see cref="IKerbalDataRepo{T}"/>
        /// </remarks>
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

            includes = GetParameter(IncludeParamName, parameters).ToString().Split(PathSeparator).Select(i => i).ToArray();

            excludes = GetParameter(ExcludeParamName, parameters) != null 
                ? GetParameter(ExcludeParamName, parameters).ToString().Split(PathSeparator).Select(i => i).ToArray() : null;

            fileName = GetParameter(FileNameParamName, parameters).ToString();

            mode = GetParameter(FileModeParamName, parameters) != null
                ? (FileMode)Enum.Parse(typeof(FileMode), GetParameter(FileModeParamName, parameters).ToString()) : FileMode.Flat;

            backup = GetParameter(BackupParamName, parameters) == null || (bool)GetParameter(BackupParamName, parameters);
        }

        /// <summary>
        /// Gets the BaseUri of the repo
        /// </summary>
        public string BaseUri
        {
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether data is backed up before a modified copy is saved.
        /// </summary>
        public bool BackupBeforeSave
        {
            get { return backup; } 
        }

        /// <summary>
        /// Gets a list of the available object ID's managed by the repository
        /// </summary>
        /// <returns>object ids</returns>
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

        /// <summary>
        /// Gets all objects managed by the repository
        /// </summary>
        /// <returns>object list</returns>
        public IList<T> Get()
        {
            var files = GetFiles();

            return files != null 
                ? files.FileNames.Cast<string>().Select(p => KspData.LoadKspFile(p, registry.Create<T>())).Where(obj => obj != null).ToList() 
                : null;
        }

        /// <summary>
        /// NOT YET IMPLMENTED
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IList<T> Get(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an object with a particular Id
        /// </summary>
        /// <param name="id">object id to retrieve</param>
        /// <returns>de-serialized object</returns>
        public T Get(string id)
        {
            if (!NameExists(id))
            {
                return null;
            }

            T obj = KspData.LoadKspFile<T>(GetFileInfo(id).FullName, registry.Create<T>());
            (obj as StorableObject).Uri = GetFileInfo(id).DirectoryName;

            return obj;
        }

        /// <summary>
        /// Adds/Updates an object of a particular ID
        /// </summary>
        /// <param name="id">Id to put</param>
        /// <param name="obj">object data</param>
        /// <returns>true=success;false=failure</returns>
        public bool Put(string id, T obj)
        {
            var info = GetFileInfo(id);
            var savePath = info != null ? info.FullName : null;

            if (savePath != null && backup)
            {
                var count = 0;
                var backupPath = savePath + "-BACKUP-" + DateTime.Now.ToString("yyyyMMdd_hhmmss");

                do
                {
                    count++;
                } while (File.Exists(backupPath + "_" + count));
                File.Copy(savePath, backupPath + "_" + count);
            }
            else if (savePath == null)
            {
                if (mode == FileMode.DirPerFile)
                {
                    var files = GetFiles();
                    var saveDir = BaseUri + "\\" + id;
                    Directory.CreateDirectory(saveDir);
                    savePath = saveDir + "\\" + fileName;
                }
                else
                {
                    var files = GetFiles();
                    savePath = BaseUri + "\\" + id + fileName;                   
                }
            }
     
            KspData.SaveFile(savePath, obj, registry.Create<T>());

            return true;
        }

        /// <summary>
        /// Deletes an object with a matching ID
        /// </summary>
        /// <param name="id">id to delete</param>
        /// <returns>true=success;false=failure</returns>
        public bool Delete(string id)
        {
            if (!NameExists(id))
            {
                return false;
            }

            if (mode == FileMode.DirPerFile)
            {
                Directory.Delete(GetFileInfo(id).Directory.FullName, true);
            }
            else
            {
                File.Delete(GetFileInfo(id).FullName);
            }

            return true;
        }

        private FileSet GetFiles()
        {
            var files = new FileSet();
            files.Includes.AddRange(includes);

            if (excludes != null && excludes.Any())
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
            return GetFileInfo(name) != null;
        }

        private object GetParameter(string name, IDictionary<string, object> parameters)
        {
            return parameters.ContainsKey(name) ?  parameters[name] : null;
        }
    }
}
