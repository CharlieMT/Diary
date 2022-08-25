using MahApps.Metro.Controls;
using Diary.ViewModels;
using MahApps.Metro.Controls.Dialogs;
using Diary.Models.Wrappers;


namespace Diary.Views
{
    public partial class DBSettingsView : MetroWindow
    {
        private EditDBSettingsViewModel editDBSettingsViewModel;
        public DBSettingsView(DBSettingsWrapper dbSettings = null)
        {
            InitializeComponent();

            editDBSettingsViewModel = new EditDBSettingsViewModel(dbSettings, DialogCoordinator.Instance);
            DataContext = editDBSettingsViewModel;

            Closing += editDBSettingsViewModel.OnWindowsClosing;
        }

    }


}
