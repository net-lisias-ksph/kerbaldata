// -----------------------------------------------------------------------
// <copyright file="CrewDataManager.cs" company="OpenSauceSolutions">
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
    public class CrewDataManager
    {
        private JArray crew;
        /// <summary>
        /// Initializes a new instance of the <see cref="CrewDataManager" /> class.
        /// </summary>	
        public CrewDataManager(JToken crew)
        {
            this.crew = crew as JArray;
        }

        public JArray Data
        {
            get
            {
                return crew;
            }
        }
    }
}
