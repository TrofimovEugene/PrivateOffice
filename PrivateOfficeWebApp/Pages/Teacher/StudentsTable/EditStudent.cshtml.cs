using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp
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
		public Student Student { get; set; }
		[BindProperty]
		public List<Group> Groups { get; set; }
		public async Task<IActionResult> OnGet(int? id)
		{
			if (id == null)
				return NotFound();

			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Students/" + id);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Student = JsonConvert.DeserializeObject<Student>(jsonResponse);

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
			var reqStudent = new Student
			{
				IdStudent = Student.IdStudent,
				FirstName = Student.FirstName,
				SecondName = Student.SecondName,
				IdGroup = idgroup
			};


			var jsonRequest = JsonConvert.SerializeObject(reqStudent);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/Students/" + Student.IdStudent, httpContent);

			return RedirectToPage("./StudentsTable");
		}
	}
}