// -----------------------------------------------------------------------
// <copyright file="IIStorableObjects.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System.Collections.Generic;

    /// <summary>
    /// Base contract for a class that can manage multiple <see cref="IStorable"/> instances includes requirements to provide end-user methods
    /// </summary>
    public interface IStorableObjects
    {
        /// <summary>
        /// Gets the data manager instance used by the instance
        /// </summary>
        IKerbalDataManager DataManager { get; }

        /// <summary>
        /// Gets the number of 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the Names of objects handled by the instance (used for lazy-loading)
        /// </summary>
        IEnumerable<string> Names { get; }

        /// <summary>
        /// Checks the names collection for the provided id
        /// </summary>
        /// <param name="id">name to look for</param>
        /// <returns>true=exists;false=does not exist</returns>
        bool ContainsId(string id);

        /// <summary>
        /// Deletes the item from memory and from it's base data-store
        /// </summary>
        /// <param name="id">name to delete</param>
        void Delete(string id);

        /// <summary>
        /// Refreshes the repo name list/data from the repository
        /// </summary>
        void Refresh();

        /// <summary>
        /// Saves all child instances
        /// </summary>
        void Save();

        /// <summary>
        /// Adds an object instance to the collection
        /// </summary>
        /// <param name="data">data to add</param>
        /// <param name="id">name to give data</param>
        void Add(object data, string id = null);

        /// <summary>
        /// Removes the data object from the collection but does not delete the underlying data from the repository
        /// </summary>
        /// <param name="id"></param>
        void Remove(string id);
    }
}
