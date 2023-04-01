using Lab01Sydorova.Model;
using Lab01Sydorova.VM;
using System.Windows;

namespace Lab01Sydorova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        public AddPerson()
        {
            InitializeComponent();
            PersonCreate personCreate = new PersonCreate();
            DataContext = personCreate;
            if(personCreate.BackAction == null) {personCreate.BackAction = new System.Action(this.Close);}
        }

        public AddPerson(Person person)
        {
            InitializeComponent();
            PersonCreate personCreate = new PersonCreate(person);
            DataContext = personCreate;
            if (personCreate.BackAction == null){personCreate.BackAction = new System.Action(this.Close);}
        }

    }
}
