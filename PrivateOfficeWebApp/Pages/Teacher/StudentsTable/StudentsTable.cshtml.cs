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

namespace PrivateOfficeWebApp.Pages.Teacher.StudentsTable
{
    public class StudentsTableModel : PageModel
    {
		private readonly HttpClient _httpClient;

		public StudentsTableModel()
		{
			var clientHandler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
			};
			_httpClient = new HttpClient(clientHandler);
		}
		[BindProperty]
		public List<PagesModels.Student> Students { get; set; }
		public async Task<IActionResult> OnGet(int id)
		{
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/GetStudentFromGroup/id=" + id);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Students = JsonConvert.DeserializeObject<List<PagesModels.Student>>(jsonResponse);

			foreach (var student in Students)
			{
				response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/" + student.IdGroup);
				jsonResponse = await response.Content.ReadAsStringAsync();
				var group = JsonConvert.DeserializeObject<Group>(jsonResponse);
				student.Group = group;

			}

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/");
			jsonResponse = await response.Content.ReadAsStringAsync();
			Groups = JsonConvert.DeserializeObject<List<Group>>(jsonResponse);


			return Page();
		}

        [BindProperty]
		public ResponseStudent Student { get; set; }
		[BindProperty]
		public List<Group> Groups { get; set; }

        public async Task<IActionResult> OnPostCreateStudent(int idgroup)
        {
	        if (Request.Cookies["token_auth"] != null)
		        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

			Student.IdGroup = idgroup;
			//Student.Role = "user";
            var jsonRequest = JsonConvert.SerializeObject(Student);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Students", httpContent);
	
            return Redirect(AppSettings.WebAppUrl + "/Teacher/StudentsTable/StudentsTable?id=" + idgroup);
        }


		   [JsonObject]
		public class ResponseStudent
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
			[JsonProperty("role")]
			public string Role { get; set; }

		}
		[BindProperty]
		public List<Homework> Homeworks { get; set; }
		[BindProperty] public List<VisitedStudent> VisitedStudents { get; set; }
		public async Task<IActionResult> OnPostDelete(int id, int idgroup)
		{
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);


			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl +
												  "/api/VisitedStudents/GetVisitedFromStudent/id=" + id);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			VisitedStudents = JsonConvert.DeserializeObject<List<PagesModels.VisitedStudent>>(jsonResponse);
			
			foreach(var visitStudent in VisitedStudents)
			{
				await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/VisitedStudents/" + visitStudent.IdVisitedStudent);
			}

			 response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Homework/GetHomeworkFromStudent/id=" + id);
			 jsonResponse = await response.Content.ReadAsStringAsync();
			Homeworks = JsonConvert.DeserializeObject<List<Homework>>(jsonResponse);

			if (Homeworks != null)
			{
				foreach (var homework in Homeworks)
				{
					await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/Homework/" + homework.IdHomework);
				}
			}


			await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/Students/" + id);
			return Redirect("https://localhost:44326/Teacher/StudentsTable/StudentsTable?id=" + idgroup);
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