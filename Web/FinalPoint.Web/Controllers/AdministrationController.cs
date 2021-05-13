// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System.Threading.Tasks;
    using FinalPoint.Services.Data.Administration;
    using FinalPoint.Services.Data.Misc;
    using FinalPoint.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    public class AdministrationController : Controller
    {
        private readonly IGetBasicData basicDataRep;
        private readonly IOfficeService officeService;

        public AdministrationController(
            IGetBasicData basicDataRep,
            IOfficeService officeService)
        {
            this.basicDataRep = basicDataRep;
            this.officeService = officeService;
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
            model.CitiesItems = this.basicDataRep.GetAllCitiesAsKeyValuePairs();
            model.SortingCentersItems = this.basicDataRep.GetAllSortingCentersAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOffice( AddOfficeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CitiesItems = this.basicDataRep.GetAllCitiesAsKeyValuePairs();
                input.SortingCentersItems = this.basicDataRep.GetAllSortingCentersAsKeyValuePairs();
                return this.View(input);
            }

            await this.officeService.CreateAsync(input);
            return this.RedirectToRoute(new { Controller = "Administration", Action="Index"});
        }

        public IActionResult RemoveOffice()
        {
            return this.View();
        }
    }
}
