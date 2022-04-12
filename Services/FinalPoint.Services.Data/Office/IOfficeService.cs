﻿namespace FinalPoint.Services.Data.Office
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.Administration;

    public interface IOfficeService
    {
        Task<Office> CreateAsync(AddOfficeInputModel model);

        Task<Office> Remove(int officePostcodeToSkip);

        Task<bool> ChangeOfficeTheme(int officeId, int themeId);

        Office GetOfficeById(int officeId);

        Office GetOfficeByPostcode(int officePostcode);

        HashSet<int> GetAllOfficeIdsInRangeOfSortingCenterId(int sortingCenterId);

        HashSet<string> GetAllOfficesWithoutVirtual();

        string GetOfficeAsStringById(int officeId);

        Office GetVirtualOffice();

        IEnumerable<KeyValuePair<string, string>> GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(int officetoSkip);

        IEnumerable<KeyValuePair<string, string>> GetLoadUnloadOffices(Office currentOffice);

        IEnumerable<KeyValuePair<string, string>> GeAllOfficesAndSortingCentersAsKeyValuePairs();

        IEnumerable<KeyValuePair<string, string>> GetAllSortingCentersAsKeyValuePairs();

    }
}
