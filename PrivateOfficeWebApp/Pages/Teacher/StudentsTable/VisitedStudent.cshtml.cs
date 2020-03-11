using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using PrivateOfficeWebApp.PagesModels;

namespace PrivateOfficeWebApp
{
    public class VisitedStudentModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public VisitedStudentModel()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(clientHandler);
        }

        [BindProperty] public List<PagesModels.Student> Students { get; set; }

        [BindProperty] public VisitedStudent VisitedStudent { get; set; }
        [BindProperty] public List<VisitedStudent> VisitedStudents { get; set; }
        public Classes Classes { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
	        if (Request.Cookies["token_auth"] != null)
		        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/" + id);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Classes = JsonConvert.DeserializeObject<Classes>(jsonResponse);

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/GetStudentFromGroup/id=" +
                                                  Classes.IdGroup);
            jsonResponse = await response.Content.ReadAsStringAsync();
            Students = JsonConvert.DeserializeObject<List<PagesModels.Student>>(jsonResponse);

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl +
                                                  "/api/VisitedStudents/GetVisitedFromClasses/id=" + id);
            jsonResponse = await response.Content.ReadAsStringAsync();
            VisitedStudents = JsonConvert.DeserializeObject<List<VisitedStudent>>(jsonResponse);

            foreach (var student in Students)
            {
                response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/" + student.IdGroup);
                jsonResponse = await response.Content.ReadAsStringAsync();
                var group = JsonConvert.DeserializeObject<Group>(jsonResponse);
                student.Group = group;

            }

            return Page();
        }


        public async Task<IActionResult> OnPostUpdateStudent(int idClasses, int idStudent, bool visited)
        {
	        if (Request.Cookies["token_auth"] != null)
		        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            var value = true;

            HttpResponseMessage response =
                await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/VisitedStudents/GetVisitedFromClasses/id=" +
                                           idClasses);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            VisitedStudents = JsonConvert.DeserializeObject<List<PagesModels.VisitedStudent>>(jsonResponse);

            VisitedStudent.IdClasses = idClasses;
            VisitedStudent.IdStudent = idStudent;
            VisitedStudent.Visited = visited;


            if (VisitedStudents.Count == 0)
            {
                var jsonRequest = JsonConvert.SerializeObject(VisitedStudent);
                HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/VisitedStudents", httpContent);
               
            }
            else
            {
                foreach (var visit in VisitedStudents)
                {
                    if (visit.IdStudent == idStudent)
                    {
                        VisitedStudent.IdVisitedStudent = visit.IdVisitedStudent;
                        VisitedStudent.ConfirmVisited = visit.ConfirmVisited;

                        var jsonRequest = JsonConvert.SerializeObject(VisitedStudent);
                        HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                        await _httpClient.PutAsync(
                            AppSettings.DataBaseUrl + "/api/VisitedStudents/" + VisitedStudent.IdVisitedStudent,
                            httpContent);
                        
                        value = true;
                    }
                    else
                    {
                        value = false;
                    }
                }
            }

            if (value == false)
            {
                var jsonRequest = JsonConvert.SerializeObject(VisitedStudent);
                HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/VisitedStudents", httpContent);
               
            }

            return Redirect(AppSettings.WebAppUrl + "/Teacher/StudentsTable/VisitedStudent?id=" +
                            idClasses);
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