// -----------------------------------------------------------------------
// <copyright file="ApiConfigManager.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    internal static class ApiConfigManager 
    {
        private static IDictionary<string, ApiConfig> configurations = new Dictionary<string, ApiConfig>();

        public static ApiConfig GetConfig(string configSectionName = "kerbalData")
        {
            if (!configurations.ContainsKey(configSectionName))
            {
                try
                {
                    configurations[configSectionName] = (ApiConfig)ConfigurationManager.GetSection(configSectionName);
                }
                catch (Exception ex)
                {
                    // If exceptuion .NET confugration cannot locate the configuration or it is not properly formatted.
                    return null;
                }
            }

            return configurations[configSectionName];
        }
    }
}
