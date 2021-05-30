// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;
    using FinalPoint.Services.Data;
    using FinalPoint.Web.ViewModels.AddDispose;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AddDisposeController : Controller
    {
        private readonly IOfficeService officeService;
        private readonly IClientService clientService;
        private readonly IParcelService parcelService;
        private readonly UserManager<ApplicationUser> userManager;

        public AddDisposeController(
            IOfficeService officeService,
            IClientService clientService,
            IParcelService parcelService,
            UserManager<ApplicationUser> userManager)
        {
            this.officeService = officeService;
            this.clientService = clientService;
            this.parcelService = parcelService;
            this.userManager = userManager;
        }

        public IActionResult AddParcel()
        {
            AddParcelInputModel model = new AddParcelInputModel();
            var allClients = this.clientService.GetAllClientsAsKeyValuePairs();

            model.SenderInputModel.AllClients = allClients;
            model.RecipentInputModel.AllClients = allClients;

            model.AllOffices = this.officeService.GeAllOfficesAndSortingCentersAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddParcel(AddParcelInputModel input)
        {
            var allClients = this.clientService.GetAllClientsAsKeyValuePairs();

            input.SenderInputModel.AllClients = allClients;
            input.RecipentInputModel.AllClients = allClients;

            input.AllOffices = this.officeService.GeAllOfficesAndSortingCentersAsKeyValuePairs();

            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                input.DeliveryPrice = 10;
                input.ChargeType = Data.Models.Enums.ParcelChargeType.Dimensions;
                input.SendingOfficeId = (int)user.WorkOfficeId;
                input.CurrentOfficeId = input.SendingOfficeId;
                input.SendingEmployeeId = user.PersonalId;

                await this.parcelService.CreateAsync(input);

                this.ViewBag.isSuccess = true;
            }

            return this.View(input);
        }

        public IActionResult DisposeParcel()
        {
            return this.View();
        }
    }
}
