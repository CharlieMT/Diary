using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.Properties;
using Diary.Commands;
using Diary.Models.Domains;
using System.Windows.Input;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Diary.Views;

namespace Diary.ViewModels
{
    class EditDBSettingsViewModel : ViewModelBase
    {
        private bool conectionStatus = false;

        private DBSettingsView dBSettingsView;

        public EditDBSettingsViewModel(DBSettings dbSettings = null, DBSettingsView dBSettings = null)
        {
            dBSettingsView = dBSettings;
            CloseCommand = new RelayCommand(Close);
            CheckCommand = new AsyncRelayCommand(Check);
            ConfirmCommand = new RelayCommand(Confirm);
            if(dbSettings == null)
            {
                DBSettings = new DBSettings();
            }
            else
            {
                DBSettings = dbSettings;
            }
            GetDbSettings();
        }

        public ICommand CloseCommand { get; set; }

        public ICommand CheckCommand { get; set; }

        public ICommand ConfirmCommand { get; set; }

        private void GetDbSettings()
        {
            DBSettings.ServerName = Settings.Default.ServerName;
            DBSettings.ServerAddress = Settings.Default.ServerAddress;
            DBSettings.DatabaseName = Settings.Default.DatabaseName;
            DBSettings.UserName = Settings.Default.UserName;
            DBSettings.Password = Settings.Default.Password;
        }

        private void SetDbSettings()
        {
            Settings.Default.ServerName = DBSettings.ServerName;
            Settings.Default.ServerAddress = DBSettings.ServerAddress;
            Settings.Default.DatabaseName = DBSettings.DatabaseName;
            Settings.Default.UserName = DBSettings.UserName;
            Settings.Default.Password = DBSettings.Password;

        }
        private DBSettings _dbSettings;
        public DBSettings DBSettings
        {
            get
            {
                return _dbSettings;
            }
            set
            {
                _dbSettings = value;
                OnPropertyChanged();
            }
        }

        private void Confirm(object obj)
        {
            
            
            //CloseWindow(obj as Window);
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private async Task Check(object obj)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext($@"Server={DBSettings.ServerAddress}\{DBSettings.ServerName};Database={DBSettings.DatabaseName};User Id={DBSettings.UserName}; Password={DBSettings.Password};"))
            {
                conectionStatus = dbContext.Database.Exists();
            }

            if (!conectionStatus)
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;

                var dialog = await metroWindow.ShowMessageAsync("Connection Check", $"Podano nie poprawne parametry polaczenia do bazy danych {DBSettings.DatabaseName}", MessageDialogStyle.Affirmative);



            }
            return;
        }



        private void CloseWindow(Window window)
        {
            window.Close();
        }


    }
}
