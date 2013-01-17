// -----------------------------------------------------------------------
// <copyright file="RepoFactory.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public static class RepoFactory
    {
        // TODO: True Factory Impl
        public static IKerbalDataRepo<T> Create<T>(object[] parameters = null) where T : class, IStorable, new()
        {
            return new KspInstallFileRepo<T>(parameters);
        }
    }
}
