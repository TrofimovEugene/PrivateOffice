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
    public class EditModel : PageModel
    {
	    private readonly HttpClient _httpClient;
	    public EditModel()
	    {

		    var clientHandler = new HttpClientHandler
		    {
			    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
		    };
		    _httpClient = new HttpClient(clientHandler);
	    }

	    [BindProperty]
	    public Course Course { get; set; }
	    [BindProperty]
		public List<Group> Groups { get; set; }
		public async Task<IActionResult> OnGet(int? id)
        {
	        if (id == null)
		        return NotFound();

	        HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses/" + id);
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        Course = JsonConvert.DeserializeObject<Course>(jsonResponse);

	        response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/" + Course.IdGroup);
	        jsonResponse = await response.Content.ReadAsStringAsync();
	        var group = JsonConvert.DeserializeObject<Group>(jsonResponse);
	        Course.Group = group;

	        response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups");
	        jsonResponse = await response.Content.ReadAsStringAsync();
	        Groups = JsonConvert.DeserializeObject<List<Group>>(jsonResponse);

	        if (Course == null)
		        return NotFound();
	        return Page();
        }

		public async Task<IActionResult> OnPostAsync(int idgroup)
		{
			var reqCourse = new RequestCourse
			{
				IdCourse = Course.IdCourse,
				NameCourse = Course.NameCourse,
				NameUniversity = Course.NameUniversity,
				StartDate = Course.StartDate,
				EndDate = Course.EndDate,
				CountTime = Course.CountTime,
				IdGroup = idgroup,
				IdTeacher = 1
			};
			var jsonRequest = JsonConvert.SerializeObject(reqCourse);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/Courses/" + Course.IdCourse, httpContent);
			return RedirectToPage("./IndexCourse");
		}
		[JsonObject]
		public class RequestCourse
		{
			[JsonProperty("idCourse")]
			public int IdCourse { get; set; }
			[JsonProperty("nameCourse")]
			public string NameCourse { get; set; }
			[JsonProperty("idTeacher")]
			public int IdTeacher { get; set; }
			[JsonProperty("startDate")]
			public DateTime StartDate { get; set; }
			[JsonProperty("endDate")]
			public DateTime EndDate { get; set; }
			[JsonProperty("nameUniversity")]
			public string NameUniversity { get; set; }
			[JsonProperty("countTime")]
			public int CountTime { get; set; }
			[JsonProperty("idGroup")]
			public int? IdGroup { get; set; }
		}

    }
}