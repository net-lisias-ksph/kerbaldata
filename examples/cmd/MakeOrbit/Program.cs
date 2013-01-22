namespace KerbalData.Apps.CommandLine.MakeOrbit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
            var body = (Body)Enum.Parse(typeof(Body), args[3].Trim());


            var kd = new KerbalData(kspPath);
            var save = kd.Saves[gameName];
            var vessel = save.Game.FlightState.Vessels.Where(v => v["name"].ToString() == vesselName).FirstOrDefault();

            vessel.Orbit.Change(body);

            save.Save();
        }
    }
}
