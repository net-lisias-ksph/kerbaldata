// -----------------------------------------------------------------------
// <copyright file="Orbit.cs" company="OpenSauceSolutions">
// � 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    /// 
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Orbit>))]
    public class Orbit : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Orbit" /> class.
        /// </summary>	
        public Orbit()
        {
        }

        [JsonProperty("SMA")]
        public decimal Sma { get; set; }

        [JsonProperty("ECC")]
        public decimal Ecc { get; set; }

        [JsonProperty("INC")]
        public decimal Inc { get; set; }

        [JsonProperty("LPE")]
        public decimal Lpe { get; set; }

        [JsonProperty("LAN")]
        public decimal Lan { get; set; }

        [JsonProperty("MNA")]
        public decimal Mna { get; set; }

        [JsonProperty("EPH")]
        public decimal Eph { get; set; }

        [JsonProperty("REF")]
        public int Ref { get; set; }

        [JsonProperty("OBJ")]
        public decimal Obj { get; set; }

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
