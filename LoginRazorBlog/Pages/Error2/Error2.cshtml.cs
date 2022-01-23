using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Pages.Error2
{
  
    [IgnoreAntiforgeryToken]
    public class Error2 : PageModel
    {
        [BindProperty]
        public int statusCode { get; set; }
        [BindProperty]
        public String? Message { get; set; }
        [BindProperty]
        public String? stackTrace { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //  await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();

            ViewData["statusCode"] = HttpContext.Response.StatusCode;

            ViewData["message"] = exception.Error.Message;

            ViewData["stackTrace"] = exception.Error.StackTrace;

            statusCode = HttpContext.Response.StatusCode;
            Message= exception.Error.Message; ;
            stackTrace = exception.Error.StackTrace;

            return Page();
        }




    }
}
