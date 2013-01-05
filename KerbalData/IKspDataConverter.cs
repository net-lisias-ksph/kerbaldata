// -----------------------------------------------------------------------
// <copyright file="IKspDataConnverter.cs" company="OpenSauceSolutions">
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
    public interface IKspDataConverter
    {
        string BuildJson(KspDataContext context);
        string BuildKsp(string json);
    }
}
