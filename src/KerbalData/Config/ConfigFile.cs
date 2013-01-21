// -----------------------------------------------------------------------
// <copyright file="ConfigFile.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Newtonsoft.Json;

    /// <summary>
    /// Represents a KSP configuration file.
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartFile>))]
    public class ConfigFile : StorableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFile" /> class.
        /// </summary>	
        public ConfigFile()
        {
        }

        // Current implmentation only provides strongly typed properties for core settings. Presets and Terrain collections have a high likely hood of being migrated elsewhere in the future
        // they don't really belong in settings.cfg, it looks like a dumping ground for global settings ATM

        [JsonProperty("SETTINGS_FILE_VERSION")]
        public string SettingsVersion { get; set; }

        [JsonProperty("CALL_HOME")]
        public bool CallHome { get; set; }

        [JsonProperty("DONT_SEND_IP")]
        public bool DontSendIp { get; set; }

        [JsonProperty("CHECK_FOR_UPDATES")]
        public bool CheckForUpdtes { get; set; }

        [JsonProperty("VERBOSE_DEBUG_LOG")]
        public bool VerboseDebugLog { get; set; }

        [JsonProperty("SHOW_CONSOLE_ON_ERROR")]
        public bool ShowConsoleOnError { get; set; }

        [JsonProperty("AUTOSAVE_INTERVAL")]
        public int AutosaveInterval { get; set; }

        [JsonProperty("AUTOSAVE_SHORT_INTERVAL")]
        public int AutosaveShortInterval { get; set; }

        [JsonProperty("SHOW_SPACE_CENTER_CREW")]
        public bool ShowSpaceCenterCrew { get; set; }

        [JsonProperty("UI_SIZE")]
        public int UiSize { get; set; }

        [JsonProperty("SCREEN_RESOLUTION_WIDTH")]
        public int ResloutionWidth { get; set; }

        [JsonProperty("SCREEN_RESOLUTION_HEIGHT")]
        public int ResloutionHeight { get; set; }

        [JsonProperty("FULLSCREEN")]
        public bool FullScreen { get; set; }

        [JsonProperty("QUALITY_PRESET")]
        public int QualityPreset { get; set; }

        [JsonProperty("ANTI_ALIASING")]
        public int AntiAliasing { get; set; }

        [JsonProperty("TEXTURE_QUALITY")]
        public int TextureQuality { get; set; }

        [JsonProperty("LIGHT_QUALITY")]
        public int LightQuality { get; set; }

        [JsonProperty("SHADOWS_QUALITY")]
        public int ShadowsQuality { get; set; }

        [JsonProperty("FRAMERATE_LIMIT")]
        public int FramerateLimit { get; set; }

        [JsonProperty("CONIC_PATCH_DRAW_MODE")]
        public int ConicPatchDrawMode { get; set; }

        [JsonProperty("CONIC_PATCH_LIMIT")]
        public int ConicPatchLimit { get; set; }

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
