// -----------------------------------------------------------------------
// <copyright file="VesselEvents.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Model for vessel events
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<VesselEvents>))]
    public class VesselEvents : KerbalDataObject
    {
        public VesselEvents()
            : base()
        {
            DisplayName = "Events (" + base.Count + ")";
        }

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);
            DisplayName = "Events (" + base.Count + ")";
        }
    }
}
