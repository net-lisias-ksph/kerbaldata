// -----------------------------------------------------------------------
// <copyright file="Vessel.cs" company="OpenSauceSolutions">
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
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Vessel data model
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Vessel>))]
    public class Vessel : KerbalDataObject
    {
        private string pid, name, type, situation,landedAt, reference;
        private bool landed, splashed, prst, eva;
        private decimal met, lct, latitude, longitude, altitude, hgt;
        private decimal[] nrm, rot;
        private float[] com;
        private int stage, root;
        private Orbit orbit;
        private IList<Part> parts;
        private ActionGroups actionGroups;

        /// <summary>
        /// Gets or sets the Pid. - File Property: GAME
        /// </summary>
        [JsonProperty("pid")]
        public string Pid
        {
            get
            {
                return pid;
            }
            set
            {
                pid = value;
                OnPropertyChanged("Pid", pid);
            }
        }

        /// <summary>
        /// Gets or sets the name. - File Property: GAME
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
        /// Gets or sets the type. - File Property: GAME
        /// </summary>
        [JsonProperty("type")]
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged("Type", type);
            }
        }

        /// <summary>
        /// Gets or sets the situation. - File Property: GAME
        /// </summary>
        [JsonProperty("sit")]
        public string Situation
        {
            get
            {
                return situation;
            }
            set
            {
                situation = value;
                OnPropertyChanged("Situation", situation);
            }
        }

        /// <summary>
        /// Gets or sets the landed flag. - File Property: GAME
        /// </summary>
        [JsonProperty("landed")]
        public bool Landed
        {
            get
            {
                return landed;
            }
            set
            {
                landed = value;
                OnPropertyChanged("Landed", landed);
            }
        }

        /// <summary>
        /// Gets or sets the landed at value. - File Property: GAME
        /// </summary>
        [JsonProperty("landedAt")]
        public string LandedAt
        {
            get
            {
                return landedAt;
            }
            set
            {
                landedAt = value;
                OnPropertyChanged("LandedAt", landedAt);
            }
        }

        /// <summary>
        /// Gets or sets the splashed flag. - File Property: GAME
        /// </summary>
        [JsonProperty("splashed")]
        public bool Splashed
        {
            get
            {
                return splashed;
            }
            set
            {
                splashed = value;
                OnPropertyChanged("Splashed", splashed);
            }
        }

        /// <summary>
        /// Gets or sets the Met. - File Property: GAME
        /// </summary>
        [JsonProperty("met")]
        public decimal Met
        {
            get
            {
                return met;
            }
            set
            {
                met = value;
                OnPropertyChanged("Met", met);
            }
        }

        /// <summary>
        /// Gets or sets Mission Elapsed time
        /// </summary>
        public TimeSpan MissionElapsedTime
        {
            get
            {
                return new TimeSpan((long)Met * 1000000L);
            }
            set
            {
                Met = value.Ticks / 1000000L;
            }
        }

        /// <summary>
        /// Gets or sets lct. - File Property: lct
        /// </summary>
        [JsonProperty("lct")]
        public decimal Lct
        {
            get
            {
                return lct;
            }
            set
            {
                lct = value;
                OnPropertyChanged("Lct", lct);
            }
        }

        /// <summary>
        /// Gets or sets root. - File Property: root
        /// </summary>
        [JsonProperty("root")]
        public int Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
                OnPropertyChanged("Root", root);
            }
        }

        /// <summary>
        /// Gets or sets latitude. - File Property: lat
        /// </summary>
        [JsonProperty("lat")]
        public decimal Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude", latitude);
            }
        }

        /// <summary>
        /// Gets or sets longitude. - File Property: lon
        /// </summary>
        [JsonProperty("lon")]
        public decimal Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude", longitude);
            }
        }

        /// <summary>
        /// Gets or sets altitude. - File Property: alt
        /// </summary>
        [JsonProperty("alt")]
        public decimal Altitude
        {
            get
            {
                return altitude;
            }
            set
            {
                altitude = value;
                OnPropertyChanged("Altitude", altitude);
            }
        }

        /// <summary>
        /// Gets or sets hgt. - File Property: hgt
        /// </summary>
        [JsonProperty("hgt")]
        public decimal Hgt
        {
            get
            {
                return hgt;
            }
            set
            {
                hgt = value;
                OnPropertyChanged("Hgt", hgt);
            }
        }

        /// <summary>
        /// Gets or sets nrm. - File Property: nrm
        /// </summary>
        [JsonProperty("nrm")]
        public decimal[] Nrm
        {
            get
            {
                return nrm;
            }
            set
            {
                nrm = value;
                OnPropertyChanged("Nrm", nrm);
            }
        }
        
        /// <summary>
        /// Gets or sets Rot. - File Property: rot
        /// </summary>
        [JsonProperty("rot")]
        public decimal[] Rot
        {
            get
            {
                return rot;
            }
            set
            {
                rot = value;
                OnPropertyChanged("Rot", rot);
            }
        }

        /// <summary>
        /// Gets or sets Com. - File Property: CoM
        /// </summary>
        [JsonProperty("CoM")]
        public float[] Com
        {
            get
            {
                return com;
            }
            set
            {
                com = value;
                OnPropertyChanged("Com", com);
            }
        }

        /// <summary>
        /// Gets or sets stage. - File Property: stg
        /// </summary>
        [JsonProperty("stg")]
        public int Stage
        {
            get
            {
                return stage;
            }
            set
            {
                stage = value;
                OnPropertyChanged("Stage", stage);
            }
        }

        /// <summary>
        /// Gets or sets prst. - File Property: Prst
        /// </summary>
        [JsonProperty("prst")]
        public bool Prst
        {
            get
            {
                return prst;
            }
            set
            {
                prst = value;
                OnPropertyChanged("Prst", prst);
            }
        }

        /// <summary>
        /// Gets or sets eva. - File Property: Eva
        /// </summary>
        [JsonProperty("eva")]
        public bool Eva
        {
            get
            {
                return eva;
            }
            set
            {
                eva = value;
                OnPropertyChanged("Eva", eva);
            }
        }

        /// <summary>
        /// Gets or sets ref. - File Property: ref
        /// </summary>
        [JsonProperty("ref")]
        public string Ref
        {
            get
            {
                return reference;
            }
            set
            {
                reference = value;
                OnPropertyChanged("Ref", reference);
            }
        }

        /// <summary>
        /// Gets or sets orbit. - File Property: ORBIT
        /// </summary>
        [JsonProperty("ORBIT")]
        public Orbit Orbit
        {
            get
            {
                return orbit;
            }
            set
            {
                orbit = value;
                OnPropertyChanged("Orbit", orbit);
            }
        }

        /// <summary>
        /// Gets or sets part collection. - File Property: PART
        /// </summary>
        [JsonProperty("PART")]
        public IList<Part> Parts
        {
            get
            {
                return parts;
            }
            set
            {
                parts = value;
                OnPropertyChanged("Parts", parts);
            }
        }

        /// <summary>
        /// Gets or sets vessel action groups. - File Property: ACTIONGROUPS
        /// </summary>
        [JsonProperty("ACTIONGROUPS")]
        public ActionGroups ActionGroups
        {
            get
            {
                return actionGroups;
            }
            set
            {
                actionGroups = value;
                OnPropertyChanged("ActionGroups", actionGroups);
            }
        }

        /// <summary>
        /// Fills all resource parts in this vessel
        /// </summary>
        /// <returns>number of resource parts filed</returns>
        public int FillResources()
        {
            if (Parts == null)
            {
                return 0;
            }

            var count = 0;

            foreach (var res in Parts)
            {
                res.FillResources();
                count++;
            }

            return count;
        }

        /// <summary>
        /// Emppties all resources in this vessel
        /// </summary>
        /// <returns>number of parts emptied</returns>
        public int EmptyResources()
        {
            if (Parts == null)
            {
                return 0;
            }

            var count = 0;

            foreach (var res in Parts)
            {
                res.EmptyResources();
                count++;
            }

            return count;
        }

    }
}
