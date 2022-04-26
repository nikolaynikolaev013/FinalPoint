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
            var model = this.PopulateIndexViewModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeIndexInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.userService.ChangeUserWorkOffice(this.User.FindFirstValue(ClaimTypes.NameIdentifier), input.OfficeId);

                if (result)
                {
                    this.themeService.UpdateTheme();
                }
            }

            input = this.PopulateIndexViewModel();
            return this.View(input);
        }

        [AllowAnonymous]
        public IActionResult Assets()
        {
            var model = new LoginUsersAndOfficesShowViewModel();
            model.Users = this.userService.GetAllUsers();
            model.Offices = this.officeService.GetAllOfficesWithoutVirtual();
            return this.View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        private HomeIndexInputModel PopulateIndexViewModel()
        {
            var model = new HomeIndexInputModel();
            var user = this.userService.GetUserById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            model.IsOwner = this.User.IsInRole(GlobalConstants.OwnerRoleName);
            model.IsAdministrator = this.User.IsInRole(GlobalConstants.AdministratorRoleName) || this.User.IsInRole(GlobalConstants.OfficeOwnerRoleName);
            model.FullName = this.userManager.GetUserAsync(this.User)?.Result?.FullName;
            model.CurrentWorkOfficeId = user.WorkOfficeId;
            model.IsFromVirtualOffice = user.WorkOfficeId == this.officeService.GetVirtualOffice().Id;
            model.AvailableOffices = this.officeService.GeAllOfficesAndSortingCentersAsKeyValuePairs().OrderByDescending(x => x.Key == user.WorkOfficeId.ToString());

            return model;
        }
    }
}
