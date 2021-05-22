namespace FinalPoint.Services.Data.Administration
{
    using System;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.AddDispose;
    using Microsoft.AspNetCore.Identity;

    public class ParcelService : IParcelService
    {
        private readonly IDeletableEntityRepository<Parcel> parcelRep;
        private readonly IClientService clientService;

        public ParcelService(
            IDeletableEntityRepository<Parcel> parcelRep,
            IClientService clientService,
            UserManager<ApplicationUser> userManager)
        {
            this.parcelRep = parcelRep;
            this.clientService = clientService;
        }

        public async Task CreateAsync(AddParcelInputModel input)
        {
            Parcel newParcel = new Parcel()
            {
                Description = input.Description,
                Width = input.Width,
                Height = input.Height,
                Length = input.Length,
                Weight = input.Weight,
                HasCashOnDelivery = input.HasCashOnDelivery,
                CashOnDeliveryPrice = input.CashOnDeliveryPrice,
                IsFragile = input.IsFragile,
                DontPaletize = input.DontPaletize,
                SendingEmployeeId = input.SendingEmployeeId,
                CurrentOfficeId = input.CurrentOfficeId,
                SendingOfficeId = input.SendingOfficeId,
                ReceivingOfficeId = input.ReceivingOfficeId,
                ChargeType = input.ChargeType,
                DeliveryPrice = input.DeliveryPrice,
            };

            newParcel.SenderId = await this.AddClient(input.SenderInputModel);
            newParcel.RecipentId = await this.AddClient(input.RecipentInputModel);

            await this.parcelRep.AddAsync(newParcel);
            await this.parcelRep.SaveChangesAsync();
        }

        private async Task<int> AddClient(AddClientInputModel input)
        {
            if (!string.IsNullOrEmpty(input.FirstName) &&
                !string.IsNullOrEmpty(input.LastName))
            {
                var newClient = await this.clientService.CreateAsync(input);
                return newClient.Id;
            }
            else
            {
                return input.ClientId;
            }
        }
    }
}
