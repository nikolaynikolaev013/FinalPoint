namespace FinalPoint.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinalPoint.Web.ViewModels.Administration;

    public interface ICityService
    {
        Task<int> CreateNewCity(AddCityInputModel model);

        IEnumerable<KeyValuePair<string, string>> GetAllCitiesAsKeyValuePairs();
    }
}
