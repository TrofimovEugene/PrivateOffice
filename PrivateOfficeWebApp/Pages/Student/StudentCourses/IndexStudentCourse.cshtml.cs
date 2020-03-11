using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp.Pages.Student.StudentCourses
{
	public class IndexStudentModel : PageModel
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<IndexModel> _logger;
		public IndexStudentModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
			var clientHandler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
			};
			_httpClient = new HttpClient(clientHandler);
		}

		[BindProperty]
		public List<Course> Courses { get; set; }
		[BindProperty]
		public List<Classes> Classes { get; set; }
		[BindProperty]
		public Classes Class { get; set; }
		[BindProperty]
		public Students Student { get; set; }

		public async Task<IActionResult> OnGet(int? id)
		{
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/" + id);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Student = JsonConvert.DeserializeObject<Students>(jsonResponse);

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/GetClassesFromGroup/id=" + Student.IdGroup);
			jsonResponse = await response.Content.ReadAsStringAsync();
			Classes = JsonConvert.DeserializeObject<List<Classes>>(jsonResponse);

			if (Classes != null) { 

				foreach (var classes in Classes)
				{
					response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/" + classes.IdClasses);
					jsonResponse = await response.Content.ReadAsStringAsync();
					var course = JsonConvert.DeserializeObject<Course>(jsonResponse);
					Class = JsonConvert.DeserializeObject<Classes>(jsonResponse);
				}
			}

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses/GetCourseFromGroup/id=" + Student.IdGroup);
			jsonResponse = await response.Content.ReadAsStringAsync();
			Courses = JsonConvert.DeserializeObject<List<Course>>(jsonResponse);
			
			if (Courses != null)
			{
				foreach (var courses in Courses)
				{
					response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Teachers/" + courses.IdTeacher);
					jsonResponse = await response.Content.ReadAsStringAsync();
					var teacher = JsonConvert.DeserializeObject<Models.Teacher>(jsonResponse);
					courses.Teacher = teacher;
					
				}
			}

			return Page();
		}

		[BindProperty] public VisitedStudent VisitedStudent { get; set; }
		[BindProperty] public List<VisitedStudent> VisitedStudents { get; set; }
		public async Task<IActionResult> OnPostVisited(int idStudent, int id)
		{
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);
			var value = true;
			var idClasses = 0;
			var timeValue = false;

			HttpResponseMessage response =
			await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/VisitedStudents/GetVisitedFromStudent/id=" + idStudent);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			VisitedStudents = JsonConvert.DeserializeObject<List<VisitedStudent>>(jsonResponse);

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/" + idStudent);
			jsonResponse = await response.Content.ReadAsStringAsync();
			Student = JsonConvert.DeserializeObject<Students>(jsonResponse);

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/GetClassesFromGroup/id=" + Student.IdGroup);
			jsonResponse = await response.Content.ReadAsStringAsync();
			Classes = JsonConvert.DeserializeObject<List<Classes>>(jsonResponse);

			TimeSpan time = DateTime.Now.TimeOfDay;

			if (Classes != null)
			{

				foreach (var classes in Classes)
				{
					response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/" + classes.IdClasses);
					jsonResponse = await response.Content.ReadAsStringAsync();
					var course = JsonConvert.DeserializeObject<Course>(jsonResponse);
					Class = JsonConvert.DeserializeObject<Classes>(jsonResponse);

					if (time >= Class.StartTime && time <= Class.EndTime)
					{
						VisitedStudent.IdClasses = classes.IdClasses;
						VisitedStudent.IdStudent = idStudent;
						VisitedStudent.ConfirmVisited = true;
						idClasses = classes.IdClasses;
						timeValue = true;

					}
				}
			}

			if (timeValue == false)
			{
				return Redirect("./StudentClasses?id=" + id);
			}
			else { 
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
			return Redirect("./StudentClasses?id=" + id);
			}
		}
		
		[JsonObject]
		public class Students
		{
			[JsonProperty("idGroup")]
			public int IdGroup { get; set; }
			[JsonProperty("firstName")]
			public string FirstName { get; set; }
			[JsonProperty("secondName")]
			public string SecondName { get; set; }

			[JsonProperty("idStudent")]
			public int IdStudent { get; set; }

			[JsonProperty("login")]
			public string Login { get; set; }
			[JsonProperty("password")]
			public string Password { get; set; }


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