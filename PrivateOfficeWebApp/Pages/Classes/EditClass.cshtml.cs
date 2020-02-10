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

        [BindProperty]
        public List<Group> Groups { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/" + id);
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        Class = JsonConvert.DeserializeObject<Classes>(jsonResponse);

            return Page();
            }

        public async Task<IActionResult> OnPostEditClass(int TypeClass, int idgroup, int idCourse)
        {

			var reqClasses = new Classes
            {
                IdClasses = Class.IdClasses,
                IdTypeClasses = TypeClass,
                NameClasses = Class.NameClasses,
                IdGroup = idgroup,
                IdCourse = idCourse,
                StartTime = Class.StartTime,
                EndTime = Class.EndTime,
                DaysWeek = Class.DaysWeek,
                ReplayClasses = Class.ReplayClasses
            };

            var jsonRequest = JsonConvert.SerializeObject(reqClasses);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("https://localhost:44316/api/Classes/" + Class.IdClasses, httpContent);

            return Redirect("https://localhost:44326/Classes/ViewClasses?id=" + idCourse);
		}
    }
}