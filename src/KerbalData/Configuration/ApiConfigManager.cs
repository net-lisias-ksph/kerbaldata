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
    /// Configuration loading wrapper
    /// </summary>
    internal static class ApiConfigManager 
    {
        private static IDictionary<string, ApiConfig> configurations = new Dictionary<string, ApiConfig>();

        /// <summary>
        /// Retrieves a configuration section
        /// </summary>
        /// <param name="configSectionName">name of configuration to retrieve</param>
        /// <returns>de-serialized configuration section</returns>
        public static ApiConfig GetConfig(string configSectionName = "kerbalData")
        {
            if (!configurations.ContainsKey(configSectionName))
            {
                var apiConfig = (ApiConfig)ConfigurationManager.GetSection(configSectionName);

                if (apiConfig != null)
                {
                    configurations[configSectionName] = apiConfig;
                }
                else
                {
                    return null; 
                }
            }

            return configurations[configSectionName];
        }
    }
}
