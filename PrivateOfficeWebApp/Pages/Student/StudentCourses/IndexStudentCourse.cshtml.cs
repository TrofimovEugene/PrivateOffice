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
		public IndexStudentModel()
		{
			var clientHandler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
			};
			_httpClient = new HttpClient(clientHandler);
		}
		[BindProperty]
		public List<Classes> Classes { get; set; }
		public async Task<IActionResult> OnGet(int id)
		{
			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes");
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Classes = JsonConvert.DeserializeObject<List<Classes>>(jsonResponse);

			foreach (var classes in Classes)
			{
				response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses/" + classes.IdCourse);
				jsonResponse = await response.Content.ReadAsStringAsync();
				var course = JsonConvert.DeserializeObject<Course>(jsonResponse);
				classes.Course = course;
			}

			return Page();
		}
		
		
	}
}