// -----------------------------------------------------------------------
// <copyright file="VesselActions.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Vessel actions 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<VesselActions>))]
    public class VesselActions : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VesselActions" /> class.
        /// </summary>
        public VesselActions()
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
