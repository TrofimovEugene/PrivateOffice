using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.PagesModels;

namespace PrivateOfficeWebApp.Pages.Teacher.Courses
{
    public class ViewCourseModel : PageModel
    {
		private readonly HttpClient _httpClient;
		public ViewCourseModel()
		{

			var clientHandler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
			};
			_httpClient = new HttpClient(clientHandler);
		}

		[BindProperty]
		public Course Course { get; set; }
		public async Task<IActionResult> OnGet(int? id)
		{
			if (id == null)
				return NotFound();

			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses/" + id);
			var jsonResponse = await response.Content.ReadAsStringAsync();
			Course = JsonConvert.DeserializeObject<Course>(jsonResponse);

			if (Course == null)
				return NotFound();
			return Page();
		}
	}
}