// -----------------------------------------------------------------------
// <copyright file="IKerbalDataObject.cs" company="OpenSauceSolutions">
// � 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Base data object. Included dictionary requirement to provide storage for root level values not mapped to strongly typed properties. 
    /// <seealso href="http://james.newtonking.com/projects/json/help/?topic=html/T_Newtonsoft_Json_Linq_JToken.htm" target="_blank" alt="Newtonsoft.Json.Linq.JToken">Newtonsoft.Json.Linq.JToken</seealso>
    /// </summary>
    public interface IKerbalDataObject : IDictionary<string, JToken>
    {
        string DisplayName { get; }
    }
}
