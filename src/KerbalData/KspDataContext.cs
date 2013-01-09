// -----------------------------------------------------------------------
// <copyright file="KspDataContext.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Model object provided to de-serializer/serilaizer classes for defining the basic structure of the KSP data prior to conversion to JSON
    /// </summary>
    public sealed class KspDataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KspDataContext" /> class.
        /// </summary>	
        public KspDataContext()
        {
            Data = new KspDataObject();
        }

        /// <summary>
        /// Gets the Data object for this contenxt. This property is where de-serialized data is stored prior to conversion to JSON
        /// </summary>
        public KspDataObject Data { get; internal set; }
    }
}
