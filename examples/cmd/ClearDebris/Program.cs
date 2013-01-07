using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KerbalData;
using Newtonsoft.Json.Linq;

namespace ClearDebris
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() < 1)
            {
                Console.WriteLine("The MakeOrbit command requires 3 parameters. The path of your save, the name of the flight you wish to orbit and the code of the reference body you wish to orbit");
                return;
            }

            var savePath = args[0];
            //var vesselName = args[1];
            //var refBody = args[2].Trim().ToUpper();

            var obj = KspData.LoadFile(savePath);
            KspData.SaveFile(savePath + "-BACKUP-" + DateTime.Now.ToString("yyyyMMdd_hhmmss"), obj); //Backup before editing. 

            var vessels = obj["GAME"]["FLIGHTSTATE"]["VESSEL"];
            var unknownVessels = vessels.Where(v => v["name"].ToString().ToUpper().Contains("UNKNOWN"));

            Console.WriteLine("Unknown Vessels");
            Console.WriteLine("---------------");

            var removed = 0;
            for (var i = 0; i < vessels.Count(); i++)
            {
                var vessel = vessels[i];
                if (vessel["name"].ToString().ToUpper().Contains("UNKNOWN"))
                {
                    Console.WriteLine("Name: " + vessel["name"] + " Orbiting: " + vessel["ORBIT"]["REF"]);
                    vessel.Remove();

                    removed++;
                }
            }

            Console.WriteLine("Removed " + removed + " craft from " + savePath);

            unknownVessels = vessels.Where(v => v["name"].ToString().ToUpper().Contains("UNKNOWN"));
            
            KspData.SaveFile("testdata.sfs", obj);
        }
    }
}
