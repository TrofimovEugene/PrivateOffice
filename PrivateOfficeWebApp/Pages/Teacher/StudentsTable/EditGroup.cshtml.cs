﻿using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrivateOfficeWebApp.PagesModels;

namespace PrivateOfficeWebApp.Pages.Teacher.StudentsTable
{
    public class EditGroupModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public EditGroupModel()
        {
	        var clientHandler = new HttpClientHandler
	        {
		        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
	        };
	        _httpClient = new HttpClient(clientHandler);
        }
   
        [BindProperty]
        public Group Group { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
                return NotFound();
            HttpResponseMessage response = await _httpClient.GetAsync(AppSettings.DataBaseUrl + "/api/Groups/" + id);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Group = JsonConvert.DeserializeObject<Group>(jsonResponse);
                return Page();
        }

        public async Task<IActionResult> OnPostEditGroups()
        {
            var reqGroup = new Group
            {
                IdGroup = Group.IdGroup,
                NameGroup = Group.NameGroup
            };
            var jsonRequest = JsonConvert.SerializeObject(reqGroup);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(AppSettings.DataBaseUrl + "/api/Groups/" + Group.IdGroup, httpContent);
            return Redirect( "./ViewGroups");
        }
    }
}