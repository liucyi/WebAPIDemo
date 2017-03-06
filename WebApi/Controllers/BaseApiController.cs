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
 
    [RequestAuthorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    
    public class BaseApiController : ApiController
    {
        static readonly IPersonRepository databasePlaceholder = new PersonRepository();



       
    }

}
