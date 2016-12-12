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




            var content = databasePlaceholder.GetAll();

            var Data = JsonConvert.SerializeObject(content);
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = content,
                StatusCodeDes = "",
                IsSuccess = true,
                StatusCode = (int)System.Net.HttpStatusCode.OK
            };

            var response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };


            return response;

        }





        public HttpResponseMessage GetPersonByID(int id)
        {

            Person person = databasePlaceholder.Get(id);

            if (person == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = person,
                StatusCodeDes = "",
                IsSuccess = true,
                StatusCode = (int)System.Net.HttpStatusCode.OK
            };
            var response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };

            return response;

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





        public HttpResponseMessage PutPerson(Person person)
        {

            if (!databasePlaceholder.Update(person))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = "更新",
                StatusCodeDes = "",
                IsSuccess = true,
                StatusCode = (int)System.Net.HttpStatusCode.OK
            };
            var response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };

            return response;


        }





        public HttpResponseMessage DeletePerson(int id)
        {

            Person person = databasePlaceholder.Get(id);

            if (person == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            databasePlaceholder.Remove(id);
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = "删除",
                StatusCodeDes = "",
                IsSuccess = true,
                StatusCode = (int)System.Net.HttpStatusCode.OK
            };
            var response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };

            return response;

        }

    }

}
