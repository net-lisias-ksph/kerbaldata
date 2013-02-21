// -----------------------------------------------------------------------
// <copyright file="FlightGameParameters.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Model container for game flight options
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<FlightGameParameters>))]
    public class FlightGameParameters : KerbalDataObject
    {
        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);
            DisplayName = "Flight (" + Count + ")";
        }
    }
}
