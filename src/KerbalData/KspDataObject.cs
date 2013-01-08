// -----------------------------------------------------------------------
// <copyright file="KspDataObject.cs" company="OpenSauceSolutions">
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
    public sealed class KspDataObject
    {
        public KspDataObject()
        {
            Values = new List<KspDataField>();
            Children = new List<KspDataObject>();
        }

        public string Name { get; set; }
        public List<KspDataField> Values { get; set; }
        public List<KspDataObject> Children { get; set; }
        //public string TabSpacer { get; set; }
    }
}
