namespace FinalPoint.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinalPoint.Web.ViewModels.Administration;

    public interface IOfficeService
    {
        Task CreateAsync(AddOfficeInputModel model);

        IEnumerable<KeyValuePair<string, string>> GeAllOfficesAsKeyValuePairs();

        IEnumerable<KeyValuePair<string, string>> GetAllSortingCentersAsKeyValuePairs();
    }
}
