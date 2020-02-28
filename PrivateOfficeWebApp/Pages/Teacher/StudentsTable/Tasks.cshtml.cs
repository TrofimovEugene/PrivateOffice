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
using PrivateOfficeWebApp.PagesModels;

namespace PrivateOfficeWebApp
{
    public class TasksModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public TasksModel()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(clientHandler);
        }
        [BindProperty]
        public List<Report> Reports { get; set; }

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

            HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Reports/GetReportsFromStudent/id=" + id);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Reports = JsonConvert.DeserializeObject<List<Report>>(jsonResponse);

            if (Reports != null)
            {
                foreach(var report in Reports)
                {
                    response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/" + report.IdClasses);
                    jsonResponse = await response.Content.ReadAsStringAsync();
                    Classes = JsonConvert.DeserializeObject<Classes>(jsonResponse);
                    report.Classes = Classes;
                }
            }

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/" + id);
             jsonResponse = await response.Content.ReadAsStringAsync();
            Student = JsonConvert.DeserializeObject<Student>(jsonResponse);

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/" + Student.IdGroup);
            jsonResponse = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<Group>(jsonResponse);
            Student.Group = group;

             response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/GetClassesFromGroup/id=" + group.IdGroup);
             jsonResponse = await response.Content.ReadAsStringAsync();
            Clas = JsonConvert.DeserializeObject<List<Classes>>(jsonResponse);

            return Page();
        }

        [BindProperty]
        public Report Report { get; set; }
        public async Task<IActionResult> OnPostCreateTask(int idStudent, int Idclasses)
        {
            if (Request.Cookies["token_auth"] != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            Report.IdStudent = idStudent;
            Report.IdClasses = Idclasses;

            var jsonRequest = JsonConvert.SerializeObject(Report);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Reports", httpContent);
            //return Redirect(jsonRequest);
           return Redirect(AppSettings.WebAppUrl + "/Teacher/StudentsTable/Tasks?id=" + idStudent);
        }

        public async Task<IActionResult> OnPostDelete(int id, int idStudent)
        {
            if (Request.Cookies["token_auth"] != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/Reports/" + id);
            return Redirect("https://localhost:44326/Teacher/StudentsTable/Tasks?id=" + idStudent);
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