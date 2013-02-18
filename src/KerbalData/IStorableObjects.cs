// -----------------------------------------------------------------------
// <copyright file="IIStorableObjects.cs" company="OpenSauceSolutions">
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
    /// TODO: Interface Summary
    /// </summary>
    public interface IStorableObjects
    {
        IKerbalDataManager DataManager { get; }
        int Count { get; }
        IEnumerable<string> Names { get; }
        bool ContainsId(string id);
        void Delete(string id);
        void Refresh();
        void Save();
    }
}
