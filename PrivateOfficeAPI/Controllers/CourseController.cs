using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrivateOfficeAPI.Models;

namespace PrivateOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
	    private readonly HttpClientHandler _clientHandler;
	    private readonly HttpClient _httpClient;

	    public CourseController()
	    {
			_clientHandler = new HttpClientHandler();
			_clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
			{
				return true;
			};
			_httpClient = new HttpClient(_clientHandler);
	    }

        [HttpGet("{id}")]
        public async Task<Course> GetCourse(int id)
        {
            var course = new Course();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Courses/"+id.ToString());

            string jsonString = await response.Content.ReadAsStringAsync();
            course = JsonConvert.DeserializeObject<Course>(jsonString);
 
            return course;
        }
        
        [HttpGet]
        public async Task<List<Course>> GetCourses()
        {
            var courses = new List<Course>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Courses");

            string jsonString = await response.Content.ReadAsStringAsync();
            courses = JsonConvert.DeserializeObject<List<Course>>(jsonString);
            return courses;
        }

        [HttpPost]
        public async void CreateCourse(Course course)
        {
            
            string jsonString = JsonConvert.SerializeObject(course);
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("https://localhost:44316/api/Courses", httpContent);
        }

        [HttpPut("{id}")]
        public async Task<Course> UpdateCourse(int id, Course course)
        {
            string jsonRequest= JsonConvert.SerializeObject(course);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync("https://localhost:44316/api/Courses/" + id.ToString(), httpContent);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            course = JsonConvert.DeserializeObject<Course>(jsonResponse);
            return course;
        }
    }
}