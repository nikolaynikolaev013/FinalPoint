namespace FinalPoint.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.DTOs;
    using FinalPoint.Web.ViewModels.DTOs.LoadUnload;
    using FinalPoint.Web.ViewModels.Shared;
    using FinalPoint.Web.ViewModels.ViewComponents;
    using Microsoft.EntityFrameworkCore;

    public class ProtocolService : IProtocolService
    {
        private readonly IDeletableEntityRepository<Protocol> protocolRep;
        private readonly IDeletableEntityRepository<ProtocolParcel> protocolParcelRep;
        private readonly IUserService userService;
        private readonly IOfficeService officeService;
        private readonly IParcelService parcelService;

        public ProtocolService(
            IDeletableEntityRepository<Protocol> protocolRep,
            IDeletableEntityRepository<ProtocolParcel> protocolParcelRep,
            IUserService userService,
            IOfficeService officeService,
            IParcelService parcelService)
        {
            this.protocolRep = protocolRep;
            this.protocolParcelRep = protocolParcelRep;
            this.userService = userService;
            this.officeService = officeService;
            this.parcelService = parcelService;
        }

        public async Task<NewOrOpenProtocolViewModel> CheckOrCreateProtocol(NewProtocolCreateOrOpenDataInputDto input)
        {
            var currUser = this.userService
                        .GetUserByClaimsPrincipal(input.User);

            var openProtocol = this.GetOpenProtocols(input.Type, input.RecipentOfficeId, (int)currUser.WorkOfficeId).FirstOrDefault();

            var translatedType = this.TranslateType(input.Type);

            var viewModel = new NewOrOpenProtocolViewModel();

            if (openProtocol == null)
            {
                openProtocol = new Protocol()
                {
                    Type = input.Type,
                    CreatedByEmployee = currUser,
                    OfficeFromId = (int)currUser.WorkOfficeId,
                    OfficeToId = input.RecipentOfficeId,
                };

                await this.protocolRep.AddAsync(openProtocol);
                await this.protocolRep.SaveChangesAsync();

                viewModel.Message = $"Беше създаден нов протокол за {translatedType.ToLower()} с номер: {openProtocol.Id}.";
                viewModel.TypeOfMessage = "success animate__jello";
            }
            else
            {
                viewModel.Message = $"Беше отворен стар протокол за {translatedType.ToLower()} с номер: {openProtocol.Id}.";
                viewModel.TypeOfMessage = "warning animate__jello";
            }

            await this.LoadNewProtocolParcels(currUser, input.Type, openProtocol.Id, openProtocol.OfficeFromId, openProtocol.OfficeToId, false);

            viewModel.Protocol = openProtocol;
            viewModel.TranslatedType = translatedType;
            return viewModel;
        }

        public async Task<NewOrOpenProtocolViewModel> LoadOldProtocol(NewProtocolCreateOrOpenDataInputDto input)
        {
            var currUser = this.userService
                        .GetUserByClaimsPrincipal(input.User);

            var openProtocol = this.GetProtocolWithOfficesById(input.Id);

            var translatedType = this.TranslateType(openProtocol.Type);

            var viewModel = new NewOrOpenProtocolViewModel();

            if (openProtocol == null)
            {
                throw new ArgumentException("Invalid protocolId");
            }

            await this.LoadNewProtocolParcels(currUser, openProtocol.Type, openProtocol.Id, openProtocol.OfficeFromId, openProtocol.OfficeToId, true);

            viewModel.Protocol = openProtocol;
            viewModel.TranslatedType = translatedType;
            return viewModel;
        }

        public Protocol GetProtocolWithOfficesById(int protocolId)
        {
            return this.protocolRep
                        .AllAsNoTracking()
                        .Include(x=>x.OfficeFrom)
                        .Include(x=>x.OfficeTo)
                        .Where(x => x.Id == protocolId)
                        .FirstOrDefault();
        }

        public async Task<bool> CloseProtocol(int protocolId)
        {
            var protocol = this.protocolRep
                        .All()
                        .Where(x => x.Id == protocolId)
                        .FirstOrDefault();
            if (protocol != null)
            {
                protocol.IsClosed = true;
                await this.protocolRep.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task LoadNewProtocolParcels(ApplicationUser user, ProtocolType protocolType, int protocolId, int officeFromId, int officeToId, bool withDisposed)
        {
            var parcels = this.parcelService.GetAllParcelsFromTo(protocolType, user.WorkOfficeId, officeFromId, officeToId, withDisposed);

            foreach (var parcel in parcels)
            {
                if (!this.CheckIfParcelIsInProtocol(parcel.Id, protocolId))
                {
                    await this.protocolParcelRep.AddAsync(new ProtocolParcel
                    {
                        Parcel = parcel,
                        ProtocolId = protocolId,
                        Status = ParcelStatus.Unchecked,
                        ResponsibleUser = user,
                    });
                }
            }

            await this.protocolParcelRep.SaveChangesAsync();
        }

        public async Task<CheckParcelResponseModel> TryAddParcelInProtocol(int parcelId, int protocolId, int responsibleUserPersonalId)
        {
            var responseModel = new CheckParcelResponseModel();

            var proposedProtocolParcels = this.GetProtocolParcelIds(protocolId);

            var parcel = this.parcelService.GetParcelAsParcelCheckResultDtoById(parcelId);

            if (parcel != null)
            {
                if (proposedProtocolParcels.Contains(parcelId))
                {
                    if (this.CheckIfParcelIsAlreadyCheckedOrAddedToAProtocol(parcelId, protocolId))
                    {
                        responseModel.StatusClass = "danger";
                        responseModel.AnimationClass = "fadeIn";
                        responseModel.Status = ParcelStatus.AlreadyIn;
                    }
                    else
                    {
                        responseModel.StatusClass = "success";
                        responseModel.AnimationClass = "bounceIn";
                        responseModel.Status = ParcelStatus.Checked;
                        await this.AddParcelToProtocol(parcelId, protocolId, responsibleUserPersonalId, ParcelStatus.Checked);
                    }
                }
                else
                {
                    responseModel.StatusClass = "warning";
                    responseModel.AnimationClass = "fadeInDownBig";
                    responseModel.Status = ParcelStatus.Added;
                    await this.AddParcelToProtocol(parcelId, protocolId, responsibleUserPersonalId, ParcelStatus.Added);
                }

                responseModel.Description = parcel.Description;
                responseModel.NumberOfParts = parcel.NumberOfParts;
                responseModel.OfficeNameFrom = parcel.SendingOffice;
                responseModel.OfficeNameTo = parcel.ReceivingOffice;
                responseModel.ParcelId = parcel.Id;
            }
            else
            {
                responseModel.StatusClass = "danger";
                responseModel.AnimationClass = "flipInX";
                responseModel.Status = ParcelStatus.NotFound;
            }

            responseModel.TranslatedStatus = this.TranslateStatus(responseModel.Status);
            responseModel.ResultMessage = this.GetResultMessageBasedOnStatus(responseModel.Status, true);

            return responseModel;
        }

        public async Task AddParcelToProtocol(int parcelId, int protocolId, int resposnibleUserPersonalId, ParcelStatus status)
        {
            var protocolParcelObj = this.protocolParcelRep
                            .All()
                            .Where(x => x.ProtocolId == protocolId && x.ParcelId == parcelId)
                            .FirstOrDefault();

            var protocol = this.GetProtocolWithOfficesById(protocolId);
            var virtualOffice = this.officeService.GetVirtualOffice();

            if (protocol.Type == ProtocolType.Loading)
            {
                if (!await this.parcelService.UpdateParcelCurrentOfficeByOfficePostcode(parcelId, virtualOffice.PostCode))
                {
                    return;
                }
            }
            else
            {
                var responsibleUser = this.userService.GetUserByPersonalId(resposnibleUserPersonalId);

                if (!await this.parcelService.UpdateParcelCurrentOfficeByOfficeId(parcelId, responsibleUser.WorkOfficeId))
                {
                    return;
                }
            }

            if (protocolParcelObj != null)
            {
                protocolParcelObj.Status = status;
            }
            else
            {
                var currUser = this.userService.GetUserByPersonalId(resposnibleUserPersonalId);

                protocolParcelObj = new ProtocolParcel()
                {
                    Status = status,
                    ParcelId = parcelId,
                    ProtocolId = protocolId,
                    ResponsibleUser = currUser,
                    TimeEdited = DateTime.UtcNow,
                };
                await this.protocolParcelRep.AddAsync(protocolParcelObj);
            }

            await this.protocolParcelRep.SaveChangesAsync();
        }

        public async Task<CheckParcelResponseModel> TryRemoveParcelFromProtocol(int parcelId, int protocolId, int responsibleUserPersonalId)
        {
            var responseModel = new CheckParcelResponseModel();

            var parcel = this.parcelService.GetParcelAsParcelCheckResultDtoById(parcelId);

            if (parcel != null)
            {
                if (this.CheckIfParcelIsAlreadyCheckedOrAddedToAProtocol(parcelId, protocolId))
                {
                    responseModel.StatusClass = "warning";
                    responseModel.AnimationClass = "fadeIn";
                    responseModel.Status = ParcelStatus.Unchecked;
                    await this.RemoveParcelFromProtocol(parcelId, protocolId, responsibleUserPersonalId);
                }
                else
                {
                    responseModel.StatusClass = "danger";
                    responseModel.AnimationClass = "bounceIn";
                    responseModel.Status = ParcelStatus.FoundButNotInProtocol;
                }

                responseModel.Description = parcel.Description;
                responseModel.NumberOfParts = parcel.NumberOfParts;
                responseModel.OfficeNameFrom = parcel.SendingOffice;
                responseModel.OfficeNameTo = parcel.ReceivingOffice;
                responseModel.ParcelId = parcel.Id;
            }
            else
            {
                responseModel.StatusClass = "danger";
                responseModel.AnimationClass = "flipInX";
                responseModel.Status = ParcelStatus.NotFound;
            }

            responseModel.TranslatedStatus = this.TranslateStatus(responseModel.Status);
            responseModel.ResultMessage = this.GetResultMessageBasedOnStatus(responseModel.Status, false);

            return responseModel;
        }

        public async Task RemoveParcelFromProtocol(int parcelId, int protocolId, int resposnibleUserPersonalId)
        {
            var protocol = this.GetProtocolWithOfficesById(protocolId);

            if (protocol.Type == ProtocolType.Loading)
            {
                var responsibleUser = this.userService.GetUserByPersonalId(resposnibleUserPersonalId);

                if (!await this.parcelService.UpdateParcelCurrentOfficeByOfficeId(parcelId, responsibleUser.WorkOfficeId))
                {
                    return;
                }
            }
            else
            {
                if (!await this.parcelService.UpdateParcelCurrentOfficeByOfficePostcode(parcelId, 90001))
                {
                    return;
                }
            }

            var protocolParcelObj = this.protocolParcelRep
                            .All()
                            .Where(x => x.ProtocolId == protocolId && x.ParcelId == parcelId)
                            .FirstOrDefault();

            this.protocolParcelRep.HardDelete(protocolParcelObj);

            await this.protocolParcelRep.SaveChangesAsync();
        }

        public ICollection<ProtocolParcel> GetAllParcelProtocolsByParcelId(int parcelId)
        {
            return this.protocolParcelRep
                .AllAsNoTracking()
                .Include(x => x.ResponsibleUser)
                .Include(x => x.Protocol)
                .Include(x => x.Protocol.CreatedByEmployee)
                .Include(x => x.Protocol.OfficeFrom)
                .Include(x => x.Protocol.OfficeTo)
                .Where(x => x.ParcelId == parcelId)
                .OrderBy(x => x.CreatedOn)
                .ToHashSet();
        }

        public bool CheckIfParcelIsInProtocol(int parcelId, int protocolId)
        {
            return this.protocolRep
                .All()
                .Where(x => x.Id == protocolId)
                .Select(x => new { x.Parcels })
                .FirstOrDefault()
                .Parcels
                .FirstOrDefault(x => x.ParcelId == parcelId) != null;
        }

        public bool CheckIfParcelIsAlreadyCheckedOrAddedToAProtocol(int parcelId, int protocolId)
        {
            var parcel = this.protocolRep
                .All()
                .Where(x => x.Id == protocolId)
                .Select(x => new { x.Parcels })
                .FirstOrDefault()
                .Parcels
                .FirstOrDefault(x => x.ParcelId == parcelId);
            return parcel?.Status == ParcelStatus.Checked || parcel?.Status == ParcelStatus.Added;
        }

        public ICollection<ParcelsTableShowParcelViewModel> GetAllProtocolParcels(int protocolId, bool withDisposed)
        {
            var output = new HashSet<ParcelsTableShowParcelViewModel>();
            var protocolParcels = new HashSet<ProtocolParcel>();

            if (withDisposed)
            {
                protocolParcels = this.protocolParcelRep
                    .AllWithDeleted()
                    .Include(x => x.ResponsibleUser)

                    .Include(x => x.Parcel)
                    .ThenInclude(x => x.Sender)

                    .Include(x => x.Parcel)
                    .ThenInclude(x => x.Recipent)


                    .Include(x => x.Protocol)
                    .ThenInclude(x => x.OfficeFrom)

                    .Include(x => x.Protocol)
                    .ThenInclude(x => x.OfficeTo)
                    .ToHashSet();

            }
            else
            {
                protocolParcels = this.protocolParcelRep
                        .All()
                        .Include(x => x.ResponsibleUser)

                        .Include(x => x.Parcel)
                        .ThenInclude(x => x.Sender)

                        .Include(x => x.Parcel)
                        .ThenInclude(x => x.Recipent)


                        .Include(x => x.Protocol)
                        .ThenInclude(x => x.OfficeFrom)

                        .Include(x => x.Protocol)
                        .ThenInclude(x => x.OfficeTo)
                        .ToHashSet();
            }

            var result =
                    protocolParcels
                    .Where(x => x.ProtocolId == protocolId)

                    .OrderByDescending(x => x.Status == ParcelStatus.Added)
                    .ThenByDescending(x => x.Status == ParcelStatus.Checked)
                    .ToHashSet();

            foreach (var protocolParcel in result)
            {
                var newParcel = new ParcelsTableShowParcelViewModel()
                {
                    Parcel = this.parcelService.GetSingleParcelInfoByParcelId(protocolParcel.ParcelId),
                    ProtocolParcel = protocolParcel,
                    TranslatedStatus = this.TranslateStatus(protocolParcel.Status),
                };
                output.Add(newParcel);
            }

            return output;
        }

        public int GetNumberOfCheckedAndAddedParcels(int protocolId)
        {
            return this.protocolParcelRep
                .All()

                .Where(x => x.ProtocolId == protocolId
                        && (x.Status == ParcelStatus.Added || x.Status == ParcelStatus.Checked))
                .ToList()
                .Count();
        }

        public ICollection<Protocol> GetLocalProtocolsByOfficeId(int id)
        {
            return this.protocolRep
                .AllAsNoTracking()
                .Include(x => x.CreatedByEmployee)
                .Include(x => x.OfficeFrom)
                .Include(x => x.OfficeTo)
                .Where(x => x.OfficeFromId == id)
                .Select(x => new Protocol
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    CreatedByEmployee = x.CreatedByEmployee,
                    OfficeFrom = x.OfficeFrom,
                    OfficeTo = x.OfficeTo,
                    Type = x.Type,
                    IsClosed = x.IsClosed,
                })
                .OrderByDescending(x => x.CreatedOn)
                .ToHashSet();
        }

        public ICollection<Protocol> GetOpenProtocols(ProtocolType protocolType, int recipentOfficeId, int senderOfficeId)
        {
            var recipentOffice = this.officeService
                            .GetOfficeById(recipentOfficeId);

            var senderOffice = this.officeService
                            .GetOfficeById(senderOfficeId);

            return this.protocolRep
                .All()
                .Where(x => x.Type == protocolType
                    && x.IsClosed == false
                    && x.OfficeTo == recipentOffice
                    && x.OfficeFrom == senderOffice)
                .ToHashSet();
        }

        public ICollection<int> GetProtocolParcelIds(int protocolId)
        {
            return this.protocolParcelRep
                    .All()
                    .Where(x => x.ProtocolId == protocolId)
                    .Select(x => x.Parcel.Id)
                    .ToHashSet();
        }

        public string TranslateType(ProtocolType input)
        {
            var translatedType = string.Empty;

            switch (input)
            {
                case ProtocolType.Loading:
                    translatedType = "Товарене";
                    break;
                case ProtocolType.Unloading:
                    translatedType = "Разтоварване";
                    break;
                case ProtocolType.Grouping:
                    translatedType = "Групиране";
                    break;
                case ProtocolType.Ungrouping:
                    translatedType = "Разгрупиране";
                    break;
            }

            return translatedType;
        }

        private string TranslateStatus(ParcelStatus status)
        {
            var translatedStatus = string.Empty;
            switch (status)
            {
                case ParcelStatus.Added:
                    translatedStatus = "Добавена";
                    break;
                case ParcelStatus.Checked:
                    translatedStatus = "Проверена";
                    break;
                case ParcelStatus.Unchecked:
                    translatedStatus = "Непроверена";
                    break;
                case ParcelStatus.NotFound:
                    translatedStatus = "Невалиден номер";
                    break;
            }

            return translatedStatus;
        }

        private string GetResultMessageBasedOnStatus(ParcelStatus status, bool isCheck)
        {
            var resultMessage = string.Empty;
            if (isCheck)
            {
                switch (status)
                {
                    case ParcelStatus.Added:
                        resultMessage = "Добавена пратка";
                        break;
                    case ParcelStatus.Checked:
                        resultMessage = "Проверена пратка";
                        break;
                    case ParcelStatus.Unchecked:
                        resultMessage = "Непроверена пратка";
                        break;
                    case ParcelStatus.NotFound:
                        resultMessage = "Пратката не беше открита!";
                        break;
                    case ParcelStatus.AlreadyIn:
                        resultMessage = "Пратката вече е добавена в протокола!";
                        break;
                }
            }
            else
            {
                switch (status)
                {
                    case ParcelStatus.Unchecked:
                        resultMessage = "Пратката беше премахната успешно";
                        break;
                    case ParcelStatus.NotFound:
                        resultMessage = "Пратката не беше открита!";
                        break;
                    case ParcelStatus.FoundButNotInProtocol:
                        resultMessage = "Не можете да премахнете пратка, която не е проверена в протокола в протокола.";
                        break;
                }
            }

            return resultMessage;
        }

    }
}
