namespace FinalPoint.Services.Data.Parcel
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.AddDispose;
    using FinalPoint.Web.ViewModels.DTOs;
    using FinalPoint.Web.ViewModels.Shared;

    public interface IParcelService
    {
        public Task<Parcel> CreateAsync(AddParcelInputModel input);

        Task<bool> DisposeParcel(int parcelId, ClaimsPrincipal user);

        Task<bool> UpdateParcelCurrentOfficeByOfficeId(int parcelId, int newCurrentOfficeId);

        Task<bool> UpdateParcelCurrentOfficeByOfficePostcode(int parcelId, int newCurrentOfficePostcode);

        ICollection<SingleParcelSearchShowPartialViewModel> SearchForParcels(int? parcelId, string firstName, string lastName, string phoneNumber, ClaimsPrincipal user, bool isGlobal);

        ICollection<FinalPoint.Data.Models.Parcel> GetAllParcelsFromTo(ProtocolType protocolType, int currentOfficeId, int officeFromId, int officeToId, bool withDisposed);

        ParcelCheckResultDto GetParcelAsParcelCheckResultDtoById(int parcelId);

        Parcel GetParcelById(int parcelId);

        SingleParcelSearchShowPartialViewModel GetSingleParcelInfoByParcelId(int parcelId);

        Parcel GetParcelWithOfficesAndCitiesById(int parcelId);
    }
}
