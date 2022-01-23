using LoginRazorBlog.ServiceApplication;
using LoginRazorBlog.ServiceApplication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Pages.Login
{
    public class SignInModel:PageModel
    {
        private readonly ILocalAccountService _localAccountService;
        private readonly ILogger<SignInModel> _logger;
        private IMediator _mediator;

        public SignInModel(ILocalAccountService localAccountService, ILogger<SignInModel> logger, IMediator mediator)
        {
            _localAccountService=localAccountService;
            _logger=logger;
            _mediator=mediator;
        }

        [BindProperty]
        [Required(ErrorMessage = "Please enter a username.")]
        [Display(Name = "Username")]
        [MinLength(2, ErrorMessage = "Username must be at least 2 characters"), MaxLength(32)]
        [RegularExpression("[a-z0-9]+", ErrorMessage = "Username must be lower case letters or numbers.")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter a password.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters"), MaxLength(32)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9._~!@#$^&*]{8,}$", ErrorMessage = "Password must be minimum eight characters, at least one letter and one number")]
        public string Password { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
          //  await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Page();
        }

        /// <summary>
        /// koding dibawah pakai service pattern
        /// </summary>
        /// <returns></returns>



        //public async Task<IActionResult> OnPostAsync()
        //{
        //    try
        //    {
        //       if (ModelState.IsValid)
        //        {
        //            var uid = await _localAccountService.ValidateAsync(Username, Password);
        //            if (uid != Guid.Empty)
        //            {
        //                var claims = new List<Claim>
        //                {
        //                    new (ClaimTypes.Name, Username),
        //                    new (ClaimTypes.Role, "Administrator"),
        //                    new ("uid", uid.ToString())
        //                };
        //                var ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //                var p = new ClaimsPrincipal(ci);

        //                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, p);
        //                await _localAccountService.LogSuccessLoginAsync(uid,
        //                    HttpContext.Connection.RemoteIpAddress?.ToString());

        //                var successMessage = $@"Authentication success for local account ""{Username}""";

        //                _logger.LogInformation(successMessage);

        //                return RedirectToPage("/Dashboard/Index");
        //            }
        //            ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
        //            return Page();
        //        }

        //        var failMessage = $@"Authentication failed for local account ""{Username}""";

        //        _logger.LogWarning(failMessage);

        //        Response.StatusCode = StatusCodes.Status400BadRequest;
        //        ModelState.AddModelError(string.Empty, "Bad Request.");
        //        return Page();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogWarning($@"Authentication failed for local account ""{Username}""");

        //        ModelState.AddModelError(string.Empty, e.Message);
        //        return Page();
        //    }
        //}






        //kode dibawah ini mencoba untuk menggunakan custom error handler. tp mengapa isi valuenya selalu kosong ???

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var uid = await _mediator.Send(new ValidateLoginCommand(Username, Password));
        //        if (uid != Guid.Empty)
        //        {
        //            var claims = new List<Claim>
        //            {
        //                new (ClaimTypes.Name, Username),
        //                new (ClaimTypes.Role, "Administrator"),
        //                new ("uid", uid.ToString())
        //            };
        //            var ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var p = new ClaimsPrincipal(ci);

        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, p);
        //            await _mediator.Send(new LogSuccessLoginCommand(uid, HttpContext.Connection.RemoteIpAddress?.ToString()));

        //            var successMessage = $@"Authentication success for local account ""{Username}""";

        //            _logger.LogInformation(successMessage);

        //            return RedirectToPage("/Admin/Post");
        //        }
        //        ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
        //        return Page();
        //    }

        //    return Page();
        //}



        //kode dibawah menggunakan mediator pattern
        // menggunakan try catch pattern utk menangkap error yg terbit. kemudian dilimpahkan ke modelstate error

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var uid = await _mediator.Send(new ValidateLoginCommand(Username, Password));
                    if (uid != Guid.Empty)
                    {
                        var claims = new List<Claim>
                    {
                        new (ClaimTypes.Name, Username),
                        new (ClaimTypes.Role, "Administrator"),
                        new ("uid", uid.ToString())
                    };
                        var ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var p = new ClaimsPrincipal(ci);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, p);
                        await _mediator.Send(new LogSuccessLoginCommand(uid, HttpContext.Connection.RemoteIpAddress?.ToString()));

                        var successMessage = $@"Authentication success for local account ""{Username}""";

                        _logger.LogInformation(successMessage);

                        return RedirectToPage("/Admin/Post");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                    return Page();
                }


                var failMessage = $@"Authentication failed for local account ""{Username}""";

                _logger.LogWarning(failMessage);

                Response.StatusCode = StatusCodes.Status400BadRequest;
                ModelState.AddModelError(string.Empty, "Bad Request.");
                return Page();
            }
            catch (System.Exception e)
            {
                _logger.LogWarning($@"Authentication failed for local account ""{Username}""");

                ModelState.AddModelError(string.Empty, e.Message);
                return Page();
            }
        }


    }
}
