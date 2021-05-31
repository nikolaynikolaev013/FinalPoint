using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalPoint.Data.Models;
using FinalPoint.Web.ViewModels.AddDispose;
using FinalPoint.Web.ViewModels.DTOs;

namespace FinalPoint.Services.Data
{
    public interface IParcelService
    {
        public Task CreateAsync(AddParcelInputModel input);

        ICollection<Parcel> GetAllParcelsFromTo(int officeFromId, int officeToId);

        ParcelCheckResultDto GetParcelAsParcelCheckResultDtoById(int parcelId);

        ICollection<Parcel> SearchForParcels(int? parcelId, string firstName, string lastName, string phoneNumber, ClaimsPrincipal user);

        Task<bool> DisposeParcel(int parcelId, ClaimsPrincipal user);

        Parcel GetParcelById(int parcelId);

        Parcel GetParcelWithOfficesAndCitiesById(int parcelId);

        Task<bool> UpdateParcelCurrentOfficeByOfficeId(int parcelId, int newCurrentOfficeId);

        Task<bool> UpdateParcelCurrentOfficeByOfficePostcode(int parcelId, int newCurrentOfficePostcode);
    }
}
