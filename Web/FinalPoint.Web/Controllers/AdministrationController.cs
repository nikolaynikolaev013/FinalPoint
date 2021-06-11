// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Common;
    using FinalPoint.Data.Models;
    using FinalPoint.Services.Data;
    using FinalPoint.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.OwnerRoleName)]
    public class AdministrationController : Controller
    {
        private readonly IOfficeService officeService;
        private readonly ICityService cityService;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministrationController(
            IOfficeService officeService,
            ICityService cityService,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.officeService = officeService;
            this.cityService = cityService;
            this.userService = userService;
            this.userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult FireEmployee()
        {
            FireEmployeeInputModel model = new FireEmployeeInputModel();

            this.FillUpAvailableEmployeesToDelete(model);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FireEmployee(FireEmployeeInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var removedUser = await this.userService.RemoveUser(input.EmployeeToFire);
                if (removedUser != null)
                {
                    input.ResultMessage = $"Служител - {removedUser.FirstName} {removedUser.MiddleName} {removedUser.LastName} - {removedUser.PersonalId} - беше уволнен успешно.";
                }
            }

            this.FillUpAvailableEmployeesToDelete(input);

            return this.View(input);
        }

        public IActionResult AddOffice()
        {
            AddOfficeInputModel model = new AddOfficeInputModel();
            this.LoadAddOfficeData(model);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOffice(AddOfficeInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.officeService.CreateAsync(input);

                this.ModelState.Clear();
                input.ResultMessage = $"Офис {input.Name} беше регистриран успешно!";
                input.PostCode = 0;
                input.Name = string.Empty;
                input.Address = string.Empty;

                this.LoadAddOfficeData(input);
                return this.View(input);
            }

            this.LoadAddOfficeData(input);
            return this.View(input);
        }

        public IActionResult RemoveOffice()
        {
            RemoveOfficeInputModel model = new RemoveOfficeInputModel();
            model.AvailableOfficesToRemove = this.officeService.GeAllOfficesAndSortingCentersAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveOffice(RemoveOfficeInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var removedOffice = await this.officeService.Remove(input.OfficeToRemove);
                input.ResultMessage = $"Офис - {removedOffice.Name} - {removedOffice.PostCode} - беше прекратен успешно.";
            }

            input.AvailableOfficesToRemove = this.officeService.GeAllOfficesAndSortingCentersAsKeyValuePairs();

            return this.View(input);
        }

        private void LoadAddOfficeData(AddOfficeInputModel input)
        {
            input.CitiesItems = this.cityService.GetAllCitiesAsKeyValuePairs();
            input.SortingCentersItems = this.officeService.GetAllSortingCentersAsKeyValuePairs();
            input.AllUsers = this.userService.GetAllUsersAsKeyValuePair();
        }

        private void FillUpAvailableEmployeesToDelete(FireEmployeeInputModel input)
        {
            var currUserId = this.User
                            .FindFirst(ClaimTypes.NameIdentifier)
                            .Value;

            input.AvailableEmployeesToDelete = this.userService.GetAllUsersWithoutCurrentAsKeyValuePair(currUserId);
        }
    }
}
