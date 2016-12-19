using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Newtonsoft.Json.Serialization;
using WebApi.Filters;

namespace WebApi
{
    public static class WebApiConfig
    {
        public const string DEFAULT_ROUTE_NAME = "MyDefaultRoute";
        public static void Register(HttpConfiguration config)
        {

            
            // Web API 配置和服务
            config.EnableCors();
            // Web API configuration and services
            var json = config.Formatters.JsonFormatter;
            // 解决json序列化时的循环引用问题
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 干掉XML序列化器
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
           
            config.Routes.MapHttpRoute(
                name: DEFAULT_ROUTE_NAME,
                routeTemplate: "api/{controller}/{id}",
                //           routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            #region api版本控制
            //api版本控制
            //config.Routes.MapHttpRoute(
            //     name: "DefaultVersion",
            //     routeTemplate: "api/{version}/{controller}/{id}",
            //     defaults: new { id = RouteParameter.Optional }
            // );
            //config.Services.Replace(typeof(IHttpControllerSelector), new VersionHttpControllerSelector((config))); 
            #endregion
            ////2.自定义路由一：匹配到action
            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "actionapi/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            ////3.自定义路由二
            //config.Routes.MapHttpRoute(
            //    name: "TestApi",
            //    routeTemplate: "testapi/{controller}/{ordertype}/{id}",
            //    defaults: new { ordertype = "aa", id = RouteParameter.Optional }
            //);


            // 取消注释下面的代码行可对具有 IQueryable 或 IQueryable<T> 返回类型的操作启用查询支持。
            // 若要避免处理意外查询或恶意查询，请使用 QueryableAttribute 上的验证设置来验证传入查询。
            // 有关详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=279712。
            //config.EnableQuerySupport();

            // 若要在应用程序中禁用跟踪，请注释掉或删除以下代码行
            // 有关详细信息，请参阅: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            //异常处理
            // config.Filters.Add(new WebApiExceptionFilterAttribute());

            // 启用Web API特性路由
          config.MapHttpAttributeRoutes();
        }
    }
}
