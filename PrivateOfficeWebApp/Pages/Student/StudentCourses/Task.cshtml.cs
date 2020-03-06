using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp
{
    public class TaskModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public TaskModel()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(clientHandler);
        }
        [BindProperty]
        public List<Homework> Homeworks { get; set; }

        [BindProperty]
        public Student Student { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {

            if (id == null)
                return NotFound();

            if (Request.Cookies["token_auth"] != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Homework/GetHomeworkFromStudent/id=" + id);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Homeworks = JsonConvert.DeserializeObject<List<Homework>>(jsonResponse);

            foreach (var homework in Homeworks)
            {
                response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/" + homework.IdClasses);
                jsonResponse = await response.Content.ReadAsStringAsync();
                var classes = JsonConvert.DeserializeObject<Classes>(jsonResponse);
                homework.Classes = classes;
            }

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/" + id);
            jsonResponse = await response.Content.ReadAsStringAsync();
            Student = JsonConvert.DeserializeObject<Student>(jsonResponse);

            return Page();
        }
        public async Task<IActionResult> OnPostLogOut()
        {
            Response.Cookies.Delete("token_auth");
            Response.Cookies.Delete("login");
            Response.Cookies.Delete("idStudent");
            return Redirect(AppSettings.WebAppUrl + "/Index");
        }
    }
}