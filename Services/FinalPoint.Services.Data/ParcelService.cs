namespace FinalPoint.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
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

        public ParcelService(
            IDeletableEntityRepository<Parcel> parcelRep,
            IClientService clientService,
            IOfficeService officeService,
            IUserService userService)
        {
            this.parcelRep = parcelRep;
            this.clientService = clientService;
            this.officeService = officeService;
            this.userService = userService;
        }

        public async Task<Parcel> CreateAsync(AddParcelInputModel input)
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

            return newParcel;
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

        public async Task<bool> UpdateParcelCurrentOfficeByOfficeId(int parcelId, int newCurrentOfficeId)
        {
            var parcel = this.GetParcelById(parcelId);
            var newCurrentOffice = this.officeService.GetOfficeById(newCurrentOfficeId);

            if (parcel != null && newCurrentOffice != null)
            {
                parcel.CurrentOffice = newCurrentOffice;
                await this.parcelRep.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateParcelCurrentOfficeByOfficePostcode(int parcelId, int newCurrentOfficePostcode)
        {
            var parcel = this.GetParcelById(parcelId);
            var newCurrentOffice = this.officeService.GetOfficeByPostcode(newCurrentOfficePostcode);

            if (parcel != null && newCurrentOffice != null)
            {
                parcel.CurrentOffice = newCurrentOffice;
                await this.parcelRep.SaveChangesAsync();
                return true;
            }

            return false;
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
                .Include(x => x.CurrentOffice)
                .ThenInclude(x => x.City)
                .Include(x => x.SendingOffice)
                .ThenInclude(x => x.City)
                .Include(x => x.ReceivingOffice)
                .ThenInclude(x => x.City)
                .Include(x => x.Sender)
                .Include(x => x.Recipent)
                .OrderByDescending(x => x.CurrentOfficeId == x.ReceivingOfficeId)
                .ToList();
        }

        public ICollection<Parcel> GetAllParcelsFromTo(ProtocolType protocolType, int currentOfficeId, int officeFromId, int officeToId)
        {
            var currentOffice = this.officeService.GetOfficeById(currentOfficeId);
            var officeFrom = this.officeService.GetOfficeById(officeFromId);
            var officeTo = this.officeService.GetOfficeById(officeToId);

            var officeIdsInRangeOfSortingCenter = this.officeService.GetAllOfficeIdsInRangeOfSortingCenterId(currentOfficeId);

            if (protocolType == ProtocolType.Loading)
            {
                var officeIdsInRangeOfTheOfficeToSortingCenter = this.officeService.GetAllOfficeIdsInRangeOfSortingCenterId(officeToId);

                return this.parcelRep
                    .All()
                    .Where(
                            x => x.CurrentOfficeId == officeFromId

                        // && x.Protocols
                        //    .OrderByDescending(x => x.CreatedOn)
                        //    .FirstOrDefault()
                        //    .Protocol
                        //    .IsClosed == true

                        && 

                            (
                            //    // If the parcel is in the current office
                            //    // if the last protocol is closed
                            //    (officeFromId == x.CurrentOfficeId
                            //    && x.Protocols
                            //    .OrderByDescending(x => x.CreatedOn)
                            //    .FirstOrDefault()
                            //    .Protocol.IsClosed == true)

                            //    // If the parcel is in the current office
                            //    // If there is no last protocol
                            // //|| (currentOfficeId == x.CurrentOfficeId
                            // //   && x.Protocols.OrderByDescending(x => x.CreatedOn)
                            // //   .FirstOrDefault() == null)
                            //// If the current office is an office
                             (currentOffice.OfficeType == OfficeType.Office

                            // if we are loading up to a sorting center
                            && officeTo.OfficeType == OfficeType.SortingCenter

                            //if the sorting center we are loading to is the responsible sorting center of the curroffice
                            && officeFrom.ResponsibleSortingCenterId == currentOffice.ResponsibleSortingCenterId)
                            
                            || (
                                
                                // if the current office is a sorting center
                                officeFrom.OfficeType == OfficeType.SortingCenter

                                // if we are loading up to a sorting center
                                && officeTo.OfficeType == OfficeType.SortingCenter

                                // If the last protocol of the parcel is closed
                                && (x.Protocols
                                .OrderByDescending(x => x.CreatedOn)
                                .FirstOrDefault()
                                .Protocol.IsClosed == true

                                     // If the parcel is in the current office
                                     // If there is no last protocol
                                    || (currentOfficeId == x.CurrentOfficeId
                                       && x.Protocols.OrderByDescending(x => x.CreatedOn)
                                       .FirstOrDefault() == null)))
                            || (

                            // if the current office is a sorting center
                            currentOffice.OfficeType == OfficeType.SortingCenter

                            // if we are loading up to an office
                            && officeTo.OfficeType == OfficeType.Office

                            && x.Protocols
                                .OrderByDescending(x => x.CreatedOn)
                                .FirstOrDefault()
                                .Protocol.IsClosed == true

                            && officeIdsInRangeOfSortingCenter.Contains(x.ReceivingOfficeId)

                            && x.ReceivingOfficeId == officeToId))
                    )
                    .ToHashSet();
            }
            else if (protocolType == ProtocolType.Unloading)
            {
                return this.parcelRep
                        .All()
                        .Where(x => x.CurrentOfficeId == 13 // If it is in the Virtual office

                                    // If the last protocol of the parcel is closed
                                && x.Protocols.OrderByDescending(x => x.CreatedOn)
                                    .FirstOrDefault()
                                    .Protocol.IsClosed == true

                                && (

                                        // if the current office is a sorting center
                                        // if the parcel is from an office in its range
                                        (currentOffice.OfficeType == OfficeType.SortingCenter

                                         && officeIdsInRangeOfSortingCenter.Contains(
                                            x.Protocols
                                            .Where(p=>p.Protocol.IsClosed == true)
                                            .OrderByDescending(x => x.CreatedOn)
                                            .FirstOrDefault()
                                            .Protocol.OfficeFromId))

                                            // if the current office is a sorting center
                                        || (currentOffice.OfficeType == OfficeType.SortingCenter

                                            // if the parcel was sent from a sorting center
                                            && x.Protocols
                                                .Where(p=>p.Protocol.IsClosed == true)
                                                .OrderByDescending(x=>x.CreatedOn)
                                                .FirstOrDefault()
                                                .Protocol
                                                .OfficeFrom
                                                .OfficeType == OfficeType.SortingCenter

                                                // if the parcels' receiving office is in the range of the current sorting center
                                            && (officeIdsInRangeOfSortingCenter.Contains(x.ReceivingOfficeId)

                                                // if the parcel is to the sorting center
                                                || x.ReceivingOfficeId == currentOfficeId))

                                        || (currentOffice.OfficeType == OfficeType.Office

                                            && x.SendingOffice.OfficeType == OfficeType.Office

                                            && x.Protocols
                                                .Where(p => p.Protocol.IsClosed == true)
                                                .OrderByDescending(x => x.CreatedOn)
                                                .FirstOrDefault()
                                                .Protocol
                                                .OfficeFrom.OfficeType == OfficeType.SortingCenter

                                            && x.Protocols
                                                .Where(p => p.Protocol.IsClosed == true)
                                                .OrderByDescending(x => x.CreatedOn)
                                                .FirstOrDefault()
                                                .Protocol
                                                .OfficeFromId == x.ReceivingOffice.ResponsibleSortingCenterId)

                                            // if the current office is an office
                                        || (currentOffice.OfficeType == OfficeType.Office

                                            && x.Protocols
                                                .Where(p => p.Protocol.IsClosed == true)
                                                .OrderByDescending(x => x.CreatedOn)
                                                .FirstOrDefault()
                                                .Protocol
                                                .OfficeToId == currentOfficeId

                                            // if the parceels' last current office is the responsible sorting center of the receiving office
                                            && x.Protocols
                                                .Where(p => p.Protocol.IsClosed == true)
                                                .OrderByDescending(x => x.CreatedOn)
                                                .FirstOrDefault()
                                                .Protocol
                                                .OfficeFromId == currentOffice.ResponsibleSortingCenterId
                                                )))
                        .ToHashSet();
            }
            else
            {
                throw new InvalidOperationException();
            }
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
