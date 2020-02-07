using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace PrivateOfficeWebApp.Pages.Teacher.Classes
{
    public class EditClassModel : PageModel
    {
	    private readonly HttpClient _httpClient;

	    public EditClassModel()
	    {
		    var clientHandler = new HttpClientHandler
		    {
			    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
		    };
		    _httpClient = new HttpClient(clientHandler);
		}
		[BindProperty]
		public Models.Classes Class { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
	        HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/" + id);
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        Class = JsonConvert.DeserializeObject<Models.Classes>(jsonResponse);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
	        return RedirectToPage("https://localhost:44326/Teacher/Classes/EditClass?id=" + Class.IdCourse);
        }
    }
}