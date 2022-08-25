using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Diary.Commands;
using Diary.Models.Wrappers;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Diary.Views;
using Diary.Models.Domains;
using System.Threading;

namespace Diary.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        private ApplicationDbContext dbContext;
        public static bool IsConnectedToDB { get; set; }
        public MainViewModel()
        {
            SplashScreen splashScreen = new SplashScreen("Images\\StudentDiary.jpg");
            splashScreen.Show(true);
            Thread.Sleep(3500);

            RefreshStudentsCommand = new RelayCommand(RefeshStudents, IsDatabaseConnected);
            AddStudentCommand = new RelayCommand(AddEditStudent, IsDatabaseConnected);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            EditDBSettingsCommand = new RelayCommand(EditDBSettings);
            dbContext = new ApplicationDbContext();
            if (dbContext.Database.Exists())
            {
                IsConnectedToDB = true;
                RefreshDiary();
                InitGroups();
            }
            else
            {
              Task task = NoConnectionDialogBox();
            }
        }


        public ICommand RefreshStudentsCommand { get; set; }

        public ICommand AddStudentCommand { get; set; }

        public ICommand EditStudentCommand { get; set; }

        public ICommand DeleteStudentCommand { get; set; }

        public ICommand EditDBSettingsCommand { get; set; }


        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get
            {
                return _selectedStudent;
            }

            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get 
            { 
                return _selectedGroupId;
            }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get 
            { 
                return _group; 
            }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });
            Groups = new ObservableCollection<Group>(groups);
            SelectedGroupId = 0;
        }


        private void RefeshStudents(object obj)
        {
            RefreshDiary();
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie Ucznia", $"Czy na pewno chesz usunac ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName} ?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
            {
                return;
            }
            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private bool IsDatabaseConnected(object obj)
        {
            return IsConnectedToDB;
        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void RefreshDiary()
        {
            if (IsConnectedToDB)
                Students = new ObservableCollection<StudentWrapper>(_repository.GetStudent(SelectedGroupId));
            else
                Students = null;
        }

        private void EditDBSettings(object obj)
        {
            var editDBSettingsWindow = new DBSettingsView(obj as DBSettingsWrapper);
            editDBSettingsWindow.Closed += AddEditStudentWindow_Closed;
            editDBSettingsWindow.ShowDialog();
            RefreshDiary();
        }

        private async Task NoConnectionDialogBox()
        {
            var metrowindow = Application.Current.MainWindow as MetroWindow;

            var dialog = await metrowindow.ShowMessageAsync("Database Connection Error ", "Brak połaczenia z Baza Danych \n Sprawdz ustawienia ", MessageDialogStyle.AffirmativeAndNegative);

            if(dialog != MessageDialogResult.Affirmative)
            {
                return;
            }
            EditDBSettings(this);
            
        }

    }
}
