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
using WebApi.Repository;

namespace WebApi.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>

 
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        static readonly IPersonRepository databasePlaceholder = new PersonRepository();

       
        [HttpPost]
        public HttpResponseMessage Post(string FirstName, string Password)
        {
            var content = databasePlaceholder.GetAll().Where(c => c.FirstName == FirstName && c.Password == Password);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, FirstName, DateTime.Now,
                           DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", FirstName, Password),
                           FormsAuthentication.FormsCookiePath);
            //返回登录结果、用户信息、用户验证票据信息
            var oUser = new UserInfo { bRes = true, UserName = FirstName, Password = Password, Ticket = FormsAuthentication.Encrypt(ticket) };
            //将身份信息保存在session中，验证当前请求是否是有效请求
            HttpContext.Current.Session[FirstName] = oUser;

            var Data = JsonConvert.SerializeObject(content);
            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = oUser,
                StatusCodeDes = "",
                IsSuccess = true,
                StatusCode = (int)System.Net.HttpStatusCode.OK
            };

            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json"),
                
             
            };
            
            
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
