namespace FinalPoint.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.Administration;

    public interface IOfficeService
    {
        Task<Office> CreateAsync(AddOfficeInputModel model);

        Task<Office> Remove(int officePostcodeToSkip);
        
        Office GetOfficeById(int officeId);

        Office GetOfficeByPostcode(int officePostcode);

        HashSet<int> GetAllOfficeIdsInRangeOfSortingCenterId(int sortingCenterId);

        string GetOfficeAsStringById(int officeId);

        IEnumerable<KeyValuePair<string, string>> GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(int officetoSkip);

        IEnumerable<KeyValuePair<string, string>> GeAllOfficesAndSortingCentersAsKeyValuePairs();

        IEnumerable<KeyValuePair<string, string>> GetAllSortingCentersAsKeyValuePairs();

    }
}
