// -----------------------------------------------------------------------
// <copyright file="StorableObject.cs" company="OpenSauceSolutions">
// � 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Base class for top level consumer models that can be serialized/desterilized. 
    /// Contains functionality for saving/updating data store model is loaded from.
    /// </summary>
    /// <seealso href="http://james.newtonking.com/projects/json/help/?topic=html/T_Newtonsoft_Json_Linq_JToken.htm" target="_blank" alt="Newtonsoft.Json.Linq.JToken">Newtonsoft.Json.Linq.JToken</seealso>
    /// <seeals0 cref="UnMappedPropertiesConverter{T}" />
    /// </summary>
    [JsonObject]
    public abstract class StorableObject : ObservableDictionary<string, JToken>, IKerbalDataObject, IStorable, INotifyPropertyChanged
    {
        private string displayName;
        private object parent;
        private IKerbalDataManager dataManager;

        public event PropertyChangedEventHandler PropertyChanged;

        internal void SetParent<T>(StorableObjects<T> parent) where T : class, IStorable, new()
        {
            this.parent = parent;
        }

        /// <summary>
        /// Gets the instance display name
        /// </summary>
        [JsonIgnore]
        public virtual string DisplayName
        {
            get
            {
                return displayName;
            }

            protected set
            {
                displayName = value;
                OnPropertyChanged("DisplayName", displayName);
            }
        }

        /// <summary>
        /// Gets the original base data
        /// </summary>
        [JsonIgnore]
        public JObject Original
        {
            get;
            internal set; 
        }

        /// <summary>
        /// Gets the id/name of the element
        /// </summary>
        [JsonIgnore]
        public string Id
        {
            get;
            internal set; 
        }

        /// <summary>
        /// Gets the absolute URI of the data
        /// </summary>
        [JsonIgnore]
        public string Uri
        {
            get;
            set; 
        }

        /// <summary>
        /// Gets the is dirty flag
        /// </summary>
        [JsonIgnore]
        public bool IsDirty
        {
            get
            {
                return !JToken.DeepEquals(Original, JObject.FromObject(this));
            }
        }

        /// <summary>
        /// Gets the DataManager instance used by this instance
        /// </summary>
        [JsonIgnore]
        public IKerbalDataManager DataManager
        {
            get 
            { 
                return dataManager; 
            }
            internal set
            {
                dataManager = value;
                Init();
            }
        }

        /// <summary>
        /// Reverts the object state and data to it's original state after the last load or save.
        /// </summary>
        public abstract void Revert();

        /// <summary>
        /// Saves the object and all children to the name specified. 
        /// If class is loaded from KerbalData using the StorableObjects class then save will use the repo managed by
        /// storable objects. Otherwise the local file system is used. 
        /// </summary>
        /// <param name="name">ID or path to store object</param>
        /// <param name="backup">backup the data before saving</param>
        /// <returns>true=success;false=failure</returns>
        public bool Save(string name = null, bool backup = true)
        {
            if (!string.IsNullOrEmpty(name)) // Don't waste cycles saving when we don't need to. Unless we get a name value
            {
                if (parent == null) // If parent is null then this instance was loaded manually. Save using System.IO and lower level classes
                {
                    var savePath = !string.IsNullOrEmpty(name) ? name : Uri;

                    if (string.IsNullOrEmpty(savePath))
                    {
                        throw new KerbalDataException("Loaded Game was not loaded from a file, a file path is required in order to save");
                    }

                    if (File.Exists(savePath) && backup)
                    {
                        KspData.SaveFile(savePath + "-BACKUP-" + DateTime.Now.ToString("yyyyMMdd_hhmmss"), Original);
                    }

                    var data = JObject.FromObject(this);
                    KspData.SaveFile(savePath, data);
                    Original = data;
                    Uri = savePath;
                    return true;
                }
            }

            // If we have the repo then we are loaded as part of a managed repo/storableobjects pattern and we want the repo to save the data.
            // In this case we take the name parameter if it is provided as a new ID. the underlying repo will add the new object instance
            // into the collection using the provided ID. It will be immediately available as part of the StorableObjects collection. 
            var id = !string.IsNullOrEmpty(name) ? name : Id;

            var result = PutToParentRepo(id);

            // I am not really fond of this pattern but i am not sure I want to use a messaging pattern, need to think on this. 
            RefreshParent();
            Id = id;

            return result;
        }

        /// <summary>
        /// Clones a new instance of the object and all underlying data
        /// </summary>
        /// <typeparam name="T">compatible object type to clone to</typeparam>
        /// <returns>cloned object instance</returns>
        public T Clone<T>() where T : class, IStorable, new()
        {
            return JObject.FromObject(this).ToObject<T>();
        }

        // TODO: Refactor pattern to remove ugly reflection hacks to get around generics
        private void RefreshParent()
        {
            var type = GetParentType();
            type.GetMethod("Refresh").Invoke(parent, new object[]{});
        }

        private bool PutToParentRepo(string id)
        {
            var parentType = GetParentType();

#if NET45
            var repo = parentType.GetProperty("Repo").GetValue(parent);
#elif NET40 || NET35 || MONO210
            var repo = parentType.GetProperty("Repo").GetValue(parent, null);
#endif
            var result = repo.GetType().GetMethod("Put").Invoke(repo, new object[] { id, this });

            return (bool)result;
        }

        private Type GetParentType()
        {
            return Type.GetType("KerbalData.StorableObjects`1").MakeGenericType(new Type[] { this.GetType() });
        }

        /// <summary>
        /// Handles inital setup and population of data properties. 
        /// This is some of the "magic" that allows developers creating custom models to easily map
        /// StorableObjects instances with the correct repositories to custom properties without
        /// a ton of wireup code. 
        /// </summary>
        protected virtual void Init()
        {
            var procRegistry = DataManager.ProcRegistry;
            var repoFactory = DataManager.RepositoryFactory;

            var managerType = this.GetType();
            var method = repoFactory.GetType().GetMethod("Create");

            foreach (var prop in managerType.GetProperties().Where(p => p.PropertyType.FullName.Contains("StorableObjects")))
            {
                var modelType = prop.PropertyType.GetGenericArguments()[0];
                var repo =
                    method.MakeGenericMethod(new[] { modelType }).Invoke(
                        repoFactory,
                        new object[] { new Dictionary<string, object>() { { "BaseUri", Uri.EndsWith("\\") ? Uri : Uri + "\\" } }, prop.Name });
#if NET45
                prop.SetMethod.Invoke(this, new object[] { Activator.CreateInstance(prop.PropertyType, new object[] { repo, DataManager }) });
#elif NET40 || NET35 || MONO210
                prop.SetValue(this, Activator.CreateInstance(prop.PropertyType, new object[] { repo, DataManager }), BindingFlags.SetProperty, null, null, null);
#endif 


            }
        }

        protected void OnPropertyChanged(string propertyName, object value = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
