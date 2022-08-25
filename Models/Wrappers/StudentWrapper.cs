using System.ComponentModel;

namespace Diary.Models.Wrappers
{
    public class StudentWrapper : IDataErrorInfo
    {
        public StudentWrapper()
        {
            Group = new GroupWrapper();
        }

        public bool _isStudentNameValid = false;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Pole Imie Jest wymagane";
                            _isStudentNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Pole Nazwisko Jest wymagane";
                            _isStudentNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;
                    default:
                        break;
                }
                if (string.IsNullOrEmpty(Error)) _isStudentNameValid = true;

                return Error;
            }
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Math { get; set; }

        public string Technology { get; set; }

        public string Physics { get; set; }

        public string PolishLang { get; set; }

        public string ForeignLang { get; set; }

        public bool Activities { get; set; }

        public GroupWrapper Group { get; set; }

        public string Comments { get; set; }

        public string Error { get; set; }

        public bool StudentDataValid
        {
            get
            {
                return _isStudentNameValid && Group._isGroupDataValid;
            }
        }

    }
}
