using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrivateOfficeAPI.Models;

namespace PrivateOfficeAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        [HttpGet("{id}")]
        public async Task<Course> GetCourse(int id)
        {
            var course = new Course();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:44316/api/Courses/"+id.ToString());

            string jsonString = await response.Content.ReadAsStringAsync();
            course = JsonConvert.DeserializeObject<Course>(jsonString);
 
            return course;
        }
        
        [HttpGet]
        public async Task<List<Course>> GetCourses()
        {
            var courses = new List<Course>();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:44316/api/Courses");

            string jsonString = await response.Content.ReadAsStringAsync();
            courses = JsonConvert.DeserializeObject<List<Course>>(jsonString);
            return courses;
        }

        [HttpPost]
        public async void CreateCourse(Course course)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            string jsonString = JsonConvert.SerializeObject(course);
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("https://localhost:44316/api/Courses", httpContent);
        }

        [HttpPut("{id}")]
        public async Task<Course> UpdateCourse(int id, Course course)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            HttpClient httpClient = new HttpClient(clientHandler);
            string jsonRequest= JsonConvert.SerializeObject(course);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PutAsync("https://localhost:44316/api/Courses/" + id.ToString(), httpContent);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            course = JsonConvert.DeserializeObject<Course>(jsonResponse);
            return course;
        }
    }
}