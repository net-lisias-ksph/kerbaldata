// -----------------------------------------------------------------------
// <copyright file="TrackingStationGameParameters.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Model for tracking station params in the save
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<TrackingStationGameParameters>))]
    public class TrackingStationGameParameters : KerbalDataObject
    {
        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);
            DisplayName = "Tracking Station (" + base.Count + ")";
        }
    }
}
