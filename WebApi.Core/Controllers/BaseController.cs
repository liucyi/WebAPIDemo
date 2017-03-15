using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Cors;
namespace WebApi.Core.Model
{
    [EnableCors("AllowSameDomain")]
    public class BaseController : Controller
    {

    }
}
