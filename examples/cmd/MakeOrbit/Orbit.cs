// -----------------------------------------------------------------------
// <copyright file="Orbit.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Apps.CommandLine.MakeOrbit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public class Orbit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Orbit" /> class.
        /// </summary>	
        public Orbit()
        {
        }

        public string SMA { get; set; }
        public string ECC { get; set; }
        public string INC { get; set; }
        public string LPE { get; set; }
        public string LAN { get; set; }
        public string MNA { get; set; }
        public string EPH { get; set; }
        public string REF { get; set; }
        public string OBJ { get; set; }
    }
}
