using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.Pages.Teacher.Courses;

namespace PrivateOfficeWebApp.Pages
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
        public class ResponseLoginTeacher
        {
	        public string access_token { get; set; }
            public string username { get; set; }
            public int idTeacher { get; set; }
            public string Role { get; set; }
        }

        [BindProperty]
        public IndexCourseModel.RequestTeacher Teacher { get; set; }

        public async Task<IActionResult> OnPostLoginTeacher(string login, string password)
        {
	        HttpResponseMessage response = await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Teachers/token?username=" + login+"&password=" + password, null);
            var responseStr = await response.Content.ReadAsStringAsync();
            try
            {
	            var responseLogin = JsonConvert.DeserializeObject<ResponseLoginTeacher>(responseStr);
	            Response.Cookies.Append("token_auth", responseLogin.access_token); 
	            Response.Cookies.Append("login", responseLogin.username);
                Response.Cookies.Append("idTeacher", responseLogin.idTeacher.ToString());
                Response.Cookies.Append("role", responseLogin.Role);
                return Redirect(AppSettings.WebAppUrl + "/Teacher/Courses/IndexCourse?id=" +
		                            responseLogin.idTeacher);
            }
            catch
            {
	            return NotFound();
            }
            
        }
        public class ResponseLoginStudent
        {
	        public string access_token { get; set; }
	        public string username { get; set; }
	        public int idStudent { get; set; }
        }
        public async Task<IActionResult> OnPostLoginStudent(string login, string password)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Students/StudentToken?username="+login+"&password="+password, null);
            var responseStr = await response.Content.ReadAsStringAsync();
            try
            {
                var jsonResponse = JsonConvert.DeserializeObject<ResponseLoginStudent>(responseStr);
                Response.Cookies.Append("token_auth", jsonResponse.access_token);
	            Response.Cookies.Append("login", jsonResponse.username);
	            Response.Cookies.Append("idStudent", jsonResponse.idStudent.ToString());
	            return Redirect(AppSettings.WebAppUrl + "/Student/StudentCourses/IndexStudentCourse?idStudent=" + 
	                            jsonResponse.idStudent);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostRegistryTeacher()
        {
	        var jsonRequest = JsonConvert.SerializeObject(Teacher);
	        HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
	        await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Teachers", httpContent);
	        return Page();
        }
    }
}