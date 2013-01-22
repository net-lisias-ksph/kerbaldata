namespace KerbalData.Apps.CommandLine.ClearDebris
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
            if (args.Count() != 2)
            {
                Console.WriteLine("The ClearDebris command requires 2 parameters. The path of your KSP install, the name of your save game");
                return;
            }

            var kspPath = args[0].Trim();
            var gameName = args[1].Trim();

            var kd = new KerbalData(kspPath);
            var save = kd.Saves[gameName];

            Console.WriteLine("Removed " + save.Game.FlightState.ClearDebris() + " vessels.");

            save.Save();
        }
    }
}
