using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrivateOfficeAPI.Models;

namespace PrivateOfficeAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class CourseController : Controller
    {
	    private readonly HttpClient _httpClient;

	    public CourseController()
	    {
		    var clientHandler = new HttpClientHandler
		    {
			    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
		    };
		    _httpClient = new HttpClient(clientHandler);
	    }

        [HttpGet("Courses")]
        public async Task<List<Course>> GetCourse()
        {
	        HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Courses");
            string jsonString = await response.Content.ReadAsStringAsync();
            var course = JsonConvert.DeserializeObject<List<Course>>(jsonString);
            return course;
        }
        
        [HttpGet("Courses&id_teacher={idTeacher}")]
        public async Task<List<Course>> GetCourses(int idTeacher)
        {
	        var response = await _httpClient.GetAsync("https://localhost:44316/api/Courses");
            var jsonString = await response.Content.ReadAsStringAsync();
            var coursesResponse = JsonConvert.DeserializeObject<List<Course>>(jsonString);
            var courses = new List<Course>();
            foreach (var course in coursesResponse)
            {
	            if (course.IdTeacher == idTeacher)
		            courses.Add(course);
            }
            return courses;
        }

        [HttpPost("Course")]
        public async void CreateCourse(Course course)
        {
            var jsonString = JsonConvert.SerializeObject(course);
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("https://localhost:44316/api/Courses", httpContent);
        }

        [HttpPut("Course&id={id}")]
        public async Task<Course> UpdateCourse(int id, Course course)
        {
            var jsonRequest= JsonConvert.SerializeObject(course);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:44316/api/Courses/" + id.ToString(), httpContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            course = JsonConvert.DeserializeObject<Course>(jsonResponse);
            return course;
        }

        [HttpDelete("Course&id={id}")]
        public async Task<Course> DeleteCourse(int id)
        {
	        var response = await _httpClient.DeleteAsync("https://localhost:44316/api/Courses/" + id.ToString());
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        var course = JsonConvert.DeserializeObject<Course>(jsonResponse);
	        return course;
        }
    }
}