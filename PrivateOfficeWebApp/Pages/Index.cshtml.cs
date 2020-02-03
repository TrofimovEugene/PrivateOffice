using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrivateOfficeWebApp
{
    public class AuthorizationModel : PageModel
    {
        public async Task<IActionResult> OnPostLoginTeacher()
        {
            return RedirectToPage("./Courses/IndexCourse");
        }
        public async Task<IActionResult> OnPostLoginStudent()
        {
	        return RedirectToPage("./Courses/IndexCourse");
        }
    }
}