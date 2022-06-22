﻿// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

            ParcelChargeType chargeType = this.DecideChargeType(volumeWeight, weight);

            if (chargeType == ParcelChargeType.Dimensions)
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

        private ParcelChargeType DecideChargeType(decimal volumeWeight, decimal weight)
        {
            if (volumeWeight > weight)
            {
                return ParcelChargeType.Dimensions;
            }
            else
            {
                return ParcelChargeType.Weight;
            }
        }

        private (decimal, ParcelChargeType) CalculateDeliveryPrice(AddParcelInputModel input)
        {
            var finalPrice = this.CalculateDeliveryPrice(
                (decimal)input.Height,
                (decimal)input.Length,
                (decimal)input.Width,
                (decimal)input.Weight,
                input.HasCashOnDelivery,
                input.IsFragile,
                input.DontPaletize,
                input.CashOnDeliveryPrice,
                input.NumberOfParts);

            var volume = input.Height * input.Length * input.Width;

            return (finalPrice, this.DecideChargeType((decimal)volume, (decimal)input.Weight));
        }
    }
}
