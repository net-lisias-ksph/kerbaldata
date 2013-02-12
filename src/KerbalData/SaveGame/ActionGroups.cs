// -----------------------------------------------------------------------
// <copyright file="ActionGroups.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// ActionGroups assigned to the Vessel
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<ActionGroups>))]
    public class ActionGroups : KerbalDataObject
    {
        public ActionGroups()
            : base()
        {
            DisplayName = "Actions (" + base.Count + ")";
        }

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);
            DisplayName = "Actions (" + base.Count + ")";
        }

    }
}
