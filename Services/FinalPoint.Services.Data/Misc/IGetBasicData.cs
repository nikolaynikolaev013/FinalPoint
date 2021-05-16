using System;
using System.Collections.Generic;

namespace FinalPoint.Services.Data.Misc
{
    public interface IGetBasicData
    {
        IEnumerable<KeyValuePair<string, string>> GetAllSortingCentersAsKeyValuePairs();

        IEnumerable<KeyValuePair<string, string>> GetAllCitiesAsKeyValuePairs();

        IEnumerable<KeyValuePair<string, string>> GeAllOfficesAsKeyValuePairs();
    }
}
