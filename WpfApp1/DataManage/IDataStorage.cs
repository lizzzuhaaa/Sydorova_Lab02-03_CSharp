using Lab01Sydorova.Model;
using System.Collections.Generic;

namespace Lab01Sydorova.DataManage
{
    internal interface IDataStorage
    {
        void AddPerson(Person person);
        void EditPerson(Person prevPerson, Person newPerson);
        void DeletePerson(Person person);
        void SaveAll();

        List<Person> GetAll { get; set; }
    }
}
