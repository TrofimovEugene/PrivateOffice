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
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly HttpClient _httpClient;
		public IndexModel(ILogger<IndexModel> logger)
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
		public async Task<IActionResult> OnGet(int? id)
		{
			HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Courses");
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Courses = JsonConvert.DeserializeObject<List<Course>>(jsonResponse);
			return Page();
		}

		[BindProperty]
		public RequestCourse Course { get; set; }
		public async Task<IActionResult> OnPostAsync()
		{
			Course.IdTeacher = 1;
			var jsonRequest = JsonConvert.SerializeObject(Course);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PostAsync("https://localhost:44316/api/Courses", httpContent);
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
		}

		public async Task<IActionResult> OnPostDelete(int id)
		{
			await _httpClient.DeleteAsync("https://localhost:44316/api/Courses/" + id);
			return RedirectToPage("./IndexCourse");
		}
	}
}
