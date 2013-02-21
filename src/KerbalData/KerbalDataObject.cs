// -----------------------------------------------------------------------
// <copyright file="KerbalDataObject.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System.ComponentModel;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Base implementation for consumer data model. Provides wire up override for base dictionary. Used for storing properties that have not been mapped.
    /// <seealso href="http://james.newtonking.com/projects/json/help/?topic=html/T_Newtonsoft_Json_Linq_JToken.htm" target="_blank" alt="Newtonsoft.Json.Linq.JToken">Newtonsoft.Json.Linq.JToken</seealso>
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    [JsonObject]
    public class KerbalDataObject : ObservableDictionary<string, JToken>, IKerbalDataObject, INotifyPropertyChanged
    {
        private string displayName;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataObject" /> class.
        /// </summary>	
        public KerbalDataObject() : base()
        {
        }

        /// <summary>
        /// Get the display name of the instance
        /// </summary>
        [JsonIgnore]
        public virtual string DisplayName
        {
            get
            {
                return displayName;
            }

            protected set
            {
                displayName = value;
                OnPropertyChanged("DisplayName", displayName);
            }
        }

        protected void OnPropertyChanged(string propertyName, object value = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
