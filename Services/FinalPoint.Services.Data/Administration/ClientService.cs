using System;
using System.Collections.Generic;
using System.Linq;
using FinalPoint.Data.Common.Models;
using FinalPoint.Data.Common.Repositories;
using FinalPoint.Data.Models;

namespace FinalPoint.Services.Data.Administration
{
    public class ClientService : IClientService
    {
        private readonly IDeletableEntityRepository<Client> clientRep;

        public ClientService(IDeletableEntityRepository<Client> clientRep)
        {
            this.clientRep = clientRep;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllClientsAsKeyValuePairs()
        {
            return this.clientRep
                .All()
                .Select(x => new
                {
                    ClientInfo = x.FirstName + " " + x.LastName + " - " + "\n" + x.PhoneNumber,
                    Id = x.Id
                })
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.ClientInfo));
        }
    }
}
