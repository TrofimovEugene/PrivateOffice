using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateOfficeAPI.Models;

namespace PrivateOfficeAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
	    private readonly HttpClient _httpClient;
	    public ClassesController()
	    {
		    var clientHandler = new HttpClientHandler
		    {
			    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
		    };
		    _httpClient = new HttpClient(clientHandler);
	    }
        // GET: api/Classes
        [HttpGet("Classes")]
        public async Task<List<Classes>> GetClasses()
        {
            var classes = new List<Classes>();

            return classes;
        }

        // GET: api/Classes/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Classes
        [HttpPost]
        public void Post()
        {
        }

        // PUT: api/Classes/5
        [HttpPut("{id}")]
        public void Put(int id)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
