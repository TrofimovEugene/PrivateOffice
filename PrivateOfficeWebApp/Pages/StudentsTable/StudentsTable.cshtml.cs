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
		public List<Student> Students { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
	        HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Students");
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        Students = JsonConvert.DeserializeObject<List<Student>>(jsonResponse);

	        foreach (var student in Students)
	        {
				response = await _httpClient.GetAsync("https://localhost:44316/api/Groups/" + student.IdGroup);
				jsonResponse = await response.Content.ReadAsStringAsync();
				var group = JsonConvert.DeserializeObject<Group>(jsonResponse);
				student.Group = group;
	        }
	        
			response = await _httpClient.GetAsync("https://localhost:44316/api/Groups/");
	        jsonResponse = await response.Content.ReadAsStringAsync();
	        Groups = JsonConvert.DeserializeObject<List<Group>>(jsonResponse);

			return Page();
        }
		[BindProperty]
		public ResponseStudent Student { get; set; }
		[BindProperty]
		public List<Group> Groups { get; set; }
        public async Task<IActionResult> OnPost(int idgroup)
        {
	        Student.IdGroup = idgroup;
	        var jsonRequest = JsonConvert.SerializeObject(Student);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PostAsync("https://localhost:44316/api/Students", httpContent);

			return RedirectToPage("./StudentsTable");
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
        }
    }
}