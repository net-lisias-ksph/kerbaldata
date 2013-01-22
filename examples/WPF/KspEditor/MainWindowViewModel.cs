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
        private string installPath;
        private KerbalData kerbalData;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>	
        public MainWindowViewModel()
        {
            this.OpenKspInstallFolderCommand = new DelegateCommand<object>(OpenKspInstallFolderDialog);
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
                    OnPropertyChanged("KerbalData");
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
                    OnPropertyChanged("InstallPath");
                }

                UpdateInstallPath();
            }
        }

        public ICommand OpenKspInstallFolderCommand { get; private set; }

        private void UpdateInstallPath()
        {
            KerbalData = null;
            KerbalData = new KerbalData(installPath);
        }

        private void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void OpenKspInstallFolderDialog(object arg)
        {
            var dlg = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();

            InstallPath = dlg.SelectedPath;
        }
    }
}
