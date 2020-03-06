﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.PagesModels;

namespace PrivateOfficeWebApp.Pages.Teacher.StudentsTable
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
	        if (Request.Cookies["token_auth"] != null)
		        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/");
	         var jsonResponse = await response.Content.ReadAsStringAsync();
	        Groups = JsonConvert.DeserializeObject<List<Group>>(jsonResponse);

            foreach (var groups in Groups)
            {
                response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/GetCountStudentInGroup/id=" + groups.IdGroup);
                jsonResponse = await response.Content.ReadAsStringAsync();
                var countStudents = JsonConvert.DeserializeObject<int>(jsonResponse);
                groups.CountStudents = countStudents;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostCreateGroups(int idgroup)
        {
	        if (Request.Cookies["token_auth"] != null)
		        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            Group.IdGroup = idgroup;
            var jsonRequest = JsonConvert.SerializeObject(Group);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(AppSettings.DataBaseUrl + "/api/Groups", httpContent);
            //return Redirect(jsonRequest);
             return Redirect("./ViewGroups");
        }

        [BindProperty]
        public List<Homework> Homework { get; set; }
        [BindProperty]
        public List<HomeworkGroup> HomeworkGroups { get; set; }
        public async Task<IActionResult> OnPostDelete(int id)
        {
	        if (Request.Cookies["token_auth"] != null)
		        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

            HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Homework/GetHomeworkFromGroup/id=" + id);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Homework = JsonConvert.DeserializeObject<List<Homework>>(jsonResponse);
            if (Homework != null)
            {
                foreach (var homeworks in Homework)
                {
                    await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/Homework/" + homeworks.IdHomework);
                }
            }

             response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/HomeworkGroups/GetHomeworkGroupFromGroup/id=" + id);
             jsonResponse = await response.Content.ReadAsStringAsync();
            HomeworkGroups = JsonConvert.DeserializeObject<List<HomeworkGroup>>(jsonResponse);
            if(HomeworkGroups != null) { 
                foreach (var homeworksGroup in HomeworkGroups)
                {
                    await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/Homework/" + homeworksGroup.IdHomeworkGroup);
                }
            }

            await _httpClient.DeleteAsync(AppSettings.DataBaseUrl + "/api/Groups/" + id);
            return RedirectToPage("./ViewGroups");
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