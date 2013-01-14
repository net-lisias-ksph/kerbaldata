// -----------------------------------------------------------------------
// <copyright file="IKspFileConverter.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Instances of this class are responsible for de-serialzing KSP data.
    /// </summary>
    public interface IKspFileConverter
    {
        /// <summary>
        /// Accepts stream of formatted data and de-serializes the data into the provided context.
        /// </summary>
        /// <param name="reader">data to de-serialize</param>
        /// <param name="context">context instance to populate</param>
        void Parse(StreamReader reader, KspDataContext context);
    }
}
