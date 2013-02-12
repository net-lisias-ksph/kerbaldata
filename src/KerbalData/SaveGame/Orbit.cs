// -----------------------------------------------------------------------
// <copyright file="Orbit.cs" company="OpenSauceSolutions">
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
    /// Orbit data model
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Orbit>))]
    public class Orbit : KerbalDataObject
    {
        private decimal sma, ecc, inc, lpe, lan, mna, eph, obj;
        private int reference;
        
        /// <summary>
        /// Gets or sets SMA. - File Property: SMA
        /// </summary>
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
        /// Gets or sets the reference body.
        /// This is the reccomended method to update the reference body.
        /// </summary>
        public Body Body
        {
            get
            {
                Body body = (Body)Ref;
                return body;
            }
            set
            {
                Ref = (int)value;
            }
        }

        /// <summary>
        /// Changes the orbit configuration based upon the provided body. Target orbit is ~100km from surface. 
        /// MET should be changed if more than one craft are placed in orbit around the same body. Otherwise craft may spawn too close and collide.
        /// </summary>
        /// <param name="body">desired body to orbit</param>
        /// <returns>true=success;false=failure</returns>
        public bool Change(Body body)
        {
            Body = body;
            // SMA = Height from center of body in meters
            // Adjusted to put vessel ~100km above each body
            switch (body)
            {
                case Body.Kerbol:
                    Sma = 500000000;
                    break;
                case Body.Kerbin:
                    Sma = 700000;
                    break;
                case Body.Mun:
                    Sma = 300000;
                    break;
                case Body.Minmus:
                    Sma = 160000;
                    break;
                case Body.Moho:
                    Sma = 350000;
                    break;
                case Body.Eve:
                    Sma = 800000;
                    break;
                case Body.Duna:
                    Sma = 420000;
                    break;
                case Body.Ike:
                    Sma = 230000;
                    break;
                case Body.Jool:
                    Sma = 6100000;
                    break;
                case Body.Laythe:
                    Sma = 600000;
                    break;
                case Body.Vall:
                    Sma = 400000;
                    break;
                case Body.Bop:
                    Sma = 165000;
                    break;
                case Body.Tylo:
                    Sma = 700000;
                    break;
                case Body.Gilly:
                    Sma = 117000;
                    break;
                case Body.Pol:
                    Sma = 145000;
                    break;
                case Body.Dres:
                    Sma = 240000;
                    break;
                case Body.Eeloo:
                    Sma = 310000;
                    break;
            }

            return true;
        }
    }
}
