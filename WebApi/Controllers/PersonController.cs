using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using WebApi.Models;
using System.Web.Http.Cors;
using WebApi.Filter;

namespace WebApi.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [RoutePrefix("api/person")]
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
                Data = Data,
                StatusCodeDes = Enum.GetName(typeof(HttpStatusCode), HttpStatusCode.OK),
                IsSuccess = true,
                StatusCode = (int)System.Net.HttpStatusCode.OK
            };

            var response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };


            return response;

        }


        //[HttpGet,Route("api/Person/GetAllPeople_string")]
        //public string GetAllPeople_string()
        //{

        //    var content = databasePlaceholder.GetAll();

        //    var Data = JsonConvert.SerializeObject(content);
        //    HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
        //    {
        //        Data = content,
        //        StatusCodeDes = Enum.GetName(typeof(HttpStatusCode), HttpStatusCode.OK),
        //        IsSuccess = true,
        //        StatusCode = (int)System.Net.HttpStatusCode.OK
        //    };

        //    var response = new HttpResponseMessage
        //    { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };


        //    return JsonConvert.SerializeObject(viewModel);

        //}
        //[HttpGet, Route("api/Person/GetAllPeople_int")]
        //public IEnumerable<Person> GetAllPeople_int(Person Person)
        //{

        //    return  databasePlaceholder.GetAll();

        //}

        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "GET,POST", methods: "*")]
        public HttpResponseMessage GetPersonByID(int id)
        {
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel();

            Person person = databasePlaceholder.Get(id);

            if (person == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            viewModel.Data = JsonConvert.SerializeObject(person);
            viewModel.IsSuccess = true;
            viewModel.StatusCode = (int)System.Net.HttpStatusCode.OK;
            var response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };

            return response;

        }

        [CrossSite]
        public HttpResponseMessage GetPersonByName(string FirstName, string LastName)
        {
            var content = databasePlaceholder.GetAll().Where(c => c.FirstName == FirstName && c.LastName == LastName);

            var Data = JsonConvert.SerializeObject(content);
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = Data,
                StatusCodeDes = "",
                IsSuccess = true,
                StatusCode = (int)System.Net.HttpStatusCode.OK
            };

            var response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };


            return response;

        }


        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="person">用户实体</param>
        /// <returns></returns>
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




        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="person">用户实体</param>
        /// <returns></returns>
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




        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public HttpResponseMessage DeletePerson(int id)
        {
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = "ID不存在，无法删除",
                StatusCodeDes = "",
                IsSuccess = false,
                StatusCode = (int)HttpStatusCode.OK
            };
            var response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };

            Person person = databasePlaceholder.Get(id);

            if (person == null)
            {

                throw new HttpResponseException(response);

            }

            databasePlaceholder.Remove(id);

            viewModel.Data = "删除";
            viewModel.StatusCodeDes = "";
            viewModel.IsSuccess = true;
            viewModel.StatusCode = (int)HttpStatusCode.OK;


            response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };

            return response;

        }

    }

}
