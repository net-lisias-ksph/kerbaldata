// -----------------------------------------------------------------------
// <copyright file="IKspDataSerializer.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serialization
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Base serialization class interface. Provides support for parsing and writing KSP data files. 
    /// </summary>
    public interface IKspSerializer
    {
        /// <summary>
        /// Gets the value indicating whether the implementation can serialize KSP data for the supported versions
        /// </summary>
        bool CanSerialize { get; }

        /// <summary>
        /// Gets the value indicating whether the implementation can sde-serialize KSP data for the supported versions
        /// </summary>
        bool CanDeSerialize { get; }

        /// <summary>
        /// Gets the list of KSP versions this serialize supports
        /// </summary>
        IList<string> Versions { get; }

        /// <summary>
        /// Tells if a specified version is supported by the implementation
        /// </summary>
        /// <param name="version">KSP version to check</param>
        /// <returns>true=supported;false=not supported</returns>
        bool SupportsVersion(string version);

        /// <summary>
        /// Serializes the provided context into the writer
        /// </summary>
        /// <param name="context">data context to use</param>
        /// <param name="writer">writer to populate</param>
        void Serialize(KspDataContext context, StreamWriter writer);

        /// <summary>
        /// De-Serializes the provided reader into context. 
        /// </summary>
        /// <param name="context">data context to populate</param>
        /// <param name="reader">reader to use</param>
        void DeSerialize(KspDataContext context, StreamReader reader);
    }
}
