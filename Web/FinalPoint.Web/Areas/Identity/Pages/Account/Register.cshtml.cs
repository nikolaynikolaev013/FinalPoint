namespace FinalPoint.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using FinalPoint.Common;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.CustomAttributes;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.OwnerRoleName + ", " + GlobalConstants.OfficeOwnerRoleName)]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [CustomRequired]
            [EmailAddress]
            [Display(Name = "Email", Prompt = "Въведете имейла на служителя")]
            public string Email { get; set; }

            [Display(Name = "Дата на раждане")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

            [CustomRequired]
            [Display(Name = "Име:", Prompt = "Въведете малкото име на служителя")]
            public string FirstName { get; set; }

            [CustomRequired]
            [Display(Name = "Презиме:", Prompt = "Въведете презимето на служителя")]
            public string MiddleName { get; set; }

            [CustomRequired]
            [Display(Name = "Фамилно име:", Prompt = "Въведете фамилията на служителя")]
            public string LastName { get; set; }

            [PersonalId]
            [Display(Name = "Личен код:", Prompt = "Въведете личния код на служителя")]
            public int PersonalId { get; set; }

            [CustomRequired]
            [Display(Name = "Офис/РЦ в който работи")]
            public int OfficeId { get; set; }

            [CustomRequired]
            [Display(Name = "Длъжност")]
            public string Role { get; set; }

            [CustomRequired]
            [StringLength(100, ErrorMessage = "Паролата трябва да е с дължина между {2} и {1} символа.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърдете паролата")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат")]
            public string ConfirmPassword { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.PersonalId.ToString(), Email = Input.Email, FirstName = Input.FirstName, MiddleName = Input.MiddleName, LastName = Input.LastName, DateOfBirth = Input.DateOfBirth, PersonalId = Input.PersonalId, WorkOfficeId = Input.OfficeId };

                var result = await _userManager.CreateAsync(user, this.Input.Password);

                var roleResult = await this._userManager.AddToRoleAsync(user, this.Input.Role);

                if (result.Succeeded && roleResult.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
