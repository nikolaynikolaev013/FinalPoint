namespace FinalPoint.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.Administration;

    public interface IOfficeService
    {
        Task CreateAsync(AddOfficeInputModel model);

        IEnumerable<KeyValuePair<string, string>> GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(int officetoSkip);

        IEnumerable<KeyValuePair<string, string>> GeAllOfficesAndSortingCentersAsKeyValuePairs();

        IEnumerable<KeyValuePair<string, string>> GetAllSortingCentersAsKeyValuePairs();

        Task<Office> Remove(int officePostcodeToSkip);

        Office GetOffice(int officeId);
    }
}
