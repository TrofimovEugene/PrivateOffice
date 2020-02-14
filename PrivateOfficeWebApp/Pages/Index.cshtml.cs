using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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
        public async Task<IActionResult> OnPostLoginTeacher(string login, string password)
        {
	        RequestLogin requestLogin = new RequestLogin {login = login, password = password};
	        var jsonRequest = JsonConvert.SerializeObject(requestLogin);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Teachers/GetTeacherLogin", httpContent);
            var responseStr = await response.Content.ReadAsStringAsync();
            try
            {
	            var jsonResponse = JsonConvert.DeserializeObject<PagesModels.Teacher>(responseStr);
	            if (jsonResponse.Login == login && jsonResponse.Password == password)
	            {
		            Response.Cookies.Append("login", jsonResponse.Login);
                    Response.Cookies.Append("idTeacher", jsonResponse.IdTeacher.ToString());
                    return Redirect("https://localhost:44326/Teacher/Courses/IndexCourse?idTeacher=" + jsonResponse.IdTeacher);
	            }
	            return Redirect("https://localhost:44326"); 
            }
            catch
            {
	            return NotFound();
            }
            
        }
        [BindProperty]
        public RequestTeacher Teacher { get; set; }
        public async Task<IActionResult> OnPostRegistryTeacher()
        {
	        var jsonRequest = JsonConvert.SerializeObject(Teacher);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Teachers", httpContent);
	        return Page();
        }
        [JsonObject]
        public class RequestTeacher
        {
	        [JsonProperty("login")]
	        public string Login { get; set; }
	        [JsonProperty("password")]
	        public string Password { get; set; }
	        [JsonProperty("firstName")]
	        public string FirstName { get; set; }
	        [JsonProperty("secondName")]
	        public string SecondName { get; set; }
	        [JsonProperty("patronymic")]
	        public string Patronymic { get; set; }
	        //[JsonProperty("course")]
	        //public virtual List<Course> Course { get; set; }

        }
        public async Task<IActionResult> OnPostLoginStudent()
        {

            return Redirect("https://localhost:44326/Student/StudentCourses/IndexStudentCourse");
        }
    }
}