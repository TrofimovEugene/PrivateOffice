﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.PagesModels;

namespace PrivateOfficeWebApp.Pages.Student.StudentCourses
{
    public class Task : PageModel
    {
	    private readonly HttpClient _httpClient;
	    public Task()
	    {

		    var clientHandler = new HttpClientHandler
		    {
			    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
		    };
		    _httpClient = new HttpClient(clientHandler);
		}

	    [BindProperty]
	    public Course Course { get; set; }
		[BindProperty]
		public Report Reports { get; set; }
		[BindProperty]
		public List<Classes> Classes { get; set; }
		[BindProperty]
		public List<Group> Groups { get; set; }
		[BindProperty]
		public IndexStudentModel.Students Student { get; set; }
		public async Task<IActionResult> OnGet(int? id)
        {
	        if (id == null)
		        return NotFound();

	        if (Request.Cookies["token_auth"] != null)
		        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

			HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Courses/" + id);
	        var jsonResponse = await response.Content.ReadAsStringAsync();
	        Course = JsonConvert.DeserializeObject<Course>(jsonResponse);


			if (Course == null)
		        return NotFound();

			response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/" + Course.IdGroup);
	        jsonResponse = await response.Content.ReadAsStringAsync();
	        var group = JsonConvert.DeserializeObject<Group>(jsonResponse);
	        Course.Group = group;

	        response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups");
	        jsonResponse = await response.Content.ReadAsStringAsync();
	        Groups = JsonConvert.DeserializeObject<List<Group>>(jsonResponse);

            response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/GetCountStudentInGroup/id=" + group.IdGroup);
            jsonResponse = await response.Content.ReadAsStringAsync();
            var countStudents = JsonConvert.DeserializeObject<int>(jsonResponse);
            group.CountStudents = countStudents;

            return Page();
        }

		public async Task<IActionResult> OnPostAsync(int idgroup)
		{
			if (Request.Cookies["token_auth"] != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["token_auth"]);

			var reqCourse = new RequestCourse
			{
				IdCourse = Course.IdCourse,
				NameCourse = Course.NameCourse,
				NameUniversity = Course.NameUniversity,
				StartDate = Course.StartDate,
				EndDate = Course.EndDate,
				CountTime = Course.CountTime,
				IdGroup = idgroup,
				IdTeacher = Course.IdTeacher
            };
			var jsonRequest = JsonConvert.SerializeObject(reqCourse);
			HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/Courses/" + Course.IdCourse, httpContent);
			//return Redirect(jsonRequest);
			return Redirect(AppSettings.WebAppUrl + "/Teacher/Courses/Edit?id=" + Course.IdCourse);
		}
		[JsonObject]
		public class RequestCourse
		{
			[JsonProperty("idCourse")]
			public int IdCourse { get; set; }
			[JsonProperty("nameCourse")]
			public string NameCourse { get; set; }
			[JsonProperty("idTeacher")]
			public int IdTeacher { get; set; }
			[JsonProperty("startDate")]
			public DateTime StartDate { get; set; }
			[JsonProperty("endDate")]
			public DateTime EndDate { get; set; }
			[JsonProperty("nameUniversity")]
			public string NameUniversity { get; set; }
			[JsonProperty("countTime")]
			public int CountTime { get; set; }
			[JsonProperty("idGroup")]
			public int? IdGroup { get; set; }
		}

		public async Task<IActionResult> OnPostLogOut()
		{
			Response.Cookies.Delete("token_auth");
			Response.Cookies.Delete("login");
			Response.Cookies.Delete("idTeacher");
			return Redirect(AppSettings.WebAppUrl + "/Index");
		}
	}
}