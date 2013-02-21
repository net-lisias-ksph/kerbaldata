// -----------------------------------------------------------------------
// <copyright file="StorableItemMetadata.cs" company="OpenSauceSolutions">
// � 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    /// <summary>
    /// Meta-data wrapper for storable data. Used for lazy loading. 
    /// </summary>
    public class StorableItemMetadata<T> where T : class, IStorable, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorableItemMetadata{T}" /> class.
        /// </summary>	
        public StorableItemMetadata()
        {
            Loaded = false;
        }

        /// <summary>
        /// Gets the Id/Name of data object
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the Absolute URI to data element
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Gets the loaded flag true=loaded;false=not loaded. Used for lazy loading
        /// </summary>
        public bool Loaded { get; set; }

        /// <summary>
        /// Object instance, null when not loaded
        /// </summary>
        public T Object { get; set; }
    }
}
