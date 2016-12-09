using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PersonController : ApiController
    {
        static readonly IPersonRepository databasePlaceholder = new PersonRepository();


        /// <summary>
        /// 获取所有人信息
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetAllPeople()
        {


             

            var content=   databasePlaceholder.GetAll();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent("U", Encoding.UTF8);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };
            return response;

        }





        public Person GetPersonByID(int id)
        {

            Person person = databasePlaceholder.Get(id);

            if (person == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            return person;

        }





        public HttpResponseMessage PostPerson(Person person)
        {

            person = databasePlaceholder.Add(person);

            string apiName = WebApi.WebApiConfig.DEFAULT_ROUTE_NAME;

            var response =

                this.Request.CreateResponse<Person>(HttpStatusCode.Created, person);

            string uri = Url.Link(apiName, new { id = person.Id });

            response.Headers.Location = new Uri(uri);

            return response;

        }





        public bool PutPerson(Person person)
        {

            if (!databasePlaceholder.Update(person))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return true;

        }





        public void DeletePerson(int id)
        {

            Person person = databasePlaceholder.Get(id);

            if (person == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            databasePlaceholder.Remove(id);

        }

    }

}
