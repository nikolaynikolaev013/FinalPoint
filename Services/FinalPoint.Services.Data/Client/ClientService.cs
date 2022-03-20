namespace FinalPoint.Services.Data.Client
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.AddDispose;

    public class ClientService : IClientService
    {
        private readonly IDeletableEntityRepository<Client> clientRep;

        public ClientService(IDeletableEntityRepository<Client> clientRep)
        {
            this.clientRep = clientRep;
        }

        public async Task<Client> CreateAsync(AddClientInputModel input)
        {
            var newClient = new Client()
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Address = input.Address,
                EmailAddress = input.Email,
                PhoneNumber = input.PhoneNumber,
            };

            await this.clientRep.AddAsync(newClient);
            await this.clientRep.SaveChangesAsync();

            return newClient;
        }

        public Client GetClientById(int id)
        {
            return this.clientRep
                    .All()
                    .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllClientsAsKeyValuePairs()
        {
            return this.clientRep
                .AllAsNoTracking()
                .Select(x => new
                {
                    ClientInfo = x.FirstName + " " + x.LastName + " - " + "\n" + x.PhoneNumber,
                    x.Id
                })
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.ClientInfo));
        }
    }
}
