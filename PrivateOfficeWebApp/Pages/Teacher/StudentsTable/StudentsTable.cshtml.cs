using System.Collections.Generic;
using System.Net.Http;
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
			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/GetStudentFromGroup&id=" + id);
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
			Student.IdGroup = idgroup;
			var jsonRequest = JsonConvert.SerializeObject(Student);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Students", httpContent);

			return Redirect(AppSettings.WebAppUrl + "/Teacher/StudentsTable/StudentsTable?id=" + idgroup);
		}

        public async Task<IActionResult> OnPostUpdateStudent(int idgroup)
        {
            Student.IdGroup = idgroup;

            var jsonRequest = JsonConvert.SerializeObject(Student);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/Students/" + Student.IdStudent, httpContent);
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
            [JsonProperty("visited")]
            public bool Visited { get; set; }
            [JsonProperty("idStudent")]
            public int IdStudent { get; set; }

        }
		public async Task<IActionResult> OnPostDelete(int id, int idgroup)
		{
			await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/Students/" + id);
			return Redirect("https://localhost:44326/Teacher/StudentsTable/StudentsTable?id=" + idgroup);
		}
	}
}