// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using AutoMapper;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.Business.Interfaces;
    using FinalPoint.Web.ViewModels;
    using FinalPoint.Web.ViewModels.AddDispose;
    using FinalPoint.Web.ViewModels.DTOs;
    using FinalPoint.Web.ViewModels.DTOs.AddDisposeParcel;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AddDisposeController : BaseController
    {
        private readonly IOfficeService officeService;
        private readonly IClientService clientService;
        private readonly IParcelService parcelService;
        private readonly IUserService userService;
        private readonly IEmailService mailService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public AddDisposeController(
            IOfficeService officeService,
            IClientService clientService,
            IParcelService parcelService,
            IUserService userService,
            IEmailService mailService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.officeService = officeService;
            this.clientService = clientService;
            this.parcelService = parcelService;
            this.userService = userService;
            this.mailService = mailService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IActionResult AddParcel()
        {
            AddParcelInputModel model = new AddParcelInputModel();
            this.LoadAddParcelInputModel(model);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddParcel(AddParcelInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var user = this.userService.GetUserByClaimsPrincipal(this.User);

                (input.DeliveryPrice, input.ChargeType) = this.CalculateDeliveryPrice(input);

                input.SendingOffice = user.WorkOffice;
                input.CurrentOffice = user.WorkOffice;
                input.SendingEmployee = user;

                var newParcel = await this.parcelService.CreateAsync(input);

                await this.mailService.SendNewParcelEmailsAsync(newParcel.Id);

                this.ViewBag.isSuccess = true;
                this.ModelState.Clear();

                input = new AddParcelInputModel();
            }

            this.LoadAddParcelInputModel(input);
            return this.View(input);
        }

        public IActionResult DisposeParcel()
        {
            return this.View();
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public decimal CalculateDeliveryPrice(CalculateDeliveryPriceDto input)
        {
            var finalPrice = 5.20m;

            var volumeWeight = input.Height * input.Width * input.Length;

            // To decide if the charge is by dimensions or my weight
            finalPrice += (this.DecideChargeType(volumeWeight, input.Weight) == ParcelChargeType.Dimensions ? volumeWeight * 0.1m : input.Weight * 0.1m);

            // if cash on delivery is selected
            finalPrice += (input.HasCashOnDelivery && input.CashOnDeliveryPrice > 0 ? input.CashOnDeliveryPrice / 95.0m : 0);

            // if IsFragile is selected
            finalPrice *= (input.IsFragile ? 1.02m : 1);

            // if DontPaletize is selected
            finalPrice *= (input.DontPaletize ? 1.04m : 1);

            // if there are more than 1 part
            finalPrice += (input.NumberOfParts > 1 ? finalPrice * input.NumberOfParts * 0.03m : 0);

            return finalPrice;
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public ClientDetailsDto GetClientDetailsById(int id)
        {
            var client = this.clientService.GetClientById(id);
            return this.mapper.Map<ClientDetailsDto>(client);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<Result> EditClientDetails(ClientDetailsDto clientDetails)
        {
            var clientModel = this.mapper.Map<Client>(clientDetails);
            return await this.clientService.EditClientInfoAsync(clientModel);
        }

        private void LoadAddParcelInputModel(AddParcelInputModel input)
        {
            var currUser = this.userService.GetUserById(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allClients = this.clientService.GetAllClientsAsKeyValuePairs();

            input.SenderInputModel.AllClients = allClients;
            input.RecipentInputModel.AllClients = allClients;

            input.AllOffices = this.officeService.GetAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(0);
            input.CurrOfficeAsString = this.officeService.GetOfficeAsStringById(currUser.WorkOfficeId);
        }

        private (decimal, ParcelChargeType) CalculateDeliveryPrice(AddParcelInputModel input)
        {
            var heightAsDecimal = decimal.Parse(input.Height.Replace('.', ','));
            var lengthAsDecimal = decimal.Parse(input.Length.Replace('.', ','));
            var widthAsDecimal = decimal.Parse(input.Width.Replace('.', ','));

            var calculatePriceDto = this.mapper.Map<CalculateDeliveryPriceDto>(input);
            calculatePriceDto.Height = heightAsDecimal;
            calculatePriceDto.Length = lengthAsDecimal;
            calculatePriceDto.Width = widthAsDecimal;

            var finalPrice = this.CalculateDeliveryPrice(calculatePriceDto);

            var volume = heightAsDecimal * lengthAsDecimal * widthAsDecimal;

            return (finalPrice, this.DecideChargeType(volume, (decimal)input.Weight));
        }

        private ParcelChargeType DecideChargeType(decimal volumeWeight, decimal weight)
        {
            if (volumeWeight > weight / 2)
            {
                return ParcelChargeType.Dimensions;
            }
            else
            {
                return ParcelChargeType.Weight;
            }
        }
    }
}
