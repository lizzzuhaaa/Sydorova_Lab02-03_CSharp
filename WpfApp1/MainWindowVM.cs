using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading;

namespace Lab01Sydorova
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        private Person _user;
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _age = "";
        private DateTime _birthdate = DateTime.Now;
        private string _westernZodiac = "";
        private string _chineseZodiac = "";
        private ProceedCommandExec<object> _exec;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ProceedCommandExec<object> Submitting
        {
            get
            {
                return _exec ?? (_exec = new ProceedCommandExec<object>(
                           Proceed, o => IsProceedEnabled()));
            }
        }

        internal MainWindowVM() { BirthDate = DateTime.Now; }
        public async void Proceed(object obj)
        {
            FirstName = this.FirstName;
            LastName = this.LastName;
            Email = this.Email;
            AgeText = this.AgeText;
            ChineseSign = this.ChineseSign;
            SunSign = this.SunSign;

            await Task.Run((() =>
            {
                Thread.Sleep(1000);
                try
                {
                    _user = new Person(FirstName, LastName, Email, BirthDate);
                    AgeText = $"Your first name: {FirstName}" + System.Environment.NewLine +
                       $"Your last name: {LastName}" + System.Environment.NewLine +
                       $"Your email: {Email}" + System.Environment.NewLine;
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
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            ));
        }

        public string FirstName
        {
            get
            {
                return this._firstname;
            }
            set
            {
                this._firstname = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get
            {
                return this._lastname;
            }
            set
            {
                this._lastname = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return this._birthdate;
            }
            set
            {
                this._birthdate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        public bool IsAdult
        {
            get { return this._user.IsAdult; }
        }

        public string SunSign
        {
            get { return _user == null ? "" : _user.SunSign; }
            private set
            {
                this._westernZodiac = value;
                OnPropertyChanged(nameof(SunSign));
            }
        }

        public string ChineseSign
        {
            get { return _user == null ? "" : _user.ChineseSign; }

            private set
            {
                this._chineseZodiac = value;
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
                this._age = value;
                OnPropertyChanged(nameof(AgeText));
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
