﻿namespace FinalPoint.Services.Data
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.DTOs;
    using FinalPoint.Web.ViewModels.DTOs.LoadUnload;
    using FinalPoint.Web.ViewModels.ViewComponents;

    public interface IProtocolService
    {
        Task<NewOrOpenProtocolViewModel> CheckOrCreateProtocol(NewProtocolCreateOrOpenDataInputDto input);

        Task<bool> CloseProtocol(int protocolId);

        Task LoadNewProtocolParcels(ApplicationUser user, ProtocolType protocolType, int protocolId, int officeFromId, int officeToId);

        Task<CheckParcelResponseModel> TryAddParcelInProtocol(int parcelId, int protocolId, int responsibleUserPersonalId);

        Task AddParcelToProtocol(int parcelId, int protocolId, int resposnibleUserPersonalId, ParcelStatus status);

        Task<CheckParcelResponseModel> TryRemoveParcelFromProtocol(int parcelId, int protocolId, int responsibleUserPersonalId);

        Task RemoveParcelFromProtocol(int parcelId, int protocolId, int resposnibleUserPersonalId);

        ICollection<ProtocolParcel> GetAllParcelProtocolsByParcelId(int parcelId);

        bool CheckIfParcelIsInProtocol(int parcelId, int protocolId);

        bool CheckIfParcelIsAlreadyCheckedOrAddedToAProtocol(int parcelId, int protocolId);

        bool IsClosed(int protocolId);

        ICollection<ParcelsTableShowParcelViewModel> GetAllProtocolParcels(int protocolId);

        int GetNumberOfCheckedAndAddedParcels(int protocolId);

        Protocol GetProtocolWithOfficesById(int protocolId);

        ICollection<Protocol> GetOpenProtocols(ProtocolType protocolType, int recipentOfficeId, int senderOfficeId);

        ICollection<int> GetProtocolParcelIds(int protocolId);

        string TranslateType(ProtocolType input);


    }
}
