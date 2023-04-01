using Lab01Sydorova.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab01Sydorova.DataManage
{
    public class CreateDataStorage : IDataStorage
    {
        private List<Person> _people;
        internal CreateDataStorage()
        {
            try
            {
                _people = SerializationProc.Deserialize<List<Person>>(FileManageCl.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _people = new List<Person>();
                FillPeople();
                SaveAll();
            }
        }

        private void FillPeople()
        {
            Random random = new Random();
            List<string> firstNamesGirls = new List<string> { "Liza", "Regina", "Yulia", "Masha", "Maryna", "Sonia", "Nastia", "Ivan", "Sasha", "Katya", "Anna", "Eva" };
            List<string> firstNamesBoys = new List<string> { "Mark", "Dima", "Glib", "Kyrylo", "Andrew", "Sergiy", "Stepan", "Vova", "Illya", "Ivan", "Sasha", "Viktor", "Nikita" };
            List<string> secondNamesGirls = new List<string> { "Sydorova", "Budikova", "Kruta", "Vasilenko", "Dutchina", "Pekarova", "Relivkina", "Derkarch", "Grishko", "Prudnikova", "Denisova", "Polishchuk", "Sitnikova" };
            List<string> secondNamesBoys = new List<string> { "Cherkasov", "Tymoshenko", "Borovik", "Vasilenko", "Dutchin", "Pekarov", "Volkov", "Derkarch", "Grishko", "Prudnikov", "Denisiuk", "Kyivskiy", "Kutsenko" };
            List<string> emails = new List<string> { };

            for (int i = 0; i < 25; i++)
            {
                string gFirstName = firstNamesGirls[random.Next(firstNamesGirls.Count)];
                string gLastName = secondNamesGirls[random.Next(secondNamesGirls.Count)];
                string gEmail = gFirstName.ToLower() + "." + gLastName.ToLower() + "@gmail.com";
                while (emailContains(gEmail, emails))
                {
                    gEmail = gFirstName.ToLower() + "." + gLastName.ToLower() + random.Next(2023) + "@gmail.com";
                }
                emails.Add(gEmail);
                AddPerson(new Person(gFirstName, gLastName, gEmail, dateCreate()));


                string bFirstName = firstNamesBoys[random.Next(firstNamesBoys.Count)];
                string bLastName = secondNamesBoys[random.Next(secondNamesBoys.Count)];
                string bEmail = bFirstName.ToLower() + "." + bLastName.ToLower() + "@gmail.com";
                while (emailContains(bEmail, emails))
                {
                    bEmail = bFirstName.ToLower() + "." + bLastName.ToLower() + random.Next(2023) + "@gmail.com";
                }
                emails.Add(bEmail);
                AddPerson(new Person(bFirstName, bLastName, bEmail, dateCreate()));
            }

        }

        public List<Person> GetAll { get => _people.ToList(); set => _people = value; }
        public void AddPerson(Person person)
        {
            if (correctToModify(person))
            {
                _people.Add(person);
                SaveAll();
            }
            else throw new ArgumentNullException("Invalid data!");
        }

        public void DeletePerson(Person person)
        {
            _people?.Remove(person);
            SaveAll();
        }

        public void EditPerson(Person prevPerson, Person newPerson)
        {
            if (correctToModify(newPerson))
            {
                _people[_people.IndexOf(prevPerson)] = newPerson;
                SaveAll();
            }
            else throw new ArgumentNullException("Invalid data!");
        }

        public void SaveAll()
        {
            SerializationProc.Serialize(_people, FileManageCl.StorageFilePath);
        }

        private bool correctToModify(Person person) { return !string.IsNullOrWhiteSpace(person.FirstName) && !string.IsNullOrWhiteSpace(person.LastName) && !string.IsNullOrWhiteSpace(person.Email); }
        private bool emailContains(string email, List<string> allEmails) { return allEmails.Contains(email); }

        private DateTime dateCreate()
        {
            Random random = new Random();
            int year = random.Next(DateTime.Now.Date.Year - 134, DateTime.Now.Date.Year);
            int month = random.Next(1, 13);
            int day = 0;
            if (month == 2) { day = random.Next(1, 27); }
            else if (month == 4 || month == 6 || month == 9 || month == 11) { day = random.Next(1, 31); }
            else { day = random.Next(1, 32); }
            return new DateTime(year, month, day);
        }
    }
}
