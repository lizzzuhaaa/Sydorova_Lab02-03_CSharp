using System;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using Lab01Sydorova.Model;
using System.Xml.Linq;
using Lab01Sydorova.DataManage;

namespace Lab01Sydorova.VM
{
    internal class PersonCreate : INotifyPropertyChanged
    {
        private Person _user;
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _age = "";
        private DateTime _birthdate = DateTime.Now;


        private ProceedCommandExec<object> _submition;
        private ProceedCommandExec<object> _back;

        internal PersonCreate(Person person) 
        { 
            _user = person;
            FirstName = _user.FirstName;
            LastName = _user.LastName;
            Email = _user.Email;
            BirthDate = _user.Birthday;
        }
        internal PersonCreate() { }
        public string FirstName
        {
            get
            {
                return _firstname;
            }
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return _birthdate;
            }
            set
            {
                _birthdate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        public bool IsAdult
        {
            get { return _user.IsAdult; }
        }

        public string SunSign
        {
            get { return _user == null ? "" : _user.SunSign; }
            private set
            {
                OnPropertyChanged(nameof(SunSign));
            }
        }

        public string ChineseSign
        {
            get { return _user == null ? "" : _user.ChineseSign; }

            private set
            {
                OnPropertyChanged(nameof(ChineseSign));
            }
        }

        public bool IsBirthday
        {
            get { return _user == null ? false : _user.IsBirthday; }

        }
        public string AgeText
        {
            get
            {
                return _age;
            }
            private set
            {
                _age = value;
                OnPropertyChanged(nameof(AgeText));
            }
        }
        public Action BackAction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ProceedCommandExec<object> Submitting
        {
            get
            {
                return _submition ?? (_submition = new ProceedCommandExec<object>(
                           Proceed, o => IsProceedEnabled()));
            }
        }
        public ProceedCommandExec<object> Back
        {
            get
            {
                return _back ?? (_back = new ProceedCommandExec<object>(
                           BackMove, o => true));
            }
        }


        public async void Proceed(object obj)
        {

           try
            {
               
                if (_user != null)
                {
                    Person prev = _user;
                    _user = new Person(FirstName, LastName, Email, BirthDate);
                    AgeText = $"Your first name: {FirstName}" + Environment.NewLine +
                        $"Your last name: {LastName}" + Environment.NewLine +
                        $"Your email: {Email}" + Environment.NewLine;
                    if (IsBirthday)
                    {
                        AgeText += $"My congratulations, you're already {GetAge(BirthDate)} y.o";
                    }
                    else
                    {
                        AgeText += $"Your age: {GetAge(BirthDate)}";
                    }
                    ChineseSign = $"Chinese Zodi: {_user.ChineseSign}";
                    SunSign = $"Western Zodi: {_user.SunSign}";
                    if (MessageBox.Show("Do you want to submit changes?", "Submit changes",
                           MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        DataStorage.Storage.EditPerson(prev, _user);
                    }
                }
                else
                {
                    DataStorage.Storage.AddPerson(new Person(FirstName, LastName, Email, BirthDate));
                    if (MessageBox.Show("Thank you", "Your person has been added. Do you want to move back?",
                           MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        BackAction();
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void BackMove(object obj) 
        {
            try
            {
                if (MessageBox.Show("You want to move back", "Did you submit your modifications?",
                            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    BackAction();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool IsProceedEnabled()
        {
            if (string.IsNullOrWhiteSpace(_email) || string.IsNullOrWhiteSpace(_firstname) || string.IsNullOrWhiteSpace(_lastname))
            {
                return false;
            }
            return true;
        }

        private int GetAge(DateTime birthdate)
        {
            DateTime currentDate = DateTime.Now;
            int ageThisYear = currentDate.Year - birthdate.Year;
            if (birthdate > currentDate.AddYears(-ageThisYear))
                ageThisYear--;
            return ageThisYear;
        }
    }
}
