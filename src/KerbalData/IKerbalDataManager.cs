// -----------------------------------------------------------------------
// <copyright file="IKerbalDataManager.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Configuration;

    /// <summary>
    /// Base interface for top-level consumer API classes. Allows consumers to create custom models that leverage API features.
    /// </summary>
    public interface IKerbalDataManager
    {
        /// <summary>
        /// Gets the Base data URI location of resource store
        /// </summary>
        string BaseUri { get; }

        /// <summary>
        /// Gets the configuration used by this instance  
        /// </summary>
        ApiConfig ApiConfig { get; }

        /// <summary>
        /// Gets the configured <see cref="RepoFactory"/ instance>
        /// </summary>
        RepoFactory RepositoryFactory { get; }

        /// <summary>
        /// Gets the configured <see cref="ProcessorRegistry"/>
        /// </summary>
        ProcessorRegistry ProcRegistry { get; }
    }
}
