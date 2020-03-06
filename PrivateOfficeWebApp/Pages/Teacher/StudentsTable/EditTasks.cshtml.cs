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
    public class EditTasksModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public EditTasksModel()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(clientHandler);
        }
        [BindProperty]
        public Homework Homework { get; set; }

        [BindProperty]
        public Classes Classes { get; set; }
        [BindProperty]
        public List<Classes> Clas { get; set; }

        [BindProperty]
        public Student Student { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {

            if (id == null)
                return NotFound();

            if (Request.Cookies["token_auth"] != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Homework/" + id);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Homework = JsonConvert.DeserializeObject<Homework>(jsonResponse);

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/" + Homework.IdClasses);
            jsonResponse = await response.Content.ReadAsStringAsync();
            Classes = JsonConvert.DeserializeObject<Classes>(jsonResponse);
            Homework.Classes = Classes;

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/GetClassesFromGroup/id=" + Homework.IdGroup);
            jsonResponse = await response.Content.ReadAsStringAsync();
            Clas = JsonConvert.DeserializeObject<List<Classes>>(jsonResponse);

            return Page();
        }

        public async Task<IActionResult> OnPostEditTask(int idStudent, int Idclasses, int id)
        {
            if (Request.Cookies["token_auth"] != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            Homework.IdStudent = idStudent;
            Homework.IdClasses = Idclasses;
            Homework.IdHomework = id;

            var jsonRequest = JsonConvert.SerializeObject(Homework);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/Homework/" + id, httpContent);
            //return Redirect(jsonRequest);
            return Redirect(AppSettings.WebAppUrl + "/Teacher/StudentsTable/Tasks?id=" + idStudent);
        }

        public async Task<IActionResult> OnPostLogOut()
        {
            Response.Cookies.Delete("token_auth");
            Response.Cookies.Delete("login");
            Response.Cookies.Delete("idTeacher");
            Response.Cookies.Delete("role");
            return Redirect(AppSettings.WebAppUrl + "/Index");
        }
    }
}