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
    public class IndexModel : PageModel
    {
	    private readonly HttpClient _httpClient;
        public IndexModel()
	    {
		    var clientHandler = new HttpClientHandler
		    {
			    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
		    };
		    _httpClient = new HttpClient(clientHandler);
        }
        public class RequestLogin
        {
	        public string login { get; set; }
	        public string password { get; set; }
        }
        public async Task<IActionResult> OnPostLoginTeacher(string login, string password)
        {
	        RequestLogin requestLogin = new RequestLogin {login = login, password = password};
	        var jsonRequest = JsonConvert.SerializeObject(requestLogin);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:44316/api/Teachers/GetTeacherLogin", httpContent);
            var responseStr = await response.Content.ReadAsStringAsync();
            try
            {
	            var jsonResponse = JsonConvert.DeserializeObject<Teacher>(responseStr);
                if (jsonResponse.Login == login && jsonResponse.Password == password)
					return RedirectToPage("./Courses/IndexCourse");
                else
                {
	                return NotFound();
                }
            }
            catch
            {
	            return NotFound();
            }
            
        }
        public async Task<IActionResult> OnPostLoginStudent()
        {

	        return RedirectToPage("./Courses/IndexCourse");
        }
    }
}