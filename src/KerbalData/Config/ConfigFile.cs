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

        /// <summary>
        /// Gets or sets the settings version. - File Property: SETTINGS_FILE_VERSION
        /// </summary>
        [JsonProperty("SETTINGS_FILE_VERSION")]
        public string SettingsVersion { get; set; }

        /// <summary>
        /// Gets or sets call home. - File Property: CALL_HOME
        /// </summary>
        [JsonProperty("CALL_HOME")]
        public bool CallHome { get; set; }

        /// <summary>
        /// Gets or sets DontSendIp flag. - File Property: VERBOSE_DEBUG_LOG
        /// </summary>
        [JsonProperty("DONT_SEND_IP")]
        public bool DontSendIp { get; set; }

        /// <summary>
        /// Gets or sets check for updates flag. - File Property: VERBOSE_DEBUG_LOG
        /// </summary>
        [JsonProperty("CHECK_FOR_UPDATES")]
        public bool CheckForUpdtes { get; set; }

        /// <summary>
        /// Gets or sets verbose debug log flag. - File Property: VERBOSE_DEBUG_LOG
        /// </summary>
        [JsonProperty("VERBOSE_DEBUG_LOG")]
        public bool VerboseDebugLog { get; set; }

        /// <summary>
        /// Gets or sets show console on error flag. - File Property: SHOW_CONSOLE_ON_ERROR
        /// </summary>
        [JsonProperty("SHOW_CONSOLE_ON_ERROR")]
        public bool ShowConsoleOnError { get; set; }

        /// <summary>
        /// Gets or sets autosave interval . - File Property: AUTOSAVE_INTERVAL
        /// </summary>
        [JsonProperty("AUTOSAVE_INTERVAL")]
        public int AutosaveInterval { get; set; }

        /// <summary>
        /// Gets or sets autosave short interval. - File Property: AUTOSAVE_SHORT_INTERVAL
        /// </summary>
        [JsonProperty("AUTOSAVE_SHORT_INTERVAL")]
        public int AutosaveShortInterval { get; set; }

        /// <summary>
        /// Gets or sets show space center flag. - File Property: SHOW_SPACE_CENTER_CREW
        /// </summary>
        [JsonProperty("SHOW_SPACE_CENTER_CREW")]
        public bool ShowSpaceCenterCrew { get; set; }

        /// <summary>
        /// Gets or sets UI size. - File Property: UI_SIZE
        /// </summary>
        [JsonProperty("UI_SIZE")]
        public int UiSize { get; set; }

        /// <summary>
        /// Gets or sets resloution width. - File Property: SCREEN_RESOLUTION_WIDTH
        /// </summary>
        [JsonProperty("SCREEN_RESOLUTION_WIDTH")]
        public int ResloutionWidth { get; set; }

        /// <summary>
        /// Gets or sets resloution height. - File Property: SCREEN_RESOLUTION_HEIGHT
        /// </summary>
        [JsonProperty("SCREEN_RESOLUTION_HEIGHT")]
        public int ResloutionHeight { get; set; }

        /// <summary>
        /// Gets or sets full screen flag. - File Property: FULLSCREEN
        /// </summary>
        [JsonProperty("FULLSCREEN")]
        public bool FullScreen { get; set; }

        /// <summary>
        /// Gets or sets quality preset. - File Property: QUALITY_PRESET
        /// </summary>
        [JsonProperty("QUALITY_PRESET")]
        public int QualityPreset { get; set; }

        /// <summary>
        /// Gets or sets antialiasing multiplier. - File Property: ANTI_ALIASING
        /// </summary>
        [JsonProperty("ANTI_ALIASING")]
        public int AntiAliasing { get; set; }

        /// <summary>
        /// Gets or sets texture quality. - File Property: TEXTURE_QUALITY
        /// </summary>
        [JsonProperty("TEXTURE_QUALITY")]
        public int TextureQuality { get; set; }

        /// <summary>
        /// Gets or sets light quality. - File Property: LIGHT_QUALITY
        /// </summary>
        [JsonProperty("LIGHT_QUALITY")]
        public int LightQuality { get; set; }

        /// <summary>
        /// Gets or sets shadows quality. - File Property: SHADOWS_QUALITY
        /// </summary>
        [JsonProperty("SHADOWS_QUALITY")]
        public int ShadowsQuality { get; set; }

        /// <summary>
        /// Gets or sets framerate limit. - File Property: FRAMERATE_LIMIT
        /// </summary>
        [JsonProperty("FRAMERATE_LIMIT")]
        public int FramerateLimit { get; set; }

        /// <summary>
        /// Gets or sets conic patch draw mode. - File Property: CONIC_PATCH_DRAW_MODE
        /// </summary>
        [JsonProperty("CONIC_PATCH_DRAW_MODE")]
        public int ConicPatchDrawMode { get; set; }

        /// <summary>
        /// Gets or sets conic patch limit. - File Property: CONIC_PATCH_LIMIT
        /// </summary>
        [JsonProperty("CONIC_PATCH_LIMIT")]
        public int ConicPatchLimit { get; set; }

        /// <summary>
        /// Gets or sets terrain configuration. - File Property: TERRAIN
        /// </summary>
        [JsonProperty("TERRAIN")]
        public TerrainConfig Terrain { get; set; }

        /// <summary>
        /// Restores the object to the state it was in when it's data was first loaded or last saved. 
        /// </summary>
        public override void Revert()
        {
            // TODO: Implementation
        }
    }
}
