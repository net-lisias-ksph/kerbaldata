using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KerbalData;
using System.IO;

namespace KerbalData.Apps.CommandLine.MakeOrbit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() < 4)
            {
                Console.WriteLine("The MakeOrbit command requires 3 parameters. The path of your save, the name of the flight you wish to orbit and the code of the reference body you wish to orbit");
                return;
            }

            var kspPath = args[0].Trim();
            var gameName = args[1].Trim();
            var vesselName = args[2].Trim();
            var refBody = args[3].Trim().ToUpper();

            var kdm = new KerbalDataManager(kspPath);
            var gdm = kdm.Games[gameName];
            var vessel = gdm.Vessels.Data.Where(v => v["name"].ToString() == vesselName).FirstOrDefault();

            var orbit = vessel["ORBIT"];

            // 0 degree orbit heading for 090
            orbit["ECC"] = "0";
            orbit["EPH"] = "39.9399991072715";
            orbit["INC"] = "180";
            orbit["LAN"] = "81.2190688665883";
            orbit["LPE"] = "340.518331561772";
            orbit["MNA"] = "2.11222363842244";
            orbit["OBJ"] = "0";

            // SMA = Height from center of body in meters
            // Adjusted to put vessel ~100km above each body
            switch (refBody)
            {
                case "KERBOL":
                    orbit["REF"] = "0";
                    orbit["SMA"] = "500000000";
                    break;
                case "KERBIN":
                    orbit["REF"] = "1";
                    orbit["SMA"] = "700000";
                    break;
                case "MUN":
                    orbit["REF"] = "2";
                    orbit["SMA"] = "300000";
                    break;
                case "MINMUS":
                    orbit["REF"] = "3";
                    orbit["SMA"] = "160000";
                    break;
                case "MOHO":
                    orbit["REF"] = "4";
                    orbit["SMA"] = "350000";
                    break;
                case "EVE":
                    orbit["REF"] = "5";
                    orbit["SMA"] = "800000";
                    break;
                case "DUNA":
                    orbit["REF"] = "6";
                    orbit["SMA"] = "420000";
                    break;
                case "IKE":
                    orbit["REF"] = "7";
                    orbit["SMA"] = "230000";
                    break;
                case "JOOL":
                    orbit["REF"] = "8";
                    orbit["SMA"] = "6100000";
                    break;
                case "LAYTHE":
                    orbit["REF"] = "9";
                    orbit["SMA"] = "600000";
                    break;
                case "VALL":
                    orbit["REF"] = "10";
                    orbit["SMA"] = "400000";
                    break;
                case "BOP":
                    orbit["REF"] = "11";
                    orbit["SMA"] = "165000";
                    break;
                case "TYLO":
                    orbit["REF"] = "12";
                    orbit["SMA"] = "700000";
                    break;
                case "GILLY":
                    orbit["REF"] = "13";
                    orbit["SMA"] = "117000";
                    break;
                case "POL":
                    orbit["REF"] = "14";
                    orbit["SMA"] = "145000";
                    break;
                case "DRES":
                    orbit["REF"] = "15";
                    orbit["SMA"] = "240000";
                    break;
                case "EELOO":
                    orbit["REF"] = "16";
                    orbit["SMA"] = "310000";
                    break;
                default:
                    {
                        int refVal;
                        if (!int.TryParse(refBody, out refVal))
                        {
                            Console.WriteLine("Unknown reference body");
                            return;
                        }

                        orbit["REF"] = refBody;
                        orbit["SMA"] = "1000000";

                        break;
                    }
            }

            orbit["SMA"] = args.Count() == 5 ? args[4] : orbit["SMA"];

            vessel["ORBIT"] = orbit;

            gdm.SaveData();
        }
    }
}
