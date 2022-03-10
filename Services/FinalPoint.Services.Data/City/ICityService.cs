namespace FinalPoint.Services.Data.City
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinalPoint.Web.ViewModels.Administration;

    public interface ICityService
    {
        Task<int> CreateAsync(AddCityInputModel model);

        IEnumerable<KeyValuePair<string, string>> GetAllCitiesAsKeyValuePairs();
    }
}