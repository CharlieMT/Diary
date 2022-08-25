using System.Threading.Tasks;
using Diary.Properties;
using Diary.Commands;
using System.Windows.Input;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Diary.Models.Wrappers;
using System.ComponentModel;

namespace Diary.ViewModels
{
    class EditDBSettingsViewModel : ViewModelBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;

        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public EditDBSettingsViewModel(DBSettingsWrapper dbSettings = null, IDialogCoordinator dialogCoordinator = null )
        {
            if (dbSettings == null)
            {
                DBSettings = new DBSettingsWrapper();
            }
            else
            {
                DBSettings = dbSettings;
            }

            _dialogCoordinator = dialogCoordinator;
            CloseCommand = new RelayCommand(Clean);
            CheckCommand = new AsyncRelayCommand(Check, CheckForEmptyStrings);
            ConfirmCommand = new RelayCommand(Confirm, CanConfirmParameters);

            GetDbSettings();
            DBSettings.Connection = dbContext.CheckConnectionToDatabase(DBSettings.ServerAddress, DBSettings.ServerName, DBSettings.DataBaseName, DBSettings.UserName, DBSettings.Password);
        }


        public ICommand CloseCommand { get; set; }

        public ICommand CheckCommand { get; set; }

        public ICommand ConfirmCommand { get; set; }

        public void OnWindowsClosing(object sender, CancelEventArgs args)
        {
            if (CheckForEmptyStrings(this))
                return;
            if(CheckIfParametersWhereNotChanged())
                MainViewModel.IsConnectedToDB = DBSettings.Connection;
        }

        private void GetDbSettings()
        {
            DBSettings.ServerAddress = Settings.Default.ServerAddress;
            DBSettings.ServerName = Settings.Default.ServerName;
            DBSettings.UserName = Settings.Default.UserName;
            DBSettings.Password = Settings.Default.Password;
            DBSettings.DataBaseName = Settings.Default.DatabaseName;
        }

        private void SetDbSettings()
        {
            Settings.Default.ServerAddress = DBSettings.ServerAddress;
            Settings.Default.ServerName = DBSettings.ServerName;
            Settings.Default.DatabaseName = DBSettings.DataBaseName;
            Settings.Default.UserName = DBSettings.UserName;
            Settings.Default.Password = DBSettings.Password;
            Settings.Default.Save();
        }
        public DBSettingsWrapper _dbSettings;
        public DBSettingsWrapper DBSettings
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
            SetDbSettings();
            MainViewModel.IsConnectedToDB = DBSettings.Connection;
            CloseWindow(obj as Window);
        }

        private void Clean(object obj)
        {
            DBSettings.ServerAddress = string.Empty;
            DBSettings.ServerName = string.Empty;
            DBSettings.UserName = string.Empty;
            DBSettings.Password = string.Empty;
            DBSettings.DataBaseName = string.Empty;
            OnPropertyChanged(_dbSettings.ServerAddress);
        }

        private async Task Check(object obj)
        { 
            if (!ConnectionStatus())
            {
                var dialog = await _dialogCoordinator.ShowMessageAsync(this, "Connection Error !", $"Nie można połączyć sie z bazą {DBSettings.DataBaseName} \n Sprwadź parametry połącznia", MessageDialogStyle.Affirmative);
                DBSettings.Connection = false;
            }
            else
            {
                var dialog = await _dialogCoordinator.ShowMessageAsync(this, "Connection OK", $" Przetestowana połaczenie z bazą {DBSettings.DataBaseName} \n Wszystko działa", MessageDialogStyle.Affirmative);
                DBSettings.Connection = true;
            }
            return;
        }

        private bool ConnectionStatus()
        {
            return dbContext.CheckConnectionToDatabase(DBSettings.ServerAddress, DBSettings.ServerName, DBSettings.DataBaseName, DBSettings.UserName, DBSettings.Password);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private bool CanConfirmParameters(object obj)
        {
            return DBSettings.Connection;
        }

        private bool CheckForEmptyStrings(object obj)
        {
            if (string.IsNullOrEmpty(DBSettings.ServerAddress))
                return false;
            if (string.IsNullOrEmpty(DBSettings.ServerName))
                return false;
            if (string.IsNullOrEmpty(DBSettings.UserName))
                return false;
            if (string.IsNullOrEmpty(DBSettings.Password))
                return false;
            if (string.IsNullOrEmpty(DBSettings.DataBaseName))
                return false;

            return true;
        }

        private bool CheckIfParametersWhereNotChanged()
        {
            if (DBSettings.ServerAddress != Settings.Default.ServerAddress)
                return false;
            if (DBSettings.ServerName != Settings.Default.ServerName)
                return false;
            if (DBSettings.UserName != Settings.Default.UserName)
                return false;
            if (DBSettings.Password != Settings.Default.Password)
                return false;
            if (DBSettings.DataBaseName != Settings.Default.DatabaseName)
                return false;

            return true;
        }
    }
}
