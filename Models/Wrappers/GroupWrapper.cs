using System.ComponentModel;

namespace Diary.Models.Wrappers
{
    public class GroupWrapper : IDataErrorInfo
    {

        public bool _isGroupDataValid = false;
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Id))
                {
                    if (Id == 0)
                        Error = "Musisz wybrać grupe";

                    else
                        Error = string.Empty;
                }

                _isGroupDataValid = string.IsNullOrEmpty(Error) ? true : false;
                return Error;
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Error { get; set; }
    }
}
