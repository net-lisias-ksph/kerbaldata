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
    /// TODO: Class Summary
    /// </summary>
    public sealed class KspDataContext
    {
        public KspDataContext()
        {
            Data = new KspDataObject();
        }

        public KspDataObject Data { get; internal set; }
    }
}
