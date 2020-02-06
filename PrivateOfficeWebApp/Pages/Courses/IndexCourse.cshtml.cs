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

namespace PrivateOfficeWebApp.Pages
{
	public class IndexCourseModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly HttpClient _httpClient;
		public IndexCourseModel(ILogger<IndexModel> logger)
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
		public List<Group> Groups { get; set; }
		public async Task<IActionResult> OnGet(int? id)
		{
			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses");
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Courses = JsonConvert.DeserializeObject<List<Course>>(jsonResponse);

			foreach (var itemCourse in Courses)
			{
				response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/" + itemCourse.IdGroup);
				jsonResponse = await response.Content.ReadAsStringAsync();
				var @group = JsonConvert.DeserializeObject<Group>(jsonResponse);
				itemCourse.Group = @group;
			}

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/");
			jsonResponse = await response.Content.ReadAsStringAsync();
			Groups = JsonConvert.DeserializeObject<List<Group>>(jsonResponse);

			return Page();
		}

		[BindProperty]
		public RequestCourse Course { get; set; }
		// ReSharper disable once IdentifierTypo
		public async Task<IActionResult> OnPostCreateCourse(int idgroup)
		{
			Course.IdTeacher = 1;
			Course.IdGroup = idgroup;
			var jsonRequest = JsonConvert.SerializeObject(Course);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Courses", httpContent);
			return RedirectToPage("./IndexCourse");
		}
		[JsonObject]
		public class RequestCourse
		{
			[JsonProperty("idTeacher")]
			public int IdTeacher { get; set; }
			[JsonProperty("nameCourse")]
			public string NameCourse { get; set; }
			[JsonProperty("startDate")]
			public DateTime StartDate { get; set; }
			[JsonProperty("endDate")]
			public DateTime EndDate { get; set; }
			[JsonProperty("nameUniversity")]
			public string NameUniversity { get; set; }
			[JsonProperty("countTime")]
			public int CountTime { get; set; }
			[JsonProperty("idGroup")]
			public int IdGroup { get; set; }
		}

		public async Task<IActionResult> OnPostDelete(int id)
		{
			await _httpClient.DeleteAsync("https://localhost:44316/api/Courses/" + id);
			return RedirectToPage("./IndexCourse");
		}
	}
}
