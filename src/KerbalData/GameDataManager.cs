
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
    /// Top level wrapper for GAME JObject. Provides methods for loading KSP data directly and quick access to key areas of the JSON document. 
    /// Class lazy loads data. First request to any getter will trigger a document load. 
    /// </summary>
    public class GameDataManager
    {
        private string filePath;
        private bool isDataLoaded = false; // Lazy loading

        private JToken orignalGame;
        private JObject game;
        private CrewDataManager crew;
        private VesselListDataManager vessels;

        /// <summary>
        /// Loads a game data manager and protects consumer from lower level exceptions/problems.
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
            catch (Exception ex) // TODO: Better exception handling across codebase. 
            {
                gdm = null;
                return false;
            }

            return true;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GameDataManager" /> class.
        /// </summary>
	    /// <param name="filePath">path of KSP game data file - typically a file with the extension "sfs"</param>
        public GameDataManager(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDataManager" /> class.
        /// </summary>
        /// <param name="game">deserialized JOBject containing a game definition. The data layout must be compatible with this API's JSON formatting. TODO: More details on this</param>
        public GameDataManager(JObject game)
        {
            InitManagers(game);
            isDataLoaded = true;
        }

        /// <summary>
        /// Gets the file path used to load this isntance's data. If null or empty the instance was loaded from a JOBject in memory.
        /// </summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }
        }

        /// <summary>
        /// Gets the current data handled by the manager
        /// </summary>
        public JObject Data
        {
            get
            {
                DataInit();
                return game;
            }
        }

        /// <summary>
        /// Gets the orignal data provided to the manager. 
        /// Upon data load the GameDataManager executes a JToken.DeepClone() operation and stores the orignal state of the JSON data. This allows for comparision (for quciker save and auto-save style routines) and revert changes.  
        /// </summary>
        public JToken OrignalData
        {
            get
            {
                DataInit();
                return orignalGame;
            }
        }

        /// <summary>
        /// Gets the crew mananger using the crew array managed by this class.
        /// </summary>
        public CrewDataManager Crew
        {
            get
            {
                DataInit();
                return crew;
            }
        }

        /// <summary>
        /// Gets the vessel list mananger using the crew array managed by this class.
        /// </summary>
        public VesselListDataManager Vessels
        {
            get
            {
                DataInit();
                return vessels;
            }
        }

        /// <summary>
        /// Gets the Parameters data object stored in this classes data. IE: game["GAME"]["PARAMETERS"]
        /// </summary>
        public JToken Parameters
        {
            get
            {
                DataInit();
                return game["GAME"]["PARAMETERS"];
            }
        }

        /// <summary>
        /// Gets the FlightState data object stored in this classes data. IE: game["GAME"]["FLIGHTSTATE"]
        /// </summary>
        public JToken FlightState
        {
            get
            {
                DataInit();
                return game["GAME"]["FLIGHTSTATE"];
            }
        }

        /// <summary>
        /// Gets the title stored in this classes data. IE: game["GAME"]["Title"]
        /// </summary>
        public string Title
        {
            get
            {
                DataInit();
                return game["GAME"]["Title"].ToString();
            }
        }

        /// <summary>
        /// Gets the title stored in this classes data. IE: game["GAME"]["Version"]
        /// </summary>
        public string Version
        {
            get
            {
                DataInit();
                return game["GAME"]["version"].ToString();
            }
        }

        /// <summary>
        /// Saves the current data state to the file specified. 
        /// </summary>
        /// <param name="filePath">defaults to path data is loaded from unless loaded by jobject where an execption is thrown if a path is not provided. The FilePath property can be used to determine if an instance was loaded from a file or a JObject</param>
        /// <param name="backupOrignal">defaults to true. creates a backup of the filePath used (if the file exists) before saving. Uses the format "filename.ext-BACKUP-yyyyMMdd_hhmmss"</param>
        /// <returns></returns>
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

        /// <summary>
        /// Reverts the state of the data to the orignal state as provided at object creation. Uses JToken.DeepClone() to preserve the orginal data while re-setting the data to edit. 
        /// </summary>
        public void Revert()
        {
            game = orignalGame.DeepClone().ToObject<JObject>();
        }

        /// <summary>
        /// Data lazy loading start. Each property getter calls this method before executing. InitMangers handles the state of isDataLoaded
        /// </summary>
        private void DataInit()
        {
            if (!isDataLoaded)
            {
                InitManagers(KspData.LoadFile(filePath));
            }
        }

        /// <summary>
        /// Loads the proivided data into property references for consumption. 
        /// </summary>
        /// <param name="obj">loaded data object instance</param>
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
