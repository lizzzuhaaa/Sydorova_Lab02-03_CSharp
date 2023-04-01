using Lab01Sydorova.DataManage;
using Lab01Sydorova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Lab01Sydorova.VM
{
    internal class TableModify: INotifyPropertyChanged
    {
        private ObservableCollection<Person> _all;
        private Person _exactPerson;
        private ProceedCommandExec<object> _edit;
        private ProceedCommandExec<object> _add;
        private ProceedCommandExec<object> _delete;
        private ProceedCommandExec<object> _save;
        public Person ExactPerson { get => _exactPerson; set => _exactPerson = value; }
        public ObservableCollection<Person> All { get => _all; set { _all = value; OnPropertyChanged(); } }
        public ProceedCommandExec<object> Add
        {
            get{ return _add ?? (_add = new ProceedCommandExec<object>( Adding)); }
        }

        public ProceedCommandExec<object> Delete
        {
            get{return _delete ?? (_delete = new ProceedCommandExec<object>( Deleting, o => IsAbleToSubmit())); }
        }

        public ProceedCommandExec<object> Edit
        {
            get{return _edit ?? (_edit = new ProceedCommandExec<object>(Editing, o => IsAbleToSubmit()));}
        }

        public ProceedCommandExec<object> Save
        {
            get { return _save ?? (_save = new ProceedCommandExec<object>( Saving));}
        }

        private void Adding(object obj)
        {
            AddPerson window = new AddPerson();
            window.ShowDialog();

            All = new ObservableCollection<Person>(DataStorage.Storage.GetAll);
        }

        private async void Deleting(object obj)
        {
            await Task.Run(() =>
            {
                if (MessageBox.Show("Attention",
                $"Do you want to delete {_exactPerson}?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DataStorage.Storage.DeletePerson(_exactPerson);
                    _exactPerson = null;
                    All = new ObservableCollection<Person> (DataStorage.Storage.GetAll);
                }
            });
            Thread.Sleep(150);

        }
        private void Editing(object obj)
        {

            AddPerson window = new AddPerson(_exactPerson);
            window.ShowDialog();

            All = new ObservableCollection<Person>(DataStorage.Storage.GetAll);
        }
        private async void Saving(object obj)
        {
            await Task.Run(() =>
            {
                DataStorage.Storage.SaveAll();
                Thread.Sleep(150);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal TableModify()
        {
            _all = new ObservableCollection<Person>(DataStorage.Storage.GetAll);
        }

        private bool IsAbleToSubmit()
        {
            return _exactPerson != null;
        }
    }
}
