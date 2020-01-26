using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrivateOfficeAPI.Models;

namespace PrivateOfficeAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly HttpClient _httpClient;

        public TeacherController()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(clientHandler);
        }
        
        // GET: api/Teacher
        [HttpGet("Teachers")]
        public async Task<List<Teacher>> GetTeacher()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Teachers");
            string jsonString = await response.Content.ReadAsStringAsync();
            var teachers = JsonConvert.DeserializeObject<List<Teacher>>(jsonString);
            return teachers;
        }
    }
}