// -----------------------------------------------------------------------
// <copyright file="IKerbalDataRepo.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Newtonsoft.Json.Linq;

    /// <summary>
    /// TODO: Interface Summary
    /// </summary>
    public interface IKerbalDataRepo<T> where T : class, IStorable, new()
    {
        // TODO Async Delegate parameters
        string BaseUri { get; }

        bool BackupBeforeSave { get; }

        IList<T> Get();

        IList<T> Get(out IList<JObject> data);

        IList<StorableItemMetadata<T>> GetIds();

        IList<T> Get(Func<T, bool> func);

        IList<T> Get(Func<T, bool> func, out IList<JObject> data);

        T Get(string id);

        T Get(string id, out JObject data);

        bool Put(string Id, T obj);

        bool Delete(string Id);

    }


}
