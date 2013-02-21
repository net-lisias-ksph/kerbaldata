// -----------------------------------------------------------------------
// <copyright file="Orbit.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Orbit data model
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Orbit>))]
    public class Orbit : KerbalDataObject
    {
        private static readonly Dictionary<Body, decimal> offsets = new Dictionary<Body, decimal>()
        {
            { Body.Bop, 65000 },
            { Body.Dres, 140000 },
            { Body.Duna, 320000 },
            { Body.Eeloo, 210000 },
            { Body.Eve, 700000 },
            { Body.Gilly, 17000 },
            { Body.Ike, 130000 },
            { Body.Jool, 6000000 },
            { Body.Kerbin, 600000 },
            { Body.Kerbol, 499900000 },
            { Body.Laythe, 500000 },
            { Body.Minmus, 60000 },
            { Body.Moho, 250000 },
            { Body.Mun, 200000 },
            { Body.Pol, 45000 },
            { Body.Tylo, 600000 },
            { Body.Vall, 300000 }
        };

        private decimal sma, ecc, inc, lpe, lan, mna, eph, obj;
        private int reference;

        /// <summary>
        /// Gets the built in data set used for calculating offsets.
        /// </summary>
        /// <remarks>Data is not loaded from a file, this is how the <see cref="Orbit"/> class calculates offsets. This value is provided for end developers in making UI and input validation methods.</remarks>
        [JsonIgnore]
        public IDictionary<Body, decimal> AltitudeOffsets
        {
            get
            {
                return offsets;
            }
        }

        /// <summary>
        /// Gets or sets the reference body.
        /// This is the recommended method to update the reference body.
        /// </summary>
        public Body Body
        {
            get
            {
                return (Body)Ref;
            }
            set
            {
                var alt = Altitude;
                Ref = (int)value;
                OnPropertyChanged("Body", Body);
                Altitude = alt;
            }
        }

        /// <summary>
        /// Gets or sets the altitude above the reference body 
        /// </summary>
        /// <remarks>Using this value automatically handles the correct offsets and sets the Sma value as the game expects. Set/Get the desired altitude. Note: offsets do not take terrain into account and are set at ~sea level for each body.</remarks>
        [JsonIgnore]
        public decimal Altitude
        {
            get
            {
                return Sma - offsets[Body];
            }

            set
            {
                Sma = value + offsets[Body];
                OnPropertyChanged("Altitude", Sma - offsets[Body]);
            }
        }

        /// <summary>
        /// Gets or sets SMA. - File Property: SMA
        /// </summary>
        /// <remarks>for setting the craft's altitude the decimal property named Altitude is a better option</remarks>
        [JsonProperty("SMA")]
        public decimal Sma
        {
            get
            {
                return sma;
            }
            set
            {
                sma = value;
                OnPropertyChanged("Sma", sma);
            }
        }

        /// <summary>
        /// Gets or sets ECC. - File Property: ECC
        /// </summary>
        [JsonProperty("ECC")]
        public decimal Ecc
        {
            get
            {
                return ecc;
            }
            set
            {
                ecc = value;
                OnPropertyChanged("Ecc", ecc);
            }
        }

        /// <summary>
        /// Gets or sets INC. - File Property: INC
        /// </summary>
        [JsonProperty("INC")]
        public decimal Inc
        {
            get
            {
                return inc;
            }
            set
            {
                inc = value;
                OnPropertyChanged("Inc", inc);
            }
        }

        /// <summary>
        /// Gets or sets LPE. - File Property: LPE
        /// </summary>
        [JsonProperty("LPE")]
        public decimal Lpe
        {
            get
            {
                return lpe;
            }
            set
            {
                lpe = value;
                OnPropertyChanged("Lpe", lpe);
            }
        }

        /// <summary>
        /// Gets or sets LAN. - File Property: LAN
        /// </summary>
        [JsonProperty("LAN")]
        public decimal Lan
        {
            get
            {
                return lan;
            }
            set
            {
                lan = value;
                OnPropertyChanged("Lan", lan);
            }
        }

        /// <summary>
        /// Gets or sets mna. - File Property: MNA
        /// </summary>
        [JsonProperty("MNA")]
        public decimal Mna
        {
            get
            {
                return mna;
            }
            set
            {
                mna = value;
                OnPropertyChanged("Mna", mna);
            }
        }

        /// <summary>
        /// Gets or sets Eph. - File Property: EPH
        /// </summary>
        [JsonProperty("EPH")]
        public decimal Eph
        {
            get
            {
                return eph;
            }
            set
            {
                eph = value;
                OnPropertyChanged("Eph", eph);
            }
        }

        /// <summary>
        /// Gets or sets the reference body integer. - File Property: REF
        /// </summary>
        [JsonProperty("REF")]
        public int Ref
        {
            get
            {
                return reference;
            }
            set
            {
                reference = value;
                OnPropertyChanged("Ref", reference);
                DisplayName = "Orbit (" + Body + ")";
            }
        }

        /// <summary>
        /// Gets or sets the obj. - File Property: OBJ
        /// </summary>
        [JsonProperty("OBJ")]
        public decimal Obj
        {
            get
            {
                return obj;
            }
            set
            {
                obj = value;
                OnPropertyChanged("Obj", obj);
            }
        }

        /// <summary>
        /// Changes the orbit configuration based upon the provided body. Target orbit is ~100km from surface. 
        /// MET should be changed if more than one craft are placed in orbit around the same body. Otherwise craft may spawn too close and collide.
        /// </summary>
        /// <param name="body">desired body to orbit</param>
        /// <param name="altitude">desired altitude</param>
        public void Change(Body body, long altitude = 100000)
        {
            Body = body;
            Altitude = altitude;
        }
    }
}
