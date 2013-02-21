// -----------------------------------------------------------------------
// <copyright file="EditorGameParameters.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Model for Editor prefs
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<EditorGameParameters>))]
    public class EditorGameParameters : KerbalDataObject
    {
        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);
            DisplayName = "Editor (" + Count + ")";
        }
    }
}
