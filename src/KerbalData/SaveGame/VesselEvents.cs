// -----------------------------------------------------------------------
// <copyright file="VesselEvents.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Model for vessel events
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<VesselEvents>))]
    public class VesselEvents : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VesselEvents" /> class.
        /// </summary>
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
