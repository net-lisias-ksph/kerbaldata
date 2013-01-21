// -----------------------------------------------------------------------
// <copyright file="RepoFactory.cs" company="OpenSauceSolutions">
// � 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Basic reposiotry placeholder
    /// </summary>
    public static class RepoFactory
    {
        /// <summary>
        /// Creates a new reposiotry instance
        /// </summary>
        /// <typeparam name="T">model type to be managed by the repository</typeparam>
        /// <param name="parameters">paramerter data used to init the reposiotry</param>
        /// <returns>new repository instance for the requested type</returns>
        public static IKerbalDataRepo<T> Create<T>(object[] parameters = null) where T : class, IStorable, new()
        {
            const string repoName = "KerbalData.DataProviders.KspInstallFileRepo`1, KerbalData.DataProviders";

            var repoType = Type.GetType(repoName).MakeGenericType(new Type[] { typeof(T) }); 
            var repo = repoType.GetConstructor(new Type[] { typeof(object[]) }).Invoke(new object[] { parameters });
            // TODO: Configuration and loading framework for Converters. Doing it this way for the time being so I can start
            // Pulling out JSON.NET dependencies on the core framework and to prevent adding new dependcies just for data converson
            // needs
            return repo as IKerbalDataRepo<T>;
        }
    }
}
