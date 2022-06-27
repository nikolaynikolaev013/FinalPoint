namespace FinalPoint.Web.Business.Interfaces
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
        Task<NewOrOpenProtocolViewModel> CheckOrCreateProtocolAsync(NewProtocolCreateOrOpenDataInputDto input);

        Task<NewOrOpenProtocolViewModel> LoadOldProtocolAsync(NewProtocolCreateOrOpenDataInputDto input);

        Protocol GetProtocolWithOfficesById(int protocolId);

        Task<bool> CloseProtocolAsync(int protocolId);

        Task LoadNewProtocolParcelsAsync(ApplicationUser user, ProtocolType protocolType, int protocolId, int officeFromId, int officeToId, bool withDisposed);

        Task<CheckParcelResponseModel> TryAddParcelInProtocolAsync(int parcelId, int protocolId, int responsibleUserPersonalId);

        Task AddParcelToProtocolAsync(int parcelId, int protocolId, int resposnibleUserPersonalId, ParcelStatus status);

        Task<CheckParcelResponseModel> TryRemoveParcelFromProtocolAsync(int parcelId, int protocolId, int responsibleUserPersonalId);

        Task RemoveParcelFromProtocolAsync(int parcelId, int protocolId, int resposnibleUserPersonalId);

        ICollection<ProtocolParcel> GetAllParcelProtocolsByParcelId(int parcelId);

        bool CheckIfParcelIsInProtocol(int parcelId, int protocolId);

        bool CheckIfParcelIsAlreadyCheckedOrAddedToAProtocol(int parcelId, int protocolId);

        ICollection<ParcelsTableShowParcelViewModel> GetAllProtocolParcels(int protocolId, bool withDisposed);

        int GetNumberOfCheckedAndAddedParcels(int protocolId);

        ICollection<Protocol> GetLocalProtocolsByOfficeId(int id);

        ICollection<Protocol> GetOpenProtocols(ProtocolType protocolType, int recipentOfficeId, int senderOfficeId);

        ICollection<int> GetProtocolParcelIds(int protocolId);

        string TranslateType(ProtocolType input);


    }
}
