// -----------------------------------------------------------------------
// <copyright file="ActionGroups.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// ActionGroups assigned to the Vessel
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<ActionGroups>))]
    public class ActionGroups : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionGroups" /> class.
        /// </summary>
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
