using System;
using System.Collections.Generic;
using System.Linq;
using FinalPoint.Data.Models;
using FinalPoint.Data.Models.Enums;

namespace FinalPoint.Services.Data.Tests
{
    public class FakeData
    {
        public FakeData()
        {
            this.SeedCities();
            this.SeedClients();
            this.SeedOffices();
            this.SeedApplicationUsers();
        }

        public List<City> Cities { get; private set; }

        public List<Client> Clients { get; private set; }

        public List<Office> Offices { get; private set; } = new List<Office>();

        public List<ApplicationUser> ApplicationUsers { get; private set; }

        private void SeedCities()
        {
            this.Cities = new List<City> {
            new City() { Name = "Варна", Postcode = 9000, },
            };
        }

        private void SeedClients()
        {
            this.Clients = new List<Client>()
            {
                new Client() { Address = "Test address", FirstName = "Dimitar", LastName = "Dimitrov", PhoneNumber = "0877503555", },
            };
        }

        private void SeedOffices()
        {
            // Sorting Centers
            this.Offices.AddRange(new Office[] {
            new Office() { Name = "Варна НЛЦ", City = this.Cities[0], PostCode = 9000, OfficeType = OfficeType.SortingCenter,},
            });

            //Offices
            this.Offices.AddRange(new Office[] {
            new Office() { Name = "Чаталджа", City = this.Cities[0], PostCode = 9008, OfficeType = OfficeType.Office,
                ResponsibleSortingCenter = this.Offices[0], },

            new Office() { Name = "Технически университет", City = this.Cities[0], PostCode = 9009, OfficeType = OfficeType.Office, ResponsibleSortingCenter = this.Offices[0], },

            new Office() { Name = "Виртуален", City = this.Cities[0], PostCode = 909090, OfficeType = OfficeType.SortingCenter, },
            });
        }

        private void SeedApplicationUsers()
        {
            this.ApplicationUsers = new List<ApplicationUser>()
            {
                new ApplicationUser() { Email = "nikolainikolaev013@gmail.com", FirstName = "Nikolay", LastName = "Nikolaev", Id = "1", MiddleName = "Dimitrov" },
            };
        }


    }
}
