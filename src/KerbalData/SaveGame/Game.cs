// -----------------------------------------------------------------------
// <copyright file="Game.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Game data model
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Game>))]
    public class Game : KerbalDataObject
    {
        private string version;
        private string title;
        private string description;
        private int mode;
        private int status;
        private int scene;
        private GameParameters gameParameters;
        private FlightState flightState;

        /// <summary>
        /// Gets or sets the version. - File Property: version
        /// </summary>
        [JsonProperty("version")]
        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                OnPropertyChanged("Version", version);
            }
        }

        /// <summary>
        /// Gets or sets the title. - File Property: Title
        /// </summary>
        [JsonProperty("Title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title", title);
            }
        }

        /// <summary>
        /// Gets or sets the description. - File Property: Description
        /// </summary>
        [JsonProperty("Description")]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description", description);
            }
        }

        /// <summary>
        /// Gets or sets the mode. - File Property: Mode
        /// </summary>
        [JsonProperty("Mode")]
        public int Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
                OnPropertyChanged("Mode", mode);
            }
        }

        /// <summary>
        /// Gets or sets the status. - File Property: Status
        /// </summary>
        [JsonProperty("Status")]
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status", status);
            }
        }

        /// <summary>
        /// Gets or sets the scene. - File Property: scene
        /// </summary>
        [JsonProperty("scene")]
        public int Scene
        {
            get
            {
                return scene;
            }
            set
            {
                scene = value;
                OnPropertyChanged("Scene", scene);
            }
        }

        /// <summary>
        /// Gets or sets game parameters. - File Property: PARAMETERS
        /// </summary>
        [JsonProperty("PARAMETERS")]
        public GameParameters Parameters
        {
            get
            {
                return gameParameters;
            }
            set
            {
                gameParameters = value;
                OnPropertyChanged("Parameters", gameParameters);
            }
        }

        /// <summary>
        /// Gets or sets the flight state. - File Property: FLIGHTSTATE
        /// </summary>
        [JsonProperty("FLIGHTSTATE")]
        public FlightState FlightState
        {
            get
            {
                return flightState;
            }
            set
            {
                flightState = value;
                OnPropertyChanged("FlightState", flightState);
            }
        }
    }
}
