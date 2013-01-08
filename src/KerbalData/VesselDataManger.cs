// -----------------------------------------------------------------------
// <copyright file="VesselDataManger.cs" company="OpenSauceSolutions">
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
    public class VesselDataManger
    {
        private JObject vessel;

        /// <summary>
        /// Initializes a new instance of the <see cref="VesselDataManger" /> class.
        /// </summary>	
        public VesselDataManger(JToken vessel)
        {
            this.vessel = vessel as JObject;
        }

        public JObject Data
        {
            get
            {
                return vessel;
            }
        }

        /// <summary>
        /// Refuels all craft resources.
        /// </summary>
        public void Refuel()
        {
            var parts = vessel["PART"].Where(p => p.SelectToken("RESOURCE") != null);

            for (var i = 0; i < parts.Count(); i++)
            {
                var part = parts.ElementAt(i);

                if (part["RESOURCE"] is JArray)
                {
                    for (var c = 0; c < part["RESOURCE"].Count(); c++)
                    {
                        var resource = (part["RESOURCE"] as JArray)[c];
                        resource["amount"] = resource["maxAmount"].ToString();
                    }
                }
                else
                {
                    var resource = part["RESOURCE"];
                    resource["amount"] = resource["maxAmount"].ToString();
                }
            }
        }
    }
}
