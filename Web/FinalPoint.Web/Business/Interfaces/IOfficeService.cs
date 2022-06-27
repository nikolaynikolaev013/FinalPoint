namespace FinalPoint.Web.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.Administration;

    public interface IOfficeService
    {
        Task<Office> CreateAsync(AddOfficeInputModel model);

        Task<Office> RemoveAsync(int officePostcodeToSkip);

        Task<bool> ChangeOfficeThemeAsync(int officeId, int themeId);

        Office GetOfficeById(int officeId);

        Office GetOfficeByPostcode(int officePostcode);

        HashSet<int> GetAllOfficeIdsInRangeOfSortingCenterId(int sortingCenterId);

        HashSet<string> GetAllOfficesAsStringWithoutVirtual();

        string GetOfficeAsStringById(int officeId);

        Office GetVirtualOffice();

        IEnumerable<KeyValuePair<string, string>> GetAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(int officetoSkip);

        IEnumerable<KeyValuePair<string, string>> GetLoadUnloadOffices(Office currentOffice);

        IEnumerable<KeyValuePair<string, string>> GetAllOfficesAndSortingCentersAsKeyValuePairs();

        IEnumerable<KeyValuePair<string, string>> GetAllSortingCentersAsKeyValuePairs();

    }
}
