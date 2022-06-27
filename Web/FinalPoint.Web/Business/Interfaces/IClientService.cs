namespace FinalPoint.Web.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels;
    using FinalPoint.Web.ViewModels.AddDispose;

    public interface IClientService
    {
        public Task<Client> CreateAsync(Client input);

        public Task<Result> EditClientInfoAsync(Client input);

        public Client GetClientById(int id);

        IEnumerable<KeyValuePair<string, string>> GetAllClientsAsKeyValuePairs();
    }
}
