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
		public List<Classes> Classes { get; set; }
		[BindProperty]
		public Students Student { get; set; }
		public async Task<IActionResult> OnGet(int? idStudent)
		{

			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/" + idStudent);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Student = JsonConvert.DeserializeObject<Students>(jsonResponse);

			 response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/GetClassesFromGroup&id=" + Student.IdGroup);
			 jsonResponse = await response.Content.ReadAsStringAsync();
			Classes = JsonConvert.DeserializeObject<List<Classes>>(jsonResponse);

			if(Classes != null) { 
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


	}
}