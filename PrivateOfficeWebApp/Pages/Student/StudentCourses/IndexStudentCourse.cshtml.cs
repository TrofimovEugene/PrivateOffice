using System;
using System.Collections.Generic;
using System.Net.Http;
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
		public Students Student { get; set; }
		[BindProperty]
		public Teachers Teacher { get; set; }
		[BindProperty] public VisitedStudent VisitedStudent { get; set; }
		[BindProperty] public List<VisitedStudent> VisitedStudents { get; set; }
		public async Task<IActionResult> OnGet(int? idStudent)
		{

			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/" + idStudent);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Student = JsonConvert.DeserializeObject<Students>(jsonResponse);

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses/GetCourseFromGroup/id=" + Student.IdGroup);
			jsonResponse = await response.Content.ReadAsStringAsync();
			Courses = JsonConvert.DeserializeObject<List<Course>>(jsonResponse);
			
			if (Courses != null)
			{
				foreach (var courses in Courses)
				{
					response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Teachers/" + courses.IdTeacher);
					jsonResponse = await response.Content.ReadAsStringAsync();
					Teacher = JsonConvert.DeserializeObject<Teachers>(jsonResponse);
					
				}
			}

			return Page();
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
		[JsonObject]
		public class Teachers
		{
			[JsonProperty("idTeacher")]
			public int IdTeacher { get; set; }
			[JsonProperty("login")]
			public string Login { get; set; }
			[JsonProperty("password")]
			public string Password { get; set; }
			[JsonProperty("firstName")]
			public string FirstName { get; set; }
			[JsonProperty("secondName")]
			public string SecondName { get; set; }
			[JsonProperty("patronymic")]
			public string Patronymic { get; set; }
			[JsonProperty("role")]
			public string Role { get; set; }
			//[JsonProperty("course")]
			//public virtual List<Course> Course { get; set; }

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