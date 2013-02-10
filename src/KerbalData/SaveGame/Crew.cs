// -----------------------------------------------------------------------
// <copyright file="Crew.cs" company="OpenSauceSolutions">
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
    /// Crew data 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Crew>))]
    public class Crew : KerbalDataObject 
    {
        private string name;
        private decimal brave, dumb, tod, index;
        private bool badS;
        private int state;

        /// <summary>
        /// Gets or sets the crew memeber's name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name", name);
            }
        }

        /// <summary>
        /// Gets or sets the bravery level. - File Property: brave
        /// </summary>
        [JsonProperty("brave")]
        public decimal Brave
        {
            get
            {
                return brave;
            }
            set
            {
                brave = value;
                OnPropertyChanged("Brave", brave);
            }
        }


        /// <summary>
        /// Gets or sets the dumbness level. - File Property: dumb
        /// </summary>
        [JsonProperty("dumb")]
        public decimal Dumb
        {
            get
            {
                return dumb;
            }
            set
            {
                dumb = value;
                OnPropertyChanged("Dumb", dumb);
            }
        }

        /// <summary>
        /// Gets or sets the BadS level. - File Property: badS
        /// </summary>
        [JsonProperty("badS")]
        public bool BadS
        {
            get
            {
                return badS;
            }
            set
            {
                badS = value;
                OnPropertyChanged("BadS", badS);
            }
        }

        /// <summary>
        /// Gets or sets the state. - File Property: state
        /// </summary>
        [JsonProperty("state")]
        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                OnPropertyChanged("State", state);
            }
        }

        /// <summary>
        /// Gets or sets the ToD. - File Property: ToD
        /// </summary>
        [JsonProperty("ToD")]
        public decimal ToD
        {
            get
            {
                return tod;
            }
            set
            {
                tod = value;
                OnPropertyChanged("ToD", tod);
            }
        }

        /// <summary>
        /// Gets or sets the index. - File Property: idx
        /// </summary>
        [JsonProperty("idx")]
        public decimal Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
                OnPropertyChanged("Index", index);
            }
        }
    }
}
