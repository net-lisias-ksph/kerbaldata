// -----------------------------------------------------------------------
// <copyright file="IKerbalDataObject.cs" company="OpenSauceSolutions">
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
    /// TODO: Class Summary
    /// </summary>
    public interface IKerbalDataObject : IDictionary<string, JToken>
    {
       // IDictionary<string, JToken> Properties { get; set; }
    }
}
