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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Core.Controllers
{
    [Route("api/{customerid}/[controller]")]
    public class ProjectController : Controller
    {

        private IProjectRepository projectRepository = new ProjectRepository();
        // GET: api/values
        [HttpGet]

        public IEnumerable<Project> Get()
        {
            var model = projectRepository.GetAll();
            return model;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Project Get(int id)
        {
            var model = projectRepository.Get(id);
            return model;
        }
        [HttpGet]
        [Route("SearchAll")]
        public IEnumerable<Project> Get([FromBody]Project value)
        {
            var model = projectRepository.GetAll();
            return model;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Project value)
        {
            var model = projectRepository.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Project value)
        {
            var model = projectRepository.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            projectRepository.Remove(id);
        }
    }
}
