using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.PagesModels;

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
		public PagesModels.Classes Class { get; set; }

        [BindProperty]
        public PagesModels.PlanClasses PlanClasses { get; set; }
        [BindProperty]
        public PagesModels.Course Course { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            if (Request.Cookies["token_auth"] != null)
	            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Classes/" + id);
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        Class = JsonConvert.DeserializeObject<PagesModels.Classes>(jsonResponse);

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/HomeworkGroups/GetHomeworkGroupForClasses/id=" + id);
            jsonResponse = await response.Content.ReadAsStringAsync();
            HomeworkGroup = JsonConvert.DeserializeObject<HomeworkGroup>(jsonResponse);

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses/" + Class.IdCourse);
             jsonResponse = await response.Content.ReadAsStringAsync();
            Course = JsonConvert.DeserializeObject<PagesModels.Course>(jsonResponse);

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/PlanClasses/GetPlanClassesFromClasses/id=" + id);
            jsonResponse = await response.Content.ReadAsStringAsync();
            PlanClasses = JsonConvert.DeserializeObject<PagesModels.PlanClasses>(jsonResponse);

            return Page();
            }

        public async Task<IActionResult> OnPostEditClass(int TypeClass, int idgroup, int idCourse, int idHomework)
        {
	        if (Request.Cookies["token_auth"] != null)
		        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            var reqClasses = new Models.Classes
            {
                IdClasses = Class.IdClasses,
                IdTypeClasses = TypeClass,
                NameClasses = Class.NameClasses,
                DateClasses = Class.DateClasses,
                IdGroup = idgroup,
                IdCourse = idCourse,
                StartTime = Class.StartTime,
                EndTime = Class.EndTime,
                Cabinet = Class.Cabinet,
                DaysWeek = Class.DaysWeek,
                ReplayClasses = Class.ReplayClasses,
            };

            HomeworkGroup.IdHomeworkGroup = idHomework;

            var jsonRequest = JsonConvert.SerializeObject(reqClasses);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/Classes/" + Class.IdClasses, httpContent);

            jsonRequest = JsonConvert.SerializeObject(HomeworkGroup);
            httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/HomeworkGroups/" + idHomework, httpContent);

            return Redirect(AppSettings.WebAppUrl + "/Teacher/Classes/ViewClasses?id=" + idCourse);
		}

        [BindProperty]
        public HomeworkGroup HomeworkGroup { get; set; }
        public async Task<IActionResult> OnPostCreateTask(int idGroup, int Idclasses)
        {
            if (Request.Cookies["token_auth"] != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            HomeworkGroup.IdGroup = idGroup;
            HomeworkGroup.IdClasses = Idclasses;

            var jsonRequest = JsonConvert.SerializeObject(HomeworkGroup);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/HomeworkGroups", httpContent);
            //return Redirect(jsonRequest);
            return Redirect(AppSettings.WebAppUrl + "/Teacher/Classes/EditClass?id=" + Idclasses);
        }

        public async Task<IActionResult> OnPostCreatePlanClasses(int idClasses)
        {
            if (Request.Cookies["token_auth"] != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            PlanClasses.IdClasses = idClasses;

            var jsonRequest = JsonConvert.SerializeObject(PlanClasses);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/PlanClasses", httpContent);
            //return Redirect(jsonRequest);
            return Redirect(AppSettings.WebAppUrl + "/Teacher/Classes/EditClass?id=" + idClasses);
        }


        public async Task<IActionResult> OnPostEditPlanClasses(int idClasses, int id)
        {
            if (Request.Cookies["token_auth"] != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);
            
            var googleSlides = new GoogleSlides();
            googleSlides.Slides();

            PlanClasses.IdClasses = idClasses;
            PlanClasses.IdPlanClasses = id;

            var jsonRequest = JsonConvert.SerializeObject(PlanClasses);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/PlanClasses/" + id, httpContent);
            //return Redirect(jsonRequest);
          return Redirect(AppSettings.WebAppUrl + "/Teacher/Classes/EditClass?id=" + idClasses);
        }

        public async Task<IActionResult> OnPostDelete(int id, int idClasses)
        {
            if (Request.Cookies["token_auth"] != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);
            await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/PlanClasses/" + id);
            return Redirect(AppSettings.WebAppUrl + "/Teacher/Classes/EditClass?id=" + idClasses);

        }

        public async Task<IActionResult> OnPostLogOut()
        {
	        Response.Cookies.Delete("token_auth");
	        Response.Cookies.Delete("login");
	        Response.Cookies.Delete("idTeacher");
	        Response.Cookies.Delete("role");
            return Redirect(AppSettings.WebAppUrl + "/Index");
        }
    }
}