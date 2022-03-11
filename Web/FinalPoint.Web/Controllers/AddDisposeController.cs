// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Data.Models;
    using FinalPoint.Services.Data.Client;
    using FinalPoint.Services.Data.Office;
    using FinalPoint.Services.Data.Parcel;
    using FinalPoint.Services.Data.User;
    using FinalPoint.Web.ViewModels.AddDispose;
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
            if (this.ModelState.IsValid)
            {
                var user = this.userService.GetUserByClaimsPrincipal(this.User);

                input.DeliveryPrice = this.CalculateDeliveryPrice(input);

                input.ChargeType = Data.Models.Enums.ParcelChargeType.Dimensions;
                input.SendingOffice = user.WorkOffice;
                input.CurrentOffice = user.WorkOffice;
                input.SendingEmployee = user;

                await this.parcelService.CreateAsync(input);

                this.ViewBag.isSuccess = true;
                this.ModelState.Clear();
                input = new AddParcelInputModel();
            }

            this.FillUpAddParcelInputModel(input);
            return this.View(input);
        }


        public IActionResult DisposeParcel()
        {
            return this.View();
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public decimal CalculateDeliveryPrice(
            decimal height,
            decimal length,
            decimal width,
            decimal weight,
            bool hasCashOnDelivery,
            bool isFragile,
            bool dontPaletize,
            decimal cashOnDeliveryPrice,
            int numOfParts)
        {
            var finalPrice = 5.20m;

            var volumeWeight = height * width * length;

            if (volumeWeight > weight)
            {
                finalPrice += volumeWeight * 0.4m;
            }
            else
            {
                finalPrice += weight * 0.4m;
            }

            if (hasCashOnDelivery && cashOnDeliveryPrice > 0)
            {
                finalPrice += cashOnDeliveryPrice / 20.0m;
            }

            if (isFragile)
            {
                finalPrice *= 1.05m;
            }

            if (dontPaletize)
            {
                finalPrice *= 1.10m;
            }

            if (numOfParts > 1)
            {
                finalPrice += finalPrice * numOfParts * 0.05m;
            }

            return finalPrice;
        }

        private void FillUpAddParcelInputModel(AddParcelInputModel input)
        {
            var currUser = this.userService.GetUserById(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allClients = this.clientService.GetAllClientsAsKeyValuePairs();

            input.SenderInputModel.AllClients = allClients;
            input.RecipentInputModel.AllClients = allClients;

            input.AllOffices = this.officeService.GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(0);
            input.CurrOfficeAsString = this.officeService.GetOfficeAsStringById(currUser.WorkOfficeId);
        }

        private decimal CalculateDeliveryPrice(AddParcelInputModel input)
        {
            return this.CalculateDeliveryPrice(
                (decimal)input.Height,
                (decimal)input.Length,
                (decimal)input.Width,
                (decimal)input.Weight,
                input.HasCashOnDelivery,
                input.IsFragile,
                input.DontPaletize,
                input.CashOnDeliveryPrice,
                input.NumberOfParts);
        }
    }
}
