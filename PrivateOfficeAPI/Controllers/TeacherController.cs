using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrivateOfficeAPI.Models;

namespace PrivateOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly HttpClientHandler _clientHandler;
        private readonly HttpClient _httpClient;

        public TeacherController()
        {
            _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            _httpClient = new HttpClient(_clientHandler);
        }
        
        // GET: api/Teacher
        [HttpGet]
        public async Task<List<Teacher>> GetTeacher()
        {
            var teachers = new List<Teacher>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Teachers");
            string jsonString = await response.Content.ReadAsStringAsync();
            teachers = JsonConvert.DeserializeObject<List<Teacher>>(jsonString);
            return teachers;
        }
    }
}