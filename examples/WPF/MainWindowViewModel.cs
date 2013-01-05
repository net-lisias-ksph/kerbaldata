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
        private IList<dynamic> vessels;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>	
        public MainWindowViewModel()
        {
            try
            {
                var jobj = KspData.LoadFile(@"\Data\Saves\KspPersistentSfswMods.sfs").ToObject<dynamic>();

                vessels = jobj.GAME.FLIGHTSTATE.VESSEL.ToObject<IList<dynamic>>();
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
            }
            
        }

        public IList<dynamic> Vessels {
            get
            {
                return vessels;
            }
            set
            {
                vessels = value;
            }
        }
    }
}
