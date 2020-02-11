using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.Models;

namespace PrivateOfficeWebApp
{
    public class ViewGroupsModel : PageModel
    {
	    private readonly HttpClient _httpClient;
	    public ViewGroupsModel()
	    {
		    var clientHandler = new HttpClientHandler
		    {
			    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
		    };
		    _httpClient = new HttpClient(clientHandler);
		}
	    [BindProperty]
        public List<Group> Groups { get; set; }
        [BindProperty]
        public Group Group { get; set; }
		public async Task<IActionResult> OnGet()
        {
	        HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/");
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        Groups = JsonConvert.DeserializeObject<List<Group>>(jsonResponse);
	        return Page();
        }

        public async Task<IActionResult> OnPostCreateGroups(int idgroup)
        {
            Group.IdGroup = idgroup;
            var jsonRequest = JsonConvert.SerializeObject(Group);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Groups", httpContent);
            //return Redirect(jsonRequest);
             return Redirect("https://localhost:44326/StudentsTable/ViewGroups");
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/Groups/" + id);
            return RedirectToPage("./ViewGroups");
        }
	}
}