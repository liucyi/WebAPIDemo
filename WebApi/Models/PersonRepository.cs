using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class PersonRepository : IPersonRepository
    {

        // We are using the list and _fakeDatabaseID to represent what would

        // most likely be a database of some sort, with an auto-incrementing ID field:

        private List<Person> _people = new List<Person>();

        private int _fakeDatabaseID = 1;





        public PersonRepository()
        {

            // For the moment, we will load some sample data during initialization. 

            this.Add(new Person { LastName = "Lennon", FirstName = "John" });

            this.Add(new Person { LastName = "McCartney", FirstName = "Paul" });

            this.Add(new Person { LastName = "Harrison", FirstName = "George" });

            this.Add(new Person { LastName = "Starr", FirstName = "Ringo" });

        }





        public IEnumerable<Person> GetAll()
        {

            return _people;

        }





        public Person Get(int id)
        {

            return _people.Find(p => p.Id == id);

        }





        public Person Add(Person person)
        {

            if (person == null)
            {

                throw new ArgumentNullException("person");

            }

            person.Id = _fakeDatabaseID++;

            _people.Add(person);

            return person;

        }





        public void Remove(int id)
        {

            _people.RemoveAll(p => p.Id == id);

        }





        public bool Update(Person person)
        {

            if (person == null)
            {

                throw new ArgumentNullException("person");

            }

            int index = _people.FindIndex(p => p.Id == person.Id);

            if (index == -1)
            {

                return false;

            }

            _people.RemoveAt(index);

            _people.Add(person);

            return true;

        }

    }

}