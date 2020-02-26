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
using PrivateOfficeWebApp.PagesModels;

namespace PrivateOfficeWebApp.Pages.Teacher.Courses
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
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);
			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses/WithTeacher/id=" + id);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Courses = JsonConvert.DeserializeObject<List<Course>>(jsonResponse);
			
			if (Courses != null)
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
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);
			if (Request.Cookies["idTeacher"] != null)
				Course.IdTeacher = Convert.ToInt32(Request.Cookies["idTeacher"]);
			Course.IdGroup = idgroup;
			var jsonRequest = JsonConvert.SerializeObject(Course);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Courses", httpContent);
			return Redirect(AppSettings.WebAppUrl + "/Teacher/Courses/IndexCourse?id=" + Course.IdTeacher);
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
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);
			await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/Courses/" + id);
			if(Request.Cookies["idTeacher"] != null)
				return Redirect(AppSettings.WebAppUrl + "/Teacher/Courses/IndexCourse?idTeacher="+ Request.Cookies["idTeacher"]);
			return NotFound();
		}

		public async Task<IActionResult> OnPostLogOut()
		{
			Response.Cookies.Delete("token_auth");
			Response.Cookies.Delete("login");
			Response.Cookies.Delete("idTeacher");
			Response.Cookies.Delete("role");
			return Redirect(AppSettings.WebAppUrl+"/Index");
		}

		[BindProperty]
		public RequestTeacher Teacher { get; set; }
		public async Task<IActionResult> OnPostRegistryTeacher()
		{
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);
			Teacher.Course = new List<Course>();
			var jsonRequest = JsonConvert.SerializeObject(Teacher);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Teachers", httpContent);
			return Redirect(AppSettings.WebAppUrl + "/Teacher/Courses/IndexCourse?idTeacher=" + Request.Cookies["idTeacher"]);
		}

		[JsonObject]
		public class RequestTeacher
		{
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
			[JsonProperty("course")]
			public virtual List<Course> Course { get; set; }

		}
	}
}
