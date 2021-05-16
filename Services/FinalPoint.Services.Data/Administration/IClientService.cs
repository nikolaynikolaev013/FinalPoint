using System.Collections.Generic;

namespace FinalPoint.Services.Data.Administration
{
    public interface IClientService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllClientsAsKeyValuePairs();
    }
}
