using System.Collections.Generic;
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

        ParcelCheckResultDto GetParcelById(int parcelId);
    }
}
