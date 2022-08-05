using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;


namespace Diary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var metroWindow = Current.MainWindow as MetroWindow;
            metroWindow.ShowMessageAsync($"Nieoczekiwany wyjatek", $"Wystapił nie oczekiwany wyjatek \n {e.Exception.Message}");
            e.Handled = true;
        }


    }
}
