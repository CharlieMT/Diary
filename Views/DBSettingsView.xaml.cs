using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Diary.Properties;
using Diary.ViewModels;
using Diary.Models.Domains;
using MahApps.Metro.Controls.Dialogs;

namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class DBSettingsView : MetroWindow
    {

        public DBSettingsView(DBSettings dbSettings = null)
        {
            InitializeComponent();
            DataContext = new EditDBSettingsViewModel(dbSettings, this);
        }
    }


}
