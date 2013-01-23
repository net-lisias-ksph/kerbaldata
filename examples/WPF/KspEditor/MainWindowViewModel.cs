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
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Property Name constants
        const string PropNameKerbalData = "KerbalData";
        const string PropNameInstallPath = "InstallPath";

        const string PropNameSelectedSave = "SelectedSave";
        const string PropNameSelectedScenario = "SelectedScenario";
        const string PropNameSelectedTrainingScenario = "SelectedTrainingScenario";
        const string PropNameSelectedVabCraft = "SelectedVabCraft";
        const string PropNameSelectedSphCraft = "SelectedSphCraft";
        const string PropNameSelectedPart = "SelectedPart";
        const string PropNameSelectedSetting = "SelectedSetting";

        const string PropNameDisplaySave = "DisplaySave";
        const string PropNameDisplayScenario = "DisplayScenario";
        const string PropNameDisplayTrainingScenario = "DisplayTrainingScenario";
        const string PropNameDisplayVabCraft = "DisplayVabCraft";
        const string PropNameDisplaySphCraft = "DisplaySphCraft";
        const string PropNameDisplayPart = "DisplayPart";
        const string PropNameDisplaySetting = "DisplaySetting";

        const string PropNameSave = "Save";
        const string PropNameScenario = "Scenario";
        const string PropNameTrainingScenario = "TrainingScenario";
        const string PropNameVabCraft = "VabCraft";
        const string PropNameSphCraft = "SphCraft";
        const string PropNamePart = "Part";
        const string PropNameSetting = "Setting";
        #endregion

        #region private property values
        private string installPath;
        private KerbalData kerbalData;

        private string selectedSave;
        private string selectedScenario;
        private string selectedTrainingScenario;
        private string selectedVabCraft;
        private string selectedSphCraft;
        private string selectedPart;
        private string selectedSetting;

        private bool displaySave;
        private bool displayScenario;
        private bool displayTrainingScenario;
        private bool displayVabCraft;
        private bool displaySphCraft;
        private bool displayPart;
        private bool displaySetting;

        private SaveFile save;
        private SaveFile scenario;
        private SaveFile trainingScenario;
        private CraftFile vabCraft;
        private CraftFile sphCraft;
        private PartFile part;
        private ConfigFile setting;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>	
        public MainWindowViewModel()
        {
            this.OpenKspInstallFolderCommand = new DelegateCommand(OpenKspInstallFolderDialog);
        }

        public KerbalData KerbalData
        {
            get
            {
                return kerbalData;
            }
            set
            {
                if (value != kerbalData)
                {
                    kerbalData = value;
                    OnPropertyChanged(PropNameKerbalData);
                }
            }
        }

        public string InstallPath
        {
            get
            {
                return installPath;
            }
            set
            {
                if (value != installPath)
                {
                    installPath = value;
                    OnPropertyChanged(PropNameInstallPath);
                    UpdateInstallPath();
                }

            }
        }

        #region File Selection Properties
        public string SelectedSave
        {
            get
            {
                return selectedSave;
            }
            set
            {
                selectedSave = value;
                OnPropertyChanged(PropNameSelectedSave);
            }
        }

        public string SelectedScenario
        {
            get
            {
                return selectedScenario;
            }
            set
            {
                selectedScenario = value;
                OnPropertyChanged(PropNameSelectedScenario);
            }
        }

        public string SelectedTrainingScenario
        {
            get
            {
                return selectedTrainingScenario;
            }
            set
            {
                selectedTrainingScenario = value;
                OnPropertyChanged(PropNameSelectedTrainingScenario);
            }
        }

        public string SelectedVabCraft
        {
            get
            {
                return selectedVabCraft;
            }
            set
            {
                selectedVabCraft = value;
                OnPropertyChanged(PropNameSelectedVabCraft);
            }
        }

        public string SelectedSphCraft
        {
            get
            {
                return selectedSphCraft;
            }
            set
            {
                selectedSphCraft = value;
                OnPropertyChanged(PropNameSelectedSphCraft);
            }
        }

        public string SelectedPart
        {
            get
            {
                return selectedPart;
            }
            set
            {
                selectedPart = value;
                 OnPropertyChanged(PropNameSelectedPart);
            }
        }

        public string SelectedSetting
        {
            get
            {
                return selectedSetting;
            }
            set
            {
                selectedSetting = value;
                OnPropertyChanged(PropNameSelectedSetting);
            }
        }
        #endregion

        #region File Display Properties
        public bool DisplaySave
        {
            get
            {
                return displaySave;
            }
            set
            {
                displaySave = value;
                OnPropertyChanged(PropNameDisplaySave);
            }
        }

        public bool DisplayScenario
        {
            get
            {
                return displayScenario;
            }
            set
            {
                displayScenario = value;
                OnPropertyChanged(PropNameDisplayScenario);
            }
        }

        public bool DisplayTrainingScenario
        {
            get
            {
                return displayTrainingScenario;
            }
            set
            {
                displayTrainingScenario = value;
                OnPropertyChanged(PropNameDisplayTrainingScenario);
            }
        }

        public bool DisplayVabCraft
        {
            get
            {
                return displayVabCraft;
            }
            set
            {
                displayVabCraft = value;
                OnPropertyChanged(PropNameDisplayVabCraft);
            }
        }

        public bool DisplaySphCraft
        {
            get
            {
                return displaySphCraft;
            }
            set
            {
                displaySphCraft = value;
                OnPropertyChanged(PropNameDisplaySphCraft);
            }
        }

        public bool DisplayPart
        {
            get
            {
                return displayPart;
            }
            set
            {
                displayPart = value;
                OnPropertyChanged(PropNameDisplayPart);
            }
        }

        public bool DisplaySetting
        {
            get
            {
                return displaySetting;
            }
            set
            {
                displaySetting = value;
                OnPropertyChanged(PropNameDisplaySetting);
            }
        }
        #endregion

        #region Data Properties
        public SaveFile Save
        {
            get
            {
                return save;
            }
            set
            {
                save = value;
                OnPropertyChanged(PropNameSave);
            }
        }

        public SaveFile Scenario
        {
            get
            {
                return scenario;
            }
            set
            {
                scenario = value;
                OnPropertyChanged(PropNameScenario);
            }
        }

        public SaveFile TrainingScenario
        {
            get
            {
                return trainingScenario;
            }
            set
            {
                trainingScenario = value;
                OnPropertyChanged(PropNameTrainingScenario);
            }
        }

        public CraftFile VabCraft
        {
            get
            {
                return vabCraft;
            }
            set
            {
                vabCraft = value;
                OnPropertyChanged(PropNameVabCraft);
            }
        }

        public CraftFile SphCraft
        {
            get
            {
                return sphCraft;
            }
            set
            {
                sphCraft = value;
                OnPropertyChanged(PropNameSphCraft);
            }
        }

        public PartFile Part
        {
            get
            {
                return part;
            }
            set
            {
                part = value;
                OnPropertyChanged(PropNamePart);
            }
        }

        public ConfigFile Setting
        {
            get
            {
                return setting;
            }
            set
            {
                setting = value;
                OnPropertyChanged(PropNameSetting);
            }
        }
        #endregion 


        public ICommand OpenKspInstallFolderCommand { get; private set; }

        private void UpdateInstallPath()
        {
            KerbalData = null;
            KerbalData = new KerbalData(installPath);
        }

        private void OnPropertyChanged(string name, object value = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

            switch (name)
            {
                case PropNameSelectedSave:
                    Save = kerbalData.Saves[SelectedSave];
                    DisplaySave = true;
                    
                    DisplayScenario = false;
                    DisplayTrainingScenario = false;
                    DisplayVabCraft = false;
                    DisplaySphCraft = false;
                    DisplayPart = false;
                    DisplaySetting = false;

                    break;
                case PropNameSelectedScenario:
                    Scenario = kerbalData.Scenarios[SelectedScenario];
                    DisplayScenario = true;

                    DisplaySave = false;
                    DisplayTrainingScenario = false;
                    DisplayVabCraft = false;
                    DisplaySphCraft = false;
                    DisplayPart = false;
                    DisplaySetting = false;

                    break;
                case PropNameSelectedTrainingScenario:
                    TrainingScenario = kerbalData.TrainingScenarios[SelectedTrainingScenario];
                    DisplayTrainingScenario = true;

                    DisplaySave = false;
                    DisplayScenario = false;
                    DisplayVabCraft = false;
                    DisplaySphCraft = false;
                    DisplayPart = false;
                    DisplaySetting = false;

                    break;
                case PropNameSelectedVabCraft:
                    VabCraft = kerbalData.CraftInVab[SelectedVabCraft];
                    DisplayVabCraft = true;

                    DisplaySave = false;
                    DisplayScenario = false;
                    DisplayTrainingScenario = false;
                    DisplaySphCraft = false;
                    DisplayPart = false;
                    DisplaySetting = false;

                    break;
                case PropNameSelectedSphCraft:
                    SphCraft = kerbalData.CraftInSph[SelectedSphCraft];
                    DisplaySphCraft = true;

                    DisplaySave = false;
                    DisplayScenario = false;
                    DisplayTrainingScenario = false;
                    DisplayVabCraft = false;
                    DisplayPart = false;
                    DisplaySetting = false;

                    break;
                case PropNameSelectedPart:
                    Part = kerbalData.Parts[SelectedPart];
                    DisplayPart = true;

                    DisplaySave = false;
                    DisplayScenario = false;
                    DisplayTrainingScenario = false;
                    DisplayVabCraft = false;
                    DisplaySphCraft = false;
                    DisplaySetting = false;

                    break;
                case PropNameSelectedSetting:
                    Setting = kerbalData.KspSettings[SelectedSetting];
                    DisplaySetting = true;

                    DisplaySave = false;
                    DisplayScenario = false;
                    DisplayTrainingScenario = false;
                    DisplayVabCraft = false;
                    DisplaySphCraft = false;
                    DisplayPart = false;

                    break;
            }
            
        }

        private void OpenKspInstallFolderDialog()
        {
            var dlg = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();

            if (result.ToString() == "OK")
            {
                InstallPath = dlg.SelectedPath;
            }
        }
    }
}
