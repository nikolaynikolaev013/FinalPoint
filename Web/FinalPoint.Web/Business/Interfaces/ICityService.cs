namespace FinalPoint.Web.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinalPoint.Web.ViewModels.Administration;

    public interface ICityService
    {
        Task<int> CreateAsync(AddCityInputModel model);

        Task<bool> DeleteIfNoOfficeAssociatedToIt(int cityId, int officeOffsetId);

        IEnumerable<KeyValuePair<string, string>> GetAllCitiesAsKeyValuePairs();
    }
}