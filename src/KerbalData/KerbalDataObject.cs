// -----------------------------------------------------------------------
// <copyright file="KerbalDataObject.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Base implmentation for consumer data model. Provides wireup override for base dictionary. Used for storing properties that have not been mapped.
    /// <seealso href="http://james.newtonking.com/projects/json/help/?topic=html/T_Newtonsoft_Json_Linq_JToken.htm" target="_blank" alt="Newtonsoft.Json.Linq.JToken">Newtonsoft.Json.Linq.JToken</seealso>
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    [JsonObject]
    public class KerbalDataObject : Dictionary<string, JToken>, IKerbalDataObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataObject" /> class.
        /// </summary>	
        public KerbalDataObject() : base()
        {
        }

        [JsonIgnore]
        public new JToken this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
                OnPropertyChanged(key, base[key]);
            }
        }

        /// <summary>
        /// Gets the keys for unmapped properties stored by this object
        /// </summary>
        [JsonIgnore]
        public new KeyCollection Keys
        {
            get { return base.Keys; }
        }

        /// <summary>
        /// Gets the values collection
        /// </summary>
        [JsonIgnore]
        public new ValueCollection Values
        {
            get { return base.Values; }
        }

        /// <summary>
        /// Gets the comparer
        /// </summary>
        [JsonIgnore]
        public new IEqualityComparer<string> Comparer
        {
            get { return base.Comparer; }
        }

        /// <summary>
        /// Gets the count of unmapped elements
        /// </summary>
        [JsonIgnore]
        public new int Count
        {
            get { return base.Count; }
        }

        protected void OnPropertyChanged(string propertyName, object value = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            /*
            if (ParentObj != null && !IsNotificationDisabled)
            {
                if (PropertyFilters != null && !PropertyFilters.Contains(propertyName))
                {
                    return;
                }

                ParentObj.OnChildPropertyChanged(propertyName, value);
            }*/
        }
    }
}
