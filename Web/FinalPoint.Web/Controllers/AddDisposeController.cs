// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;
    using FinalPoint.Services.Data;
    using FinalPoint.Web.ViewModels.AddDispose;
    using FinalPoint.Web.ViewModels.TrackParcel;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AddDisposeController : Controller
    {
        private readonly IOfficeService officeService;
        private readonly IClientService clientService;
        private readonly IParcelService parcelService;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public AddDisposeController(
            IOfficeService officeService,
            IClientService clientService,
            IParcelService parcelService,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.officeService = officeService;
            this.clientService = clientService;
            this.parcelService = parcelService;
            this.userService = userService;
            this.userManager = userManager;
        }

        public IActionResult AddParcel()
        {
            AddParcelInputModel model = new AddParcelInputModel();
            this.FillUpAddParcelInputModel(model);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddParcel(AddParcelInputModel input)
        {
            this.FillUpAddParcelInputModel(input);

            if (this.ModelState.IsValid)
            {
                var user = this.userService.GetUserByClaimsPrincipal(this.User);
                input.DeliveryPrice = 10;
                input.ChargeType = Data.Models.Enums.ParcelChargeType.Dimensions;
                input.SendingOffice = user.WorkOffice;
                input.CurrentOffice = user.WorkOffice;
                input.SendingEmployee = user;

                await this.parcelService.CreateAsync(input);

                this.ViewBag.isSuccess = true;
            }

            return this.View(input);
        }

        public IActionResult DisposeParcel()
        {
            return this.View();
        }

        private void FillUpAddParcelInputModel(AddParcelInputModel input)
        {
            var currUser = this.userService.GetUserById(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allClients = this.clientService.GetAllClientsAsKeyValuePairs();

            input.SenderInputModel.AllClients = allClients;
            input.RecipentInputModel.AllClients = allClients;

            input.AllOffices = this.officeService.GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(currUser.WorkOfficeId);
            input.CurrOfficeAsString = this.officeService.GetOfficeAsStringById(currUser.WorkOfficeId);
        }
    }
}
