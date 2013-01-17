// -----------------------------------------------------------------------
// <copyright file="IStorable.cs" company="OpenSauceSolutions">
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
    public interface IStorable
    {
        JObject Original { get; }

        string Id { get; }

        string Uri { get; }

        bool IsDirty { get; }

        //void SetParent<T>(StorableObjects<T> parent) where T : class, IStorable, new();

        bool Save(string id = null, bool backup = true);

        T Clone<T>() where T : class, IStorable, new();
    }
}
