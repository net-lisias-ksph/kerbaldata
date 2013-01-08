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
    /// TODO: Interface Summary
    /// </summary>
    public interface IKspFileConverter
    {
        void Parse(StreamReader reader, KspDataContext context);
    }
}
