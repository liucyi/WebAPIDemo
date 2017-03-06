using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    /// <summary>
    /// 跨域访问
    /// </summary>
    public class CrossSiteAttribute : ActionFilterAttribute
    {
        private const string Origin = "Origin";
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        private const string originHeaderdefault = "*";
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.Add(AccessControlAllowOrigin, originHeaderdefault);
        }
    }
}