// -----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KSPEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json.Linq;

    using KerbalData;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public class MainWindowViewModel
    {
        private KerbalData kd;
        private string saveName = "testing"; // TODO: Change to your game or wire in UI to accept user input. 

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>	
        public MainWindowViewModel()
        {
            try
            {
                // TODO: Enter your path or wire in UI to accept user path
                kd = new KerbalData(@"C:\games\KSP_win_test");
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
            }
        }

        public IList<Vessel> Vessels {
            get
            {
                return kd.Saves[saveName].Game.FlightState.Vessels;
            }
            set
            {
                kd.Saves[saveName].Game.FlightState.Vessels = value;
            }
        }
    }
}
