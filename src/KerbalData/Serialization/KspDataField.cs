// -----------------------------------------------------------------------
// <copyright file="KspDataField.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    /// <summary>
    /// Model class used by KspDataObject as a key component in data serilaization/de-serialization.
    /// </summary>
    public sealed class KspDataField
    {
        /// <summary>
        /// Gets or sets the name of the field
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the field
        /// </summary>
        public string Value { get; set; }
    }
}
