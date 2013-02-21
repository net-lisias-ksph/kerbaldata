// -----------------------------------------------------------------------
// <copyright file="KspDataObject.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System.Collections.Generic;

    /// <summary>
    /// Core data context object used for data serilization/de-serilaization. Only implementation build on IKspDataConverter or IKspFileConverter
    /// </summary>
    public sealed class KspDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KspDataObject" /> class.
        /// </summary>	
        public KspDataObject()
        {
            Values = new List<KspDataField>();
            Children = new List<KspDataObject>();
        }

        /// <summary>
        /// Gets or sets the name of the object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the field values of the object
        /// </summary>
        public List<KspDataField> Values { get; set; }

        /// <summary>
        /// Gets or sets the child objects for this instance
        /// </summary>
        public List<KspDataObject> Children { get; set; }
    }
}
