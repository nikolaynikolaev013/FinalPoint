namespace FinalPoint.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Common;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.Business.Interfaces;
    using FinalPoint.Web.ViewModels;
    using FinalPoint.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IUserService userService;
        private readonly IOfficeService officeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IThemeService themeService;

        public HomeController(
            IUserService userService,
            IOfficeService officeService,
            UserManager<ApplicationUser> userManager,
            IThemeService themeService)
        {
            this.userService = userService;
            this.officeService = officeService;
            this.userManager = userManager;
            this.themeService = themeService;
        }

        public IActionResult Index()
        {
            var model = this.LoadIndexViewModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeIndexInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.userService
                    .SetUserNewWorkOfficeByUserIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier), input.OfficeId);

                if (result)
                {
                    this.themeService.UpdateThemeInHttpContext();
                }
            }

            input = this.LoadIndexViewModel();
            return this.View(input);
        }

        [AllowAnonymous]
        public IActionResult Assets()
        {
            var model = new LoginUsersAndOfficesShowViewModel();
            model.Users = this.userService.GetAllUsers();
            model.Offices = this.officeService.GetAllOfficesAsStringWithoutVirtual();
            return this.View(model);
        }

        private HomeIndexInputModel LoadIndexViewModel()
        {
            var model = new HomeIndexInputModel();
            var user = this.userService.GetUserById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var isOwner = this.User.IsInRole(GlobalConstants.OwnerRoleName);
            var isAdministrator = this.User.IsInRole(GlobalConstants.AdministratorRoleName) || this.User.IsInRole(GlobalConstants.OfficeOwnerRoleName);
            var isFromVirtualOffice = user.WorkOfficeId == this.officeService.GetVirtualOffice().Id;

            model.ShowOfficeModules = !isOwner || !isFromVirtualOffice;
            model.ShowAdministratorModule = isOwner || isAdministrator;
            model.IsOwner = isOwner;
            model.FullName = this.userManager.GetUserAsync(this.User)?.Result?.FullName;
            model.CurrentWorkOfficeId = user.WorkOfficeId;
            model.AvailableOffices = this.officeService.GetAllOfficesAndSortingCentersAsKeyValuePairs().OrderByDescending(x => x.Key == user.WorkOfficeId.ToString());

            return model;
        }
    }
}
