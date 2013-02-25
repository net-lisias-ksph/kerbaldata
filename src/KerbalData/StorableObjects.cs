// -----------------------------------------------------------------------
// <copyright file="StorableObjects.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Maintains the top level collection of Kerbal data objects. 
    /// Works similar to a dictionary however it does not implement IDictionary in order to restrict usage.
    /// It is important to note that unlike an IDictionary implementation this class will accept a call to the Add method
    /// even if another value with the same name already exists. The previous value will be overwritten by the latest addition to the
    /// list. 
    /// </summary>
    /// <typeparam name="T">model type being managed</typeparam>
    public class StorableObjects<T> : IStorableObjects where T : class, IStorable, new()
    {
        private IDictionary<string, StorableItemMetadata<T>> objects = new Dictionary<string, StorableItemMetadata<T>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StorableObjects{T}" /> class.
        /// </summary>	
        public StorableObjects(IKerbalDataRepo<T> repo, IKerbalDataManager dataManager)
        {
            Repo = repo;
            var ids = Repo.GetIds();
            objects = ids.ToDictionary(k => k.Id, v => v);

            DataManager = dataManager;
        }

        /// <summary>
        /// Gets or sets the value of an unmapped property using a unique name. 
        /// </summary>
        /// <param name="id">Id to lookup</param>
        /// <returns>object for the associated ID</returns>
        public T this[string id]
        {
            get
            {
                Load(id);
                return objects[id].Object;

            }
            set
            {
                //Load(id);
                Add(value, id); // Run The add method as it handles both updates and new objects. 
            }
        }

        /// <summary>
        /// Gets the data manager instance that manages this object
        /// </summary>
        public IKerbalDataManager DataManager
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of objects being maintained
        /// </summary>
        public int Count
        {
            get
            {
                return objects.Count;
            }
        }

        /// <summary>
        /// Gets the object ids maintained by this collection
        /// </summary>
        public IEnumerable<string> Names
        {
            get { return objects.Keys; }
        }

        /// <summary>
        /// Gets the repository instance used by this collection
        /// </summary>
        public IKerbalDataRepo<T> Repo { get; private set; }

        /// <summary>
        /// Checks if provided id exists as an available object instance
        /// </summary>
        /// <param name="id">id to lookup</param>
        /// <returns>true=id available;false=id not available</returns>
        public bool ContainsId(string id)
        {
            return objects.ContainsKey(id);
        }

        public void Add(object data, string id = null)
        {
            if (!(data is T))
            {
                return;
            }

            Add((T)data, id);

        }

        /// <summary>
        /// Adds the object to the data collection. Saving the object after adding to the collection will store the object data
        /// with the configured repository. 
        /// The Save() method on the object is called after adding. 
        /// </summary>
        /// <param name="obj">object to add</param>
        /// <param name="id">(optional) id to use - if none is provided the object's Id property value will be used, if this property 
        /// is null a random ID will be generated. If the ID matches that of an existing object the existing object data will be overwritten.</param>
        public void Add(T obj, string id = null)
        {
            var storable = obj as StorableObject;

            if (storable == null)
            {
                throw new KerbalDataException("Object Data cannot be null");
            }

            storable.Id = id ?? obj.Id;

            if (storable.Id == null)
            {
                storable.Id = Guid.NewGuid().ToString();
            }

            storable.Original = JObject.FromObject(obj);
            storable.SetParent(this);
            obj.Save();

            objects[id].Object = obj;
            objects[id].Loaded = true;
            (objects[id].Object as StorableObject).Uri = Repo.BaseUri + "\\" + obj.Id;

            /*
            if (!objects.ContainsKey(obj.Id))
            {
                // Not sure this path ever will fire. TODO: Analysis for removal of this branch.
                objects[obj.Id] = new StorableItemMetadata<T>()
                {
                    Id = obj.Id,
                    Loaded = true,
                    Object = obj,
                    Uri = Repo.BaseUri + obj.Id
                };               
            }
            else
            {                
                objects[id].Object = obj;
                objects[id].Loaded = true;
                (objects[id].Object as StorableObject).Uri = Repo.BaseUri + "\\" + obj.Id;
            }*/
        }

        /// <summary>
        /// Deletes the object instance from the underlying data store and unloads it from memory. 
        /// </summary>
        /// <param name="obj">object to remove (uses Id)</param>
        public void Delete(T obj)
        {
            Delete(obj.Id);
        }

        /// <summary>
        /// Deletes the object instance from the underlying data store and unloads it from memory. 
        /// </summary>
        /// <param name="id">Id to remove.</param>
        public void Delete(string id)
        {
            if (objects.ContainsKey(id))
            {
                Repo.Delete(id);
                objects.Remove(id);
            }
        }

        public void Remove(string id)
        {
            if (objects.ContainsKey(id))
            {
                objects.Remove(id);
            }            
        }

        /// <summary>
        /// Refreshes list of available data objects. (Does quick load of base meta-data only, objects are lazy loaded on first access)
        /// </summary>
        public void Refresh()
        {
            var objs = Repo.GetIds();
            foreach (var obj in objs)
            {
                if (!objects.ContainsKey(obj.Id) || objects[obj.Id] == null || objects[obj.Id].Object == null)
                {
                    objects[obj.Id] = obj;
                }
            }
        }

        /// <summary>
        /// Saves all changed objects back to the configured repository. 
        /// </summary>
        public void Save()
        {
            foreach (var name in Names)
            {
                if (objects[name].Object.IsDirty)
                {
                    objects[name].Object.Save();
                }
            }
        }

        /// <summary>
        /// Load method for lazy loading of IStorable objects
        /// </summary>
        /// <param name="id">id to load</param>
        private void Load(string id)
        {
            if (!objects.ContainsKey(id) || objects[id] == null)
            {
                //JObject data;
                var obj = Repo.Get(id);
                (obj as StorableObject).SetParent(this);
                (obj as StorableObject).DataManager = DataManager;
                objects[id] = new StorableItemMetadata<T>() 
                { 
                    Id = id, 
                    Loaded = true,
                    Object = obj,  
                };
            }
            else if (!objects[id].Loaded || objects[id].Object == null)
            {
                //JObject data;
                objects[id].Object = Repo.Get(id);
                (objects[id].Object as StorableObject).Id = id;
                (objects[id].Object as StorableObject).SetParent(this);
                (objects[id].Object as StorableObject).DataManager = DataManager;
                objects[id].Loaded = true;
            }
        }
    }
}
