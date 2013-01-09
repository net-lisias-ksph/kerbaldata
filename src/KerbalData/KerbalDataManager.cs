// -----------------------------------------------------------------------
// <copyright file="KerbalDataManager.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Manager for all data in a particular game install. When provided with an install path of a valid KSP game this class will make availble and parse all save, craft, part and config files in KSP Data format
    /// </summary>
    public class KerbalDataManager
    {
        private string gamePath;

        private Dictionary<string, GameDataManager> games = new Dictionary<string,GameDataManager>(); // To cache loaded games in memory or to not load them?
        private bool isGameDataLoaded = false; // Lazy loading

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataManager" /> class.
        /// </summary>	
        public KerbalDataManager(string gamePath = @"C:\KSP\")
        {
            this.gamePath = gamePath;
        }

        /// <summary>
        /// Gets the save games available in this KSP installation
        /// </summary>
        public IDictionary<string, GameDataManager> Games
        {
            get
            {
                InitGames();
                return games;
            }
        }

        /// <summary>
        /// Internal data init
        /// </summary>
        private void InitGames()
        {
            if (!isGameDataLoaded)
            {
                var gameDir = new DirectoryInfo(gamePath);
                
                if (gameDir.GetDirectories().Where(d => d.Name == "saves").Count() == 1
                    && gameDir.GetDirectories().Where(d => d.Name == "saves").ElementAt(0).GetDirectories().Count() > 0)
                {
                    LoadGameSaves(gameDir.GetDirectories().Where(d => d.Name == "saves").ElementAt(0));
                }

                isGameDataLoaded = true;
            }
        }

        /// <summary>
        /// Loads games found in subfolders. 
        /// </summary>
        /// <param name="dirInfo">directory to use</param>
        private void LoadGameSaves(DirectoryInfo dirInfo)
        {
            foreach (var saveDir in dirInfo.GetDirectories())
            {
                GameDataManager gdm;

                if (GameDataManager.TryLoad(saveDir.FullName + "\\persistent.sfs", out gdm))
                {
                    games.Add(saveDir.Name, gdm);
                }
            }
        }

    }
}
