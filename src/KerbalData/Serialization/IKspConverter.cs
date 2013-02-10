// -----------------------------------------------------------------------
// <copyright file="IIKspConverter.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Interface for Converter implmentations
    /// </summary>
    /// <remarks>
    /// Implmentations of this interface are responsible for converting internal DataContex definitions to desired objects types. 
    /// THis interim step in between serialization and data mapping allows for custom object type mappings used in highly advanced scenarios
    /// and to support core API use-cases.
    /// </remarks>
    /// <typeparam name="T">Model type handled by the converter instance</typeparam>
    public interface IKspConverter<T>
    {
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
        /// Converts data contex
        /// </summary>
        /// <param name="context">data context to convert</param>
        /// <returns>converted object instance of desired type</returns>
        T Convert(KspDataContext context);

        /// <summary>
        /// Converts data object
        /// </summary>
        /// <param name="obj">data object to convert</param>
        /// <returns>data context instance populated with object data</returns>
        KspDataContext Convert(T obj);

    }
}
