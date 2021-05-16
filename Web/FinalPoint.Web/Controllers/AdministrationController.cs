// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System.Threading.Tasks;
    using FinalPoint.Services.Data.Administration;
    using FinalPoint.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    public class AdministrationController : Controller
    {
        private readonly IOfficeService officeService;
        private readonly ICityService cityService;
        private readonly IUserService userService;

        public AdministrationController(
            IOfficeService officeService,
            ICityService cityService,
            IUserService userService)
        {
            this.officeService = officeService;
            this.cityService = cityService;
            this.userService = userService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult HireEmployee()
        {
            return this.View();
        }

        public IActionResult FireEmployee()
        {
            return this.View();
        }

        public IActionResult AddOffice()
        {
            AddOfficeInputModel model = new AddOfficeInputModel();
            model.CitiesItems = this.cityService.GetAllCitiesAsKeyValuePairs();
            model.SortingCentersItems = this.officeService.GetAllSortingCentersAsKeyValuePairs();
            model.AllUsers = this.userService.GetAllUsersAsKeyValuePair();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOffice(AddOfficeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CitiesItems = this.cityService.GetAllCitiesAsKeyValuePairs();
                input.SortingCentersItems = this.officeService.GetAllSortingCentersAsKeyValuePairs();
                input.AllUsers = this.userService.GetAllUsersAsKeyValuePair();
                return this.View(input);
            }

            if (input.CityInputModel.Name != null
                && input.CityInputModel.Postcode != null)
            {
                var newCityId = await this.cityService.CreateNewCity(input.CityInputModel);
                input.CityId = newCityId;
            }

            await this.officeService.CreateAsync(input);
            return this.RedirectToRoute(new { Controller = "Administration", Action = "Index" });
        }

        public IActionResult RemoveOffice()
        {
            return this.View();
        }
    }
}
