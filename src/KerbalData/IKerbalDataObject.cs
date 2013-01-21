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
    /// Base data object. Included dictionary requirment to provide storage for root level values not mapped to strongly typed properties. 
    /// </summary>
    public interface IKerbalDataObject : IDictionary<string, JToken>
    {
    }
}
