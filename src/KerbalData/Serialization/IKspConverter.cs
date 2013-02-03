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
    /// TODO: Interface Summary
    /// </summary>
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

        T Convert(KspDataContext context);
        KspDataContext Convert(T obj);

    }
}
