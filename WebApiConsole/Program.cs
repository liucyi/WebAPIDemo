using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Models;

namespace WebApiConsole
{
    class Program
    {
        private const string url = "http://localhost:43571/";
        static void Main(string[] args)
        {
            Console.WriteLine("Retreive All The People:");

            var people = GetAllPerson();

            //foreach (var person in people)
            //{

            //    Console.WriteLine(person);

            //}
            var data = JsonConvert.DeserializeObject<List<Person>>(people.Data);
            foreach (var i in data)
            {
                Console.WriteLine(i);
            }




            // WRITE A SPECIFIC PERSON TO CONSOLE (JSON):

            Console.WriteLine(Environment.NewLine + "Retreive a Person by ID:");

            var singlePerson = JsonConvert.DeserializeObject< Person >(GetPerson(2).Data);

            Console.WriteLine(singlePerson);



            // ADD NEW PERSON, THEN WRITE TO CONSOLE (JSON):

            Console.WriteLine(Environment.NewLine + "Add a new Person and return the new object:");

            JObject newPerson = AddPerson("Atten", "John");

            Console.WriteLine(newPerson);



            // UPDATE AN EXISTING PERSON, THEN WRITE TO CONSOLE (JSON):

            Console.WriteLine(Environment.NewLine + "Update an existing Person and return a boolean:");



            // Pretend we already had a person's data:
           
            var personToUpdate =  JsonConvert.DeserializeObject< Person> (GetPerson(2).Data);

            string newLastName = "Richards";



            Console.WriteLine("Update Last Name of " + personToUpdate + "to " + newLastName);



            // Pretend we don't already know the Id:

            int id = personToUpdate.Id;

            string FirstName = personToUpdate.FirstName;

            //     string LastName = personToUpdate.Value<string>("LastName");



            if (UpdatePerson(id, newLastName, FirstName))
            {

                Console.WriteLine(Environment.NewLine + "Updated person:");

                Console.WriteLine(JsonConvert.DeserializeObject< Person> (GetPerson(id).Data));  

            }



            // DELETE AN EXISTING PERSON BY ID:

            Console.WriteLine(Environment.NewLine + "Delete person object:");

            DeletePerson(5);



            // WRITE THE UPDATED LIST TO THE CONSOLE:

            {

                // WRITE ALL PEOPLE TO CONSOLE

                Console.WriteLine("Retreive All The People using classes:");

                people = GetAllPerson();

                //foreach (var person in people)
                //{

                //    Console.WriteLine(person);

                //}
                data = JsonConvert.DeserializeObject<List<Person>>(people.Data);
                foreach (var i in data)
                {
                    Console.WriteLine(i);
                }


            }



            Console.Read();

        }
        public class Person
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

        }
        /// <summary>
        /// get all Person
        /// </summary>
        /// <returns></returns>
        static HttpResponseMessageViewModel GetAllPerson()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url + "api/person").Result;
            return response.Content.ReadAsAsync<HttpResponseMessageViewModel>().Result;
        }

        static HttpResponseMessageViewModel GetPerson(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url + "api/person/" + id).Result;
            return response.Content.ReadAsAsync<HttpResponseMessageViewModel>().Result;
        }

        static JObject AddPerson(string newLastName, string newFirstName)
        {
            var newPerson = new { LastName = newLastName, FirstName = newFirstName };



            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);

            var response = client.PostAsJsonAsync("api/person", newPerson).Result;

            return response.Content.ReadAsAsync<JObject>().Result;

        }



        // Sends HTTP PUT to Person Controller on API with Anonymous Object:

        static bool UpdatePerson(int personId, string newLastName, string newFirstName)
        {

            // Initialize an anonymous object representing a the modified Person record:

            var newPerson = new { id = personId, LastName = newLastName, FirstName = newFirstName };

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);

            var response = client.PutAsJsonAsync("api/person/", newPerson).Result;

            return response.Content.ReadAsAsync<bool>().Result;

        }





        // Sends HTTP DELETE to Person Controller on API with Id Parameter:

        static void DeletePerson(int id)
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);

            var relativeUri = "api/person/" + id.ToString();

            var response = client.DeleteAsync(relativeUri).Result;

            client.Dispose();

        }
    }
}
