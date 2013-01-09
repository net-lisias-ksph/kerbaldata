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
    /// Instances of IKspDataConverter provide intenral KSP>JSON parsing. The interface will be used to swap out converters as the KSP data format evolves. Converter pattern may be made more general to provide support for any type of data format. 
    /// As long as KSP uses the ASCII format they have JSON will be hard-wired as a base "gateway" converter for all other converters. TODO: POC converter stack processor (publish to multiple endpoints/formats)
    /// </summary>
    public interface IKspDataConverter
    {
        /// <summary>
        /// Builds a JSON string using the given data context
        /// </summary>
        /// <param name="context">data context holding de-serilaized KSP data</param>
        /// <returns>JSON string veresion of provided data</returns>
        string BuildJson(KspDataContext context);

        /// <summary>
        /// Builds a KSP data string from JSON
        /// </summary>
        /// <param name="json">JSON string to convert</param>
        /// <returns>KSP string version of provided data</returns>
        string BuildKsp(string json);
    }
}
