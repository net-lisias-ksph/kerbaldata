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
    /// Top Level manager class
    /// </summary>
    public class KerbalDataManager
    {
        private string gamePath;

        private Dictionary<string, GameDataManager> games = new Dictionary<string,GameDataManager>(); // To cache loaded games in memory or to not load them?
        private bool isGameDataLoaded = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataManager" /> class.
        /// </summary>	
        public KerbalDataManager(string gamePath = @"C:\KSP\")
        {
            this.gamePath = gamePath;
        }

        public IDictionary<string, GameDataManager> Games
        {
            get
            {
                InitGames();
                return games;
            }
        }

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
