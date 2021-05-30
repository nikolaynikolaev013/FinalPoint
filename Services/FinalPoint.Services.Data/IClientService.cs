namespace FinalPoint.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.AddDispose;

    public interface IClientService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllClientsAsKeyValuePairs();

        public Task<Client> CreateAsync(AddClientInputModel input);
    }
}
