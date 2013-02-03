// -----------------------------------------------------------------------
// <copyright file="IKspDataSerializer.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Base serilaization class interface. Provides support for parsing and writing KSP data files. 
    /// </summary>
    public interface IKspSerializer
    {
        /// <summary>
        /// Gets the value indicating whether the implmentation can serialize KSP data for the supported versions
        /// </summary>
        bool CanSerialize { get; }

        /// <summary>
        /// Gets the value indicating whether the implmentation can sde-erialize KSP data for the supported versions
        /// </summary>
        bool CanDeSerialize { get; }

        /// <summary>
        /// Gets the list of KSP versions this serializer supports
        /// </summary>
        IList<string> Versions { get; }

        /// <summary>
        /// Tells if a specified version is supported by the implmentation
        /// </summary>
        /// <param name="version">KSP version to check</param>
        /// <returns>true=supported;false=notsupported</returns>
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
