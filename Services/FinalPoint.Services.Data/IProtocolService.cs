namespace FinalPoint.Services.Data
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

        Protocol GetProtocol(int protocolId);

        ICollection<Protocol> GetOpenProtocols(int recipentOfficeId, int senderOfficeId);

        ICollection<ParcelsTableShowParcelViewModel> GetAllProtocolParcels(int protocolId);

        Protocol GetProtocolById(int protocolId);

        Task LoadNewProtocolParcels(ApplicationUser user, int protocolId, int officeFromId, int officeToId);

        Task<CheckParcelResponseModel> TryCheckParcelInProtocol(int parcelId, int protocolId, int responsibleUserPersonalId);

        Task<CheckParcelResponseModel> TryRemoveParcelFromProtocol(int parcelId, int protocolId, int responsibleUserPersonalId);
    }
}
