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

            var obj = KspData.LoadFile(savePath);
            KspData.SaveFile(savePath + "-BACKUP-" + DateTime.Now.ToString("yyyyMMdd_hhmmss"), obj); //Backup before editing. 

            var count = obj["GAME"]["FLIGHTSTATE"]["VESSEL"].RemoveChildren(o => o["type"].ToString() == "Unknown" || o["type"].ToString() == "Debris");

            Console.WriteLine("Removed " + count + " vessels.");

            KspData.SaveFile("testdata.sfs", obj);
        }
    }
}
