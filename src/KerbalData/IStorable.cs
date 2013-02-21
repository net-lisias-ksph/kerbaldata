// -----------------------------------------------------------------------
// <copyright file="IStorable.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a data element that can be saved as a unit. 
    /// </summary>
    public interface IStorable : IKerbalDataObject
    {
        /// <summary>
        /// Gets the Original data 
        /// <see href="http://james.newtonking.com/projects/json/help/?topic=html/T_Newtonsoft_Json_Linq_JObject.htm" target="_blank" alt="Newtonsoft.Json.Linq.JToken">Newtonsoft.Json.Linq.JToken</seealso>
        /// </summary>
        JObject Original { get; }

        /// <summary>
        /// Gets the id/name of the data
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the uri the data was retrieved from
        /// </summary>
        string Uri { get; }

        /// <summary>
        /// Gets if the object has been altered since is was originally loaded or last saved
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// Gets the data manager instance responsible for this object
        /// </summary>
        IKerbalDataManager DataManager { get; }

        /// <summary>
        /// Saves the object 
        /// </summary>
        /// <param name="id">id to save, using a new id creates a new file // TODO: WORK FLOW Clone? Orignal object remain changed? or revert?</param>
        /// <param name="backup">backup the data on save, may be overridden by repository in use</param>
        /// <returns>true=success;false=failure</returns>
        bool Save(string id = null, bool backup = true);

        /// <summary>
        /// Clones this object and it's underlying data to a new instance of the same type. 
        /// </summary>
        /// <typeparam name="T">type to convert to</typeparam>
        /// <returns>cloned instance</returns>
        T Clone<T>() where T : class, IStorable, new();
    }
}
