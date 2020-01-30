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
		
		public IList<Course> Courses { get; set; }
		
		public async Task OnGetAsync()
		{
			HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44328/api/Courses");
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Courses = JsonConvert.DeserializeObject<IList<Course>>(jsonResponse);
		}
		[BindProperty]
		public Course Course { get; set; }
		public async Task<IActionResult> OnPostAsync()
		{
			Course.IdCourse = null;
			Course.IdTeacher = 1;
			var jsonRequest = JsonConvert.SerializeObject(Course);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PostAsync("https://localhost:44328/api/Courses", httpContent);
			return Page();
		}
	}
}
