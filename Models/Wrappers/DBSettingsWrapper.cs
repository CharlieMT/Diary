using System.ComponentModel;

namespace Diary.Models.Wrappers
{
    public class DBSettingsWrapper : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = $" Pole Nazwa Serwera jest wymagane ";
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;
                    case nameof(ServerAddress):
                        if (string.IsNullOrWhiteSpace(ServerAddress))
                        {
                            Error = "Pole Adres Serwera jest wymagane ";
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;
                    case nameof(UserName):
                        if (string.IsNullOrWhiteSpace(UserName))
                        {
                            Error = $" Pole Nazwa Użytkownika jest wymagane ";
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            Error = " Pole Hasło jest wymagane ";
                        }
                        else
                        {
                            Error = string.Empty;

                        }
                        break;
                    case nameof(DataBaseName):
                        if (string.IsNullOrWhiteSpace(DataBaseName))
                        {
                            Error = " Pole Nazwa Bazy Danych jest wymagane ";
                        }
                        else
                        {
                            Error = string.Empty;
                        }
                        break;
                    default:
                        Error = string.Empty;
                        break;
                }
                Connection = false;
                return Error;
            }
        }

        public string ServerAddress { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DataBaseName { get; set; }

        public string Error { get; set; }

        public string ServerName { get; set; }

        public bool Connection { get; set; } = true;

    }
}
