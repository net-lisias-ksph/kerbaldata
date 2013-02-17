// -----------------------------------------------------------------------
// <copyright file="IKerbalDataRepo.cs" company="OpenSauceSolutions">
// � 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Requirements for data loading repository. Additonal implmentations may be provided to store/load KSP data from any desired store. 
    /// </summary>
    /// <typeparam name="T">model type being stored</typeparam>
    public interface IKerbalDataRepo<T> where T : class, IStorable, new()
    {
        /// <summary>
        /// Gets the base Uri of the repository
        /// </summary>
        string BaseUri { get; }

        /// <summary>
        /// Gets the backup before save flag
        /// </summary>
        bool BackupBeforeSave { get; }

        /// <summary>
        /// Gets all data elements available to the repository 
        /// </summary>
        /// <returns>all available data</returns>
        IList<T> Get();

        /// <summary>
        /// Gets all data elements available to the repository 
        /// </summary>
        /// <param name="data">base data definition // TODO: break JSON.NET dependency</param>
        /// <returns>all availablee data</returns>
        //IList<T> Get(out IList<JObject> data);

        /// <summary>
        /// Gets meta-data for all data elements (used for quick loading of needed data to lookup elements without de-serializing all data.
        /// </summary>
        /// <returns>meta-data for all elements</returns>
        IList<StorableItemMetadata<T>> GetIds();

        /// <summary>
        /// Gets all data elements available to the repository that match the predicate
        /// </summary>
        /// <param name="predicate">filter to use</param>
        /// <returns>data matching requirement</returns>
        IList<T> Get(Func<T, bool> predicate);

        /// <summary>
        /// Gets all data elements available to the repository that match the predicate
        /// </summary>
        /// <param name="predicate">filter to use</param>
        /// <param name="data">base data definition // TODO: break JSON.NET dependency</param>
        /// <returns>data matching requirement</returns>
        //IList<T> Get(Func<T, bool> predicate, out IList<JObject> data);

        /// <summary>
        /// Gets data element by Id/Name
        /// </summary>
        /// <param name="id">Id/Name to retrieve</param>
        /// <returns>requested data element</returns>
        T Get(string id);

        /// <summary>
        /// Gets data element by Id/Name
        /// </summary>
        /// <param name="id">Id/Name to retrieve</param>
        /// <param name="data">base data definition // TODO: break JSON.NET dependency</param>
        /// <returns>requested data element</returns>
        //T Get(string id, out JObject data);

        /// <summary>
        /// Puts/Updates a data elemetn
        /// </summary>
        /// <param name="id">id/name to update</param>
        /// <param name="obj">value to update with</param>
        /// <returns>true=success;false=failure;</returns>
        bool Put(string id, T obj);

        /// <summary>
        /// Deeletes the data element by Id/name
        /// </summary>
        /// <param name="Id">Id/name to delete</param>
        /// <returns>true=success;false=failure;</returns>
        bool Delete(string Id);
    }
}
