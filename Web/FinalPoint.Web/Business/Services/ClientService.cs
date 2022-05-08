﻿namespace FinalPoint.Web.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using FinalPoint.Common;
    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.Business.Interfaces;
    using FinalPoint.Web.ViewModels;

    public class ClientService : IClientService
    {
        private readonly IDeletableEntityRepository<Client> clientRep;
        private readonly IMapper mapper;

        public ClientService(
            IDeletableEntityRepository<Client> clientRep,
            IMapper mapper)
        {
            this.clientRep = clientRep;
            this.mapper = mapper;
        }

        public async Task<Client> CreateAsync(Client input)
        {

            if (input.EmailAddress != null)
            {
                var existingClient = this.clientRep
                    .AllAsNoTracking()
                    .Where(x => x.EmailAddress == input.EmailAddress)
                    .FirstOrDefault();

                if (existingClient != null)
                {
                    return existingClient;
                }
            }

            var newClient = input;
            await this.clientRep.AddAsync(newClient);
            await this.clientRep.SaveChangesAsync();

            return newClient;
        }

        public async Task<Result> EditClientInfo(Client input)
        {
            if (string.IsNullOrEmpty(input.LastName)
                || string.IsNullOrEmpty(input.FirstName)
                || string.IsNullOrEmpty(input.PhoneNumber))
            {
                return new Result() { Success = false, Message = GlobalErrorMessages.PleaseFillAllFields };
            }

            var existingClient = this.clientRep
                .All()
                .AsEnumerable()
                .FirstOrDefault(x => x.Id == input.Id);

            if (existingClient == null)
            {
                return new Result() { Success = false };
            }

            existingClient = this.mapper.Map<Client>(input);

            await this.clientRep.SaveChangesAsync();
            return new Result() { Success = true };
        }

        public Client GetClientById(int id)
        {
            return this.clientRep
                    .All()
                    .AsEnumerable()
                    .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllClientsAsKeyValuePairs()
        {
            return this.clientRep
                .AllAsNoTracking()
                .Select(x => new
                {
                    ClientInfo = x.FirstName + " " + x.LastName + " - " + "\n" + x.PhoneNumber,
                    x.Id,
                })
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.ClientInfo));
        }
    }
}
