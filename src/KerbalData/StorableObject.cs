// -----------------------------------------------------------------------
// <copyright file="StorableObject.cs" company="OpenSauceSolutions">
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
    /// base class for top level consumer models. Contains functionality for saving/updating data store model is loaded from.
    /// </summary>
    [JsonObject]
    public abstract class StorableObject : Dictionary<string, JToken>, IKerbalDataObject, IStorable 
    {
        private object parent;

        internal void SetParent<T>(StorableObjects<T> parent) where T : class, IStorable, new()
        {
            this.parent = parent;
        }

        /// <summary>
        /// Orignal data
        /// </summary>
        [JsonIgnore]
        public JObject Original
        {
            get;
            internal set; 
        }

        [JsonIgnore]
        public string Id
        {
            get;
            internal set; 
        }

        [JsonIgnore]
        public string Uri
        {
            get;
            internal set; 
        }

        [JsonIgnore]
        public bool IsDirty
        {
            get
            {
                return !JToken.DeepEquals(Original, JObject.FromObject(this));
            }
        }

        [JsonIgnore]
        public new KeyCollection Keys
        {
            get { return base.Keys; }
        }

        [JsonIgnore]
        public new ValueCollection Values
        {
            get { return base.Values; }
        }

        [JsonIgnore]
        public new IEqualityComparer<string> Comparer
        {
            get { return base.Comparer; }
        }

        [JsonIgnore]
        public new int Count
        {
            get { return base.Count; }
        }

        /// <summary>
        /// Reverts the object state and data to it's orginal state after the last load or save.
        /// </summary>
        public abstract void Revert();

        /// <summary>
        /// Saves the object and all children to the name specified. 
        /// If class is loaded from KerbalData using the StorableObjects class then save will use the repo managed by
        /// storable objects. Otherwise the local filesystem is used. 
        /// </summary>
        /// <param name="name">ID or path to store object</param>
        /// <param name="backup">backup the data before saving</param>
        /// <returns>true=success;false=failure</returns>
        public bool Save(string name = null, bool backup = true)
        {
            //if (IsDirty || !string.IsNullOrEmpty(name)) // IsDirty implmentation is very expensive right now, for most data operations it's just quicker to save. Re-evaluate when real Diff and deep change matching is implmented. 
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

                // If we have the repo then we are loaded as part of a managed repo/storableobjects pattern and we want the repo to save the data.
                // In this case we take the name paramter if it is provided as a new ID. the underlying repo will add the new object instance
                // into the collection using the provided ID. It will be immideitaly available as part of the StorableObjects collection. 
                var id = !string.IsNullOrEmpty(name) ? name : Id;

                var result = PutToParentRepo(id);
                Original = JObject.FromObject(this);

                // I am not really fond of this pattern but i am not sure I want to use a messaging pattern, need to think on this. 
                RefreshParent();
                Id = id;

                return result;
            }

            return false;
        }

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
                
            var repo = parentType.GetProperty("Repo").GetValue(parent);
            var result = repo.GetType().GetMethod("Put").Invoke(repo, new object[] { id, this });

            return (bool)result;
        }

        private Type GetParentType()
        {
            return Type.GetType("KerbalData.StorableObjects`1").MakeGenericType(new Type[] { this.GetType() });
        }
    }
}
