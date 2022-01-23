using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Pages.Dashboard
{
    public class Index:PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            
            return Page();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            return Page();
        }
    }
}
