// -----------------------------------------------------------------------
// <copyright file="ConfigFile.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a KSP configuration file.
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<ConfigFile>))]
    public class ConfigFile : StorableObject
    {
        // Current implmentation only provides strongly typed properties for core settings. Presets and Terrain collections have a high likelyhood of being migrated elsewhere in the future
        // they don't really belong in settings.cfg, it looks like a dumping ground for global settings ATM

        private string settingsVersion;
        private bool callHome, dontSendIp, checkForUpdates, showConsoleOnError, showSpaceCenterCrew, fullScreen, verboseDebugLog;
        private int autosaveInterval, autosaveShortInterval, uiSize, resloutionWidth, resloutionHeight, qualityPreset, 
            antiAliasing, textureQuality, lightQuality, shadowsQuality, framerateLimit, conicPatchDrawMode, conicPatchLimit;
        private TerrainConfig terrian;

        public ConfigFile()
            : base()
        {
            DisplayName = "Configuration";
        }

        /// <summary>
        /// Gets or sets the settings version. - File Property: SETTINGS_FILE_VERSION
        /// </summary>
        [JsonProperty("SETTINGS_FILE_VERSION")]
        public string SettingsVersion
        {
            get
            {
                return settingsVersion;
            }
            set
            {
                settingsVersion = value;
                OnPropertyChanged("SettingsVersion", settingsVersion);
            }
        }

        /// <summary>
        /// Gets or sets call home. - File Property: CALL_HOME
        /// </summary>
        [JsonProperty("CALL_HOME")]
        public bool CallHome
        {
            get
            {
                return callHome;
            }
            set
            {
                callHome = value;
                OnPropertyChanged("CallHome", callHome);
            }
        }

        /// <summary>
        /// Gets or sets DontSendIp flag. - File Property: VERBOSE_DEBUG_LOG
        /// </summary>
        [JsonProperty("DONT_SEND_IP")]
        public bool DontSendIp
        {
            get
            {
                return dontSendIp;
            }
            set
            {
                dontSendIp = value;
                OnPropertyChanged("DontSendIp", dontSendIp);
            }
        }

        /// <summary>
        /// Gets or sets check for updates flag. - File Property: VERBOSE_DEBUG_LOG
        /// </summary>
        [JsonProperty("CHECK_FOR_UPDATES")]
        public bool CheckForUpdates
        {
            get
            {
                return checkForUpdates;
            }
            set
            {
                checkForUpdates = value;
                OnPropertyChanged("CheckForUpdates", checkForUpdates);
            }
        }

        /// <summary>
        /// Gets or sets verbose debug log flag. - File Property: VERBOSE_DEBUG_LOG
        /// </summary>
        [JsonProperty("VERBOSE_DEBUG_LOG")]
        public bool VerboseDebugLog
        {
            get
            {
                return verboseDebugLog;
            }
            set
            {
                verboseDebugLog = value;
                OnPropertyChanged("VerboseDebugLog", VerboseDebugLog);
            }
        }

        /// <summary>
        /// Gets or sets show console on error flag. - File Property: SHOW_CONSOLE_ON_ERROR
        /// </summary>
        [JsonProperty("SHOW_CONSOLE_ON_ERROR")]
        public bool ShowConsoleOnError
        {
            get
            {
                return showConsoleOnError;
            }
            set
            {
                showConsoleOnError = value;
                OnPropertyChanged("ShowConsoleOnError", showConsoleOnError);
            }
        }

        /// <summary>
        /// Gets or sets autosave interval . - File Property: AUTOSAVE_INTERVAL
        /// </summary>
        [JsonProperty("AUTOSAVE_INTERVAL")]
        public int AutosaveInterval
        {
            get
            {
                return autosaveInterval;
            }
            set
            {
                autosaveInterval = value;
                OnPropertyChanged("AutosaveInterval", autosaveInterval);
            }
        }

        /// <summary>
        /// Gets or sets autosave short interval. - File Property: AUTOSAVE_SHORT_INTERVAL
        /// </summary>
        [JsonProperty("AUTOSAVE_SHORT_INTERVAL")]
        public int AutosaveShortInterval
        {
            get
            {
                return autosaveShortInterval;
            }
            set
            {
                autosaveShortInterval = value;
                OnPropertyChanged("AutosaveShortInterval", autosaveShortInterval);
            }
        }

        /// <summary>
        /// Gets or sets show space center flag. - File Property: SHOW_SPACE_CENTER_CREW
        /// </summary>
        [JsonProperty("SHOW_SPACE_CENTER_CREW")]
        public bool ShowSpaceCenterCrew
        {
            get
            {
                return showSpaceCenterCrew;
            }
            set
            {
                showSpaceCenterCrew = value;
                OnPropertyChanged("ShowSpaceCenterCrew", showSpaceCenterCrew);
            }
        }

        /// <summary>
        /// Gets or sets UI size. - File Property: UI_SIZE
        /// </summary>
        [JsonProperty("UI_SIZE")]
        public int UiSize
        {
            get
            {
                return uiSize;
            }
            set
            {
                uiSize = value;
                OnPropertyChanged("UiSize", uiSize);
            }
        }

        /// <summary>
        /// Gets or sets resloution width. - File Property: SCREEN_RESOLUTION_WIDTH
        /// </summary>
        [JsonProperty("SCREEN_RESOLUTION_WIDTH")]
        public int ResloutionWidth
        {
            get
            {
                return resloutionWidth;
            }
            set
            {
                resloutionWidth = value;
                OnPropertyChanged("ResloutionWidth", resloutionWidth);
            }
        }

        /// <summary>
        /// Gets or sets resloution height. - File Property: SCREEN_RESOLUTION_HEIGHT
        /// </summary>
        [JsonProperty("SCREEN_RESOLUTION_HEIGHT")]
        public int ResloutionHeight
        {
            get
            {
                return resloutionHeight;
            }
            set
            {
                resloutionHeight = value;
                OnPropertyChanged("ResloutionHeight", resloutionHeight);
            }
        }

        /// <summary>
        /// Gets or sets full screen flag. - File Property: FULLSCREEN
        /// </summary>
        [JsonProperty("FULLSCREEN")]
        public bool FullScreen
        {
            get
            {
                return fullScreen;
            }
            set
            {
                fullScreen = value;
                OnPropertyChanged("FullScreen", fullScreen);
            }
        }

        /// <summary>
        /// Gets or sets quality preset. - File Property: QUALITY_PRESET
        /// </summary>
        [JsonProperty("QUALITY_PRESET")]
        public int QualityPreset
        {
            get
            {
                return qualityPreset;
            }
            set
            {
                qualityPreset = value;
                OnPropertyChanged("QualityPreset", qualityPreset);
            }
        }

        /// <summary>
        /// Gets or sets antialiasing multiplier. - File Property: ANTI_ALIASING
        /// </summary>
        [JsonProperty("ANTI_ALIASING")]
        public int AntiAliasing
        {
            get
            {
                return antiAliasing;
            }
            set
            {
                antiAliasing = value;
                OnPropertyChanged("AntiAliasing", antiAliasing);
            }
        }

        /// <summary>
        /// Gets or sets texture quality. - File Property: TEXTURE_QUALITY
        /// </summary>
        [JsonProperty("TEXTURE_QUALITY")]
        public int TextureQuality
        {
            get
            {
                return textureQuality;
            }
            set
            {
                textureQuality = value;
                OnPropertyChanged("TextureQuality", textureQuality);
            }
        }

        /// <summary>
        /// Gets or sets light quality. - File Property: LIGHT_QUALITY
        /// </summary>
        [JsonProperty("LIGHT_QUALITY")]
        public int LightQuality
        {
            get
            {
                return lightQuality;
            }
            set
            {
                lightQuality = value;
                OnPropertyChanged("LightQuality", lightQuality);
            }
        }

        /// <summary>
        /// Gets or sets shadows quality. - File Property: SHADOWS_QUALITY
        /// </summary>
        [JsonProperty("SHADOWS_QUALITY")]
        public int ShadowsQuality
        {
            get
            {
                return shadowsQuality;
            }
            set
            {
                shadowsQuality = value;
                OnPropertyChanged("ShadowsQuality", shadowsQuality);
            }
        }

        /// <summary>
        /// Gets or sets framerate limit. - File Property: FRAMERATE_LIMIT
        /// </summary>
        [JsonProperty("FRAMERATE_LIMIT")]
        public int FramerateLimit
        {
            get
            {
                return framerateLimit;
            }
            set
            {
                framerateLimit = value;
                OnPropertyChanged("FramerateLimit", framerateLimit);
            }
        }

        /// <summary>
        /// Gets or sets conic patch draw mode. - File Property: CONIC_PATCH_DRAW_MODE
        /// </summary>
        [JsonProperty("CONIC_PATCH_DRAW_MODE")]
        public int ConicPatchDrawMode
        {
            get
            {
                return conicPatchDrawMode;
            }
            set
            {
                conicPatchDrawMode = value;
                OnPropertyChanged("ConicPatchDrawMode", conicPatchDrawMode);
            }
        }

        /// <summary>
        /// Gets or sets conic patch limit. - File Property: CONIC_PATCH_LIMIT
        /// </summary>
        [JsonProperty("CONIC_PATCH_LIMIT")]
        public int ConicPatchLimit
        {
            get
            {
                return conicPatchLimit;
            }
            set
            {
                conicPatchLimit = value;
                OnPropertyChanged("ConicPatchLimit", conicPatchLimit);
            }
        }

        /// <summary>
        /// Gets or sets terrain configuration. - File Property: TERRAIN
        /// </summary>
        [JsonProperty("TERRAIN")]
        public TerrainConfig Terrain
        {
            get
            {
                return terrian;
            }
            set
            {
                terrian = value;
                OnPropertyChanged("Terrain", terrian);
            }
        }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            // TODO: Implementation
        }
    }
}
