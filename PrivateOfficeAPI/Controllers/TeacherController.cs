using System.Collections.Generic;
using System.Net;
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
        [JsonObject]
        public class RequestAuth
        {
            [JsonProperty("login")]
            public string Login { get; set; }
            [JsonProperty("password")]
            public string Password { get; set; }
        }
        public TeacherController()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(clientHandler);
        }
        
        // GET: api/Teachers
        [HttpGet("Teachers")]
        public async Task<List<Teacher>> GetTeacher()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Teachers");
            string jsonString = await response.Content.ReadAsStringAsync();
            var teachers = JsonConvert.DeserializeObject<List<Teacher>>(jsonString);
            return teachers;
        }
        
        //POST: api/Teachers
        [HttpPost("Teacher")]
        public async Task<Teacher> LoginTeacher(RequestAuth requestAuth)
        {
            if (requestAuth == null)
                return null;
            HttpResponseMessage response =
                await _httpClient.GetAsync("https://localhost:44316/api/Teachers/GetTeacherLogin/" + requestAuth.Login);
            string jsonString = await response.Content.ReadAsStringAsync();
            var teacher = JsonConvert.DeserializeObject<Teacher>(jsonString);
            if (teacher.Password == requestAuth.Password)
                return teacher;
            else
                return null;
        }
    }
}