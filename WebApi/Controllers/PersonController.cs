using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using WebApi.Models;
using System.Web.Http.Cors;
using System.Web.Security;
using WebApi.Filters;

namespace WebApi.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [RoutePrefix("api/Person")]

    public class PersonController : ApiController
    {
        static readonly IPersonRepository databasePlaceholder = new PersonRepository();


        /// <summary>
        /// 获取所有人信息
        /// </summary>
        /// <returns></returns>

         // [Route("GetAllPeople")]
   
        public HttpResponseMessage GetAllPeople()
        {

            var content = databasePlaceholder.GetAll();

            var Data = JsonConvert.SerializeObject(content);
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = Data,
                StatusCodeDes = System.Enum.GetName(typeof(HttpStatusCode), HttpStatusCode.OK),
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK
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
        [Route("GetPersonByID/{id}")]
        [EnableCors(origins: "*", headers: "GET,POST", methods: "*")]
 
        [CrossSite]
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

        [Route("GetPersonByName/{FirstName}/{LastName}")]
        [RequestAuthorize]

        public HttpResponseMessage GetPersonByName(string FirstName, string LastName)
        {
            var content = databasePlaceholder.GetAll().Where(c => c.FirstName == FirstName && c.LastName == LastName);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, FirstName, DateTime.Now,
                           DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", FirstName, LastName),
                           FormsAuthentication.FormsCookiePath);
            //返回登录结果、用户信息、用户验证票据信息
            var oUser = new UserInfo { bRes = true, UserName = FirstName, Password = LastName, Ticket = FormsAuthentication.Encrypt(ticket) };
            //将身份信息保存在session中，验证当前请求是否是有效请求
            HttpContext.Current.Session[FirstName] = oUser;

            var Data = JsonConvert.SerializeObject(oUser);
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = oUser,
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
        [HttpPost]
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
        [HttpPut]
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
        [HttpDelete]
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


        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="person">用户实体</param>
        /// <returns></returns>
        [Route("PostPersonList")]
        [HttpPost]
        public HttpResponseMessage PostPersonList(dynamic person)
        {


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

        public class UserInfo
        {
            public bool bRes { get; set; }

            public string UserName { get; set; }

            public string Password { get; set; }

            public string Ticket { get; set; }
        }
    }

}
