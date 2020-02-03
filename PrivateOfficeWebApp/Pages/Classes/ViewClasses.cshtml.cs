using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp.Pages.Courses
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
		public List<Models.Classes> Classes { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
	        if (id == null)
				return NotFound();

	        HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Courses/" + id);
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        Course = JsonConvert.DeserializeObject<Course>(jsonResponse);

	        response = await _httpClient.GetAsync("https://localhost:44316/api/Classes/id=" + id);
	        jsonResponse = await response.Content.ReadAsStringAsync();
	        Classes = JsonConvert.DeserializeObject<List<Models.Classes>>(jsonResponse);

	        if (Course == null)
		        return NotFound();
	        return Page();
        }
        [BindProperty]
		public RequestClasses Class { get; set; }
		public async Task<IActionResult> OnPostAsync()
		{
			var jsonRequest = JsonConvert.SerializeObject(Class);
	        HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
	        await _httpClient.PostAsync("https://localhost:44316/api/Classes", httpContent);
	        return Redirect("https://localhost:44326/Courses/Edit?id=" + Class.IdCourse);
        }

		[JsonObject]
		public class RequestClasses
		{
			[JsonProperty("idTypeClasses")]
			public int IdTypeClasses { get; set; }
			[JsonProperty("idCourse")]
			public int IdCourse { get; set; }
			[JsonProperty("nameClasses")]
			public string NameClasses { get; set; }
			[JsonProperty("startTime")]
			public TimeSpan StartTime { get; set; }
			[JsonProperty("endTime")]
			public TimeSpan EndTime { get; set; }
			[JsonProperty("daysWeek")]
			public string DaysWeek { get; set; }
			[JsonProperty("replayClasses")]
			public string ReplayClasses { get; set; }
		}
	}
}