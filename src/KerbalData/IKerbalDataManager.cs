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
    /// TODO: Interface Summary
    /// </summary>
    public interface IKerbalDataManager
    {
        string BaseUri { get; }
        ApiConfig ApiConfig { get; }
        RepoFactory RepositoryFactory { get; }
        ProcessorRegistry ProcRegistry { get; }
    }
}
