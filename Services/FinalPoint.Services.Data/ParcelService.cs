namespace FinalPoint.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.AddDispose;
    using FinalPoint.Web.ViewModels.DTOs;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ParcelService : IParcelService
    {
        private readonly IDeletableEntityRepository<Parcel> parcelRep;
        private readonly IClientService clientService;
        private readonly IOfficeService officeService;
        private readonly IUserService userService;
        private readonly IParcelService parcelService;

        public ParcelService(
            IDeletableEntityRepository<Parcel> parcelRep,
            IClientService clientService,
            IOfficeService officeService,
            IUserService userService,
            IParcelService parcelService)
        {
            this.parcelRep = parcelRep;
            this.clientService = clientService;
            this.officeService = officeService;
            this.userService = userService;
            this.parcelService = parcelService;
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

        public ParcelCheckResultDto GetParcelAsParcelCheckResultDtoById(int parcelId)
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

        public Parcel GetParcelById(int parcelId)
        {
            return this.parcelRep
                .All()
                .Where(x => x.Id == parcelId)
                .FirstOrDefault();
        }

        public Parcel GetParcelWithOfficesAndCitiesById(int parcelId)
        {
            return this.parcelRep
                .All()
                .Include(x => x.ReceivingOffice)
                .ThenInclude(x => x.City)
                .Include(x => x.SendingOffice)
                .ThenInclude(x => x.City)
                .Include(x => x.CurrentOffice)
                .ThenInclude(x => x.City)
                .Where(x => x.Id == parcelId)
                .FirstOrDefault();
        }

        public ICollection<Parcel> SearchForParcels(int? parcelId, string firstName, string lastName, string phoneNumber, ClaimsPrincipal user)
        {
            var employee = this.userService.GetUserByClaimsPrincipal(user);

            return this.parcelRep
                .All()
                .Where(x => parcelId != 0 ? x.Id == parcelId : true
                            && firstName != "0" ? x.Recipent.FirstName == firstName : true
                            && lastName != "0" ? x.Recipent.LastName == lastName : true
                            && phoneNumber != "0" ? x.Recipent.PhoneNumber == phoneNumber : true
                            && x.ReceivingOfficeId == employee.WorkOfficeId)
                .Include(x => x.SendingOffice)
                .ThenInclude(x => x.City)
                .Include(x => x.ReceivingOffice)
                .ThenInclude(x => x.City)
                .Include(x => x.Sender)
                .Include(x => x.Recipent)
                .ToList();
        }

        public async Task<bool> DisposeParcel(int parcelId, ClaimsPrincipal user)
        {
            var employee = this.userService.GetUserByClaimsPrincipal(user);
            var parcel = this.parcelRep
                        .All()
                        .Where(x => x.Id == parcelId)
                        .FirstOrDefault();

            if (parcel != null && parcel.CurrentOfficeId == employee.WorkOfficeId)
            {
                parcel.SendingEmployeeId = employee.WorkOfficeId;
                parcel.CurrentOfficeId = employee.WorkOfficeId;

                this.parcelRep.Delete(parcel);
                await this.parcelRep.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateParcelCurrentOffice(int parcelId,int newCurrentOfficeId)
        {
            var parcel = this.parcelService.GetParcelById(parcelId);
            var newCurrentOffice = this.officeService.GetOffice(newCurrentOfficeId);

            if (parcel != null && newCurrentOffice != null)
            {
                parcel.CurrentOffice = newCurrentOffice;
                await this.parcelRep.SaveChangesAsync();
                return true;
            }

            return false;
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
