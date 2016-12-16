﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Filters
{
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        //重写基类的验证方式，加入我们自定义的Ticket验证
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                //解密用户ticket,并校验用户名密码是否匹配
                var encryptTicket = authorization.Parameter;
                if (ValidateTicket(encryptTicket))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
                {
                    Data = "用户未登录",
                    StatusCodeDes = "",
                    IsSuccess = false,
                    StatusCode = (int)System.Net.HttpStatusCode.OK
                };
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                var response = new HttpResponseMessage
                {
                    Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json"),
                };
                if (isAnonymous) base.OnAuthorization(actionContext);

                else throw new HttpResponseException(response);



                //var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                //bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                //if (isAnonymous) base.OnAuthorization(actionContext);
                //else HandleUnauthorizedRequest(actionContext);
            }
        }

        //校验用户名密码（正式环境中应该是数据库校验）
        private bool ValidateTicket(string encryptTicket)
        {
            try
            {
                //解密Ticket
                var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;

                //从Ticket里面获取用户名和密码
                var index = strTicket.IndexOf("&");
                string strUser = strTicket.Substring(0, index);
                string strPwd = strTicket.Substring(index + 1);

                if (strUser == "123" && strPwd == "123")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
                {
                    Data = "票据错误",
                    StatusCodeDes = Enum.GetName(typeof(HttpStatusCode), HttpStatusCode.NonAuthoritativeInformation),
                    IsSuccess = false,
                    StatusCode = (int)System.Net.HttpStatusCode.NonAuthoritativeInformation
                };
                var response = new HttpResponseMessage
                {
                    Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json"),
                };


                throw new HttpResponseException(response);
            }
        }
    }
}