namespace FinalPoint.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.AddDispose;
    using FinalPoint.Web.ViewModels.DTOs;
    using Microsoft.AspNetCore.Identity;

    public class ParcelService : IParcelService
    {
        private readonly IDeletableEntityRepository<Parcel> parcelRep;
        private readonly IClientService clientService;
        private readonly IOfficeService officeService;

        public ParcelService(
            IDeletableEntityRepository<Parcel> parcelRep,
            IClientService clientService,
            IOfficeService officeService)
        {
            this.parcelRep = parcelRep;
            this.clientService = clientService;
            this.officeService = officeService;
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
                NumberOfParts = input.NumberOfParts,
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

        public ICollection<Parcel> GetAllParcelsFromTo(int officeFromId, int officeToId)
        {
            var officeFrom = this.officeService.GetOffice(officeFromId);

            return this.parcelRep
                .All()
                .Where(x => x.CurrentOfficeId == officeFromId
                &&
                    (x.ReceivingOfficeId == officeToId
                    || x.ReceivingOffice.ResponsibleSortingCenterId == officeToId
                    || officeFrom.ResponsibleSortingCenterId == officeToId)
                )
                .ToHashSet();
        }

        public ParcelCheckResultDto GetParcelById(int parcelId)
        {
            return this.parcelRep
                .All()
                .Where(x => x.Id == parcelId)
                .Select(x=>new ParcelCheckResultDto{
                    Description = x.Description,
                    NumberOfParts = x.NumberOfParts,
                    SendingOffice = x.SendingOffice.Name,
                    ReceivingOffice = x.ReceivingOffice.Name,
                    Id = x.Id,
                })
                .FirstOrDefault();
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
