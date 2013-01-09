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
    /// Management wrapper for Crew  List JArray
    /// </summary>
    public class CrewDataManager
    {
        private JArray crew;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrewDataManager" /> class.
        /// </summary>	
        /// <param name="crew">JArray value containing collection of crew objects</param>
        public CrewDataManager(JToken crew)
        {
            this.crew = crew as JArray;
        }

        /// <summary>
        /// Crew List JSON Data Object
        /// </summary>
        public JArray Data
        {
            get
            {
                return crew;
            }
        }
    }
}
