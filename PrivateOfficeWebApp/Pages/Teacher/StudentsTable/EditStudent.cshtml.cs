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
    public class EditStudentModel : PageModel
    {
		private readonly HttpClient _httpClient;
		public EditStudentModel()
		{

			var clientHandler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
			};
			_httpClient = new HttpClient(clientHandler);
		}


		[BindProperty]
		public PagesModels.Student Student { get; set; }
		[BindProperty]
		public List<Group> Groups { get; set; }
		public async Task<IActionResult> OnGet(int? id)
		{
			if (id == null)
				return NotFound();

			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/" + id);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Student = JsonConvert.DeserializeObject<PagesModels.Student>(jsonResponse);

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/" + Student.IdGroup);
			jsonResponse = await response.Content.ReadAsStringAsync();
			var group = JsonConvert.DeserializeObject<Group>(jsonResponse);
			Student.Group = group;


			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/"); ;
			jsonResponse = await response.Content.ReadAsStringAsync();
			Groups = JsonConvert.DeserializeObject<List<Group>>(jsonResponse);
			if (Student == null)
				return NotFound();
			return Page();
		}
		

		public async Task<IActionResult> OnPostEditStudent(int idgroup)
		{
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

			var reqStudent = new PagesModels.Student
			{
				IdStudent = Student.IdStudent,
				FirstName = Student.FirstName,
				SecondName = Student.SecondName,
				Login = Student.Login,
				Password = Student.Password, 
				IdGroup = idgroup
			};

			var jsonRequest = JsonConvert.SerializeObject(reqStudent);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/Students/" + Student.IdStudent, httpContent);

			return Redirect(AppSettings.WebAppUrl + "/Teacher/StudentsTable/StudentsTable?id=" + idgroup);
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