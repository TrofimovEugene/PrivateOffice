using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.PagesModels;

namespace PrivateOfficeWebApp.Pages.Student.StudentCourses
{
    public class Task : PageModel
    {
	    private readonly HttpClient _httpClient;
	    public Task()
	    {
		    var clientHandler = new HttpClientHandler
		    {
			    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
		    };
		    _httpClient = new HttpClient(clientHandler);
		}

		[BindProperty]
		public List<Classes> Classes { get; set; }
		[BindProperty]
		public IndexStudentModel.Students Student { get; set; }
		[BindProperty] public VisitedStudent VisitedStudent { get; set; }
		[BindProperty] public List<VisitedStudent> VisitedStudents { get; set; }
		public async Task<IActionResult> OnGet(int? id)
        {
	        if (id == null)
		        return NotFound();

	        if (Request.Cookies["token_auth"] != null)
		        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

		
			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/" + id);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Student = JsonConvert.DeserializeObject<IndexStudentModel.Students>(jsonResponse);

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/GetClassesFromGroup/id=" + Student.IdGroup);
			jsonResponse = await response.Content.ReadAsStringAsync();
			Classes = JsonConvert.DeserializeObject<List<Classes>>(jsonResponse);

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/VisitedStudents/GetVisitedFromStudent/id=" + id);
			jsonResponse = await response.Content.ReadAsStringAsync();
			VisitedStudents = JsonConvert.DeserializeObject<List<VisitedStudent>>(jsonResponse);

			if (Classes != null)
			{
			

				foreach (var classes in Classes)
				{
					response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses/" + classes.IdCourse);
					jsonResponse = await response.Content.ReadAsStringAsync();
					var course = JsonConvert.DeserializeObject<Course>(jsonResponse);
					classes.Course = course;
				}
			}

			return Page();
        }

		public async Task<IActionResult> OnPostVisitStudent(int idClasses, int idStudent, bool visited)
		{
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

			var value = true;

			HttpResponseMessage response =
				await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/VisitedStudents/GetVisitedFromStudent/id=" + idStudent);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			VisitedStudents = JsonConvert.DeserializeObject<List<VisitedStudent>>(jsonResponse);

			VisitedStudent.IdClasses = idClasses;
			VisitedStudent.IdStudent = idStudent;
			VisitedStudent.ConfirmVisited = visited;

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
					if (visit.IdClasses == idClasses)
					{
						VisitedStudent.IdVisitedStudent = visit.IdVisitedStudent;
						VisitedStudent.Visited = visit.Visited;

						var jsonRequest = JsonConvert.SerializeObject(VisitedStudent);
						HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
						await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/VisitedStudents/" + VisitedStudent.IdVisitedStudent, httpContent);

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

			return Redirect("./Task?id=" + idStudent);
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