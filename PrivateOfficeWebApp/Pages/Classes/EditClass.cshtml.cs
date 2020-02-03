using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp
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
		public Classes Class { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
	        HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44316/api/Classes/"+ id);
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        Class = JsonConvert.DeserializeObject<Classes>(jsonResponse);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
	        return RedirectToPage("https://localhost:44326/Classes/EditClass?id=" + Class.IdCourse);
        }
    }
}