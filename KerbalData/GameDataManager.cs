
// -----------------------------------------------------------------------
// <copyright file="GameDataManager.cs" company="OpenSauceSolutions">
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

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public class GameDataManager
    {
        private string filePath;
        private bool isDataLoaded = false;

        private JToken orignalGame;
        private JObject game;
        private CrewDataManager crew;
        private VesselListDataManager vessels;

        /// <summary>
        /// Loads a game data manager and protects consumer from lower level exceptions/problems
        /// </summary>
        /// <param name="path">file path of desired file to load</param>
        /// <param name="gdm">GameDataManager instance to use</param>
        /// <returns>true=success;false=failure gdm will be null</returns>
        public static bool TryLoad(string path, out GameDataManager gdm)
        {
            try
            {
                gdm = new GameDataManager(path);
            }
            catch (Exception ex) // TODO: Less mallet more config/finesse
            {
                gdm = null;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDataManager" /> class.
        /// </summary>	
        public GameDataManager(string filePath)
        {
            this.filePath = filePath;
        }

        public GameDataManager(JObject game)
        {
            InitManagers(game);
            isDataLoaded = true;
        }

        public JObject Data
        {
            get
            {
                DataInit();
                return game;
            }
        }

        public JToken OrignalData
        {
            get
            {
                DataInit();
                return orignalGame;
            }
        }

        public CrewDataManager Crew
        {
            get
            {
                DataInit();
                return crew;
            }
        }

        public VesselListDataManager Vessels
        {
            get
            {
                DataInit();
                return vessels;
            }
        }

        public JToken Parameters
        {
            get
            {
                DataInit();
                return game["GAME"]["PARAMETERS"];
            }
        }

        public JToken FlightState
        {
            get
            {
                DataInit();
                return game["GAME"]["FLIGHTSTATE"];
            }
        }

        public string Title
        {
            get
            {
                DataInit();
                return game["GAME"]["Title"].ToString();
            }
        }

        public string Version
        {
            get
            {
                DataInit();
                return game["GAME"]["version"].ToString();
            }
        }

        public bool SaveData(string filePath = null, bool backupOrignal = true)
        {
            var savePath = !string.IsNullOrEmpty(filePath) ? filePath : this.filePath;

            if (File.Exists(savePath) && backupOrignal)
            {
                KspData.SaveFile(savePath + "-BACKUP-" + DateTime.Now.ToString("yyyyMMdd_hhmmss"), orignalGame); 
            }

            KspData.SaveFile(savePath, game); 

            return true;
        }

        public void Revert()
        {
            game = orignalGame.DeepClone().ToObject<JObject>();
        }

        private void DataInit()
        {
            if (!isDataLoaded)
            {
                InitManagers(KspData.LoadFile(filePath));
            }
        }

        private void InitManagers(JObject obj)
        {
            game = obj;
            orignalGame = game.DeepClone();

            isDataLoaded = true;

            crew = new CrewDataManager(FlightState["CREW"]);
            vessels = new VesselListDataManager(FlightState["VESSEL"]);
        }

    }
}
