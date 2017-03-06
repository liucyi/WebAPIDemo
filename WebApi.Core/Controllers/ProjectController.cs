using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Core.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {

        private IProjectRepository projectRepository = new ProjectRepository();
        // GET: api/values
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var model = projectRepository.GetAll();

            HttpResponseMessageViewModel viewModel = new HttpResponseMessageViewModel()
            {
                Data = model,
                StatusCodeDes = System.Enum.GetName(typeof(HttpStatusCode), HttpStatusCode.OK),
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK
            };

            var response = new HttpResponseMessage
            { Content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json") };


            return response;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
