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

        public List<FinalPoint.Data.Models.City> Cities { get; private set; }

        public List<FinalPoint.Data.Models.Client> Clients { get; private set; }

        public List<FinalPoint.Data.Models.Office> Offices { get; private set; } = new List<FinalPoint.Data.Models.Office>();

        public List<FinalPoint.Data.Models.ApplicationUser> ApplicationUsers { get; private set; }

        private void SeedCities()
        {
            this.Cities = new List<FinalPoint.Data.Models.City> {
            new FinalPoint.Data.Models.City() { Name = "Варна", Postcode = 9000, },
            };
        }

        private void SeedClients()
        {
            this.Clients = new List<FinalPoint.Data.Models.Client>()
            {
                new FinalPoint.Data.Models.Client() { Address = "Test address", FirstName = "Dimitar", LastName = "Dimitrov", PhoneNumber = "0877503555", },
            };
        }

        private void SeedOffices()
        {
            // Sorting Centers
            this.Offices.AddRange(new FinalPoint.Data.Models.Office[] {
            new FinalPoint.Data.Models.Office() { Name = "Варна НЛЦ", City = this.Cities[0], PostCode = 9000, OfficeType = OfficeType.SortingCenter,},
            });

            //Offices
            this.Offices.AddRange(new FinalPoint.Data.Models.Office[] {
            new FinalPoint.Data.Models.Office() { Name = "Чаталджа", City = this.Cities[0], PostCode = 9008, OfficeType = OfficeType.Office,
                ResponsibleSortingCenter = this.Offices[0], },

            new FinalPoint.Data.Models.Office() { Name = "Технически университет", City = this.Cities[0], PostCode = 9009, OfficeType = OfficeType.Office, ResponsibleSortingCenter = this.Offices[0], },

            new FinalPoint.Data.Models.Office() { Name = "Виртуален", City = this.Cities[0], PostCode = 909090, OfficeType = OfficeType.SortingCenter, },
            });
        }

        private void SeedApplicationUsers()
        {
            this.ApplicationUsers = new List<FinalPoint.Data.Models.ApplicationUser>()
            {
                new FinalPoint.Data.Models.ApplicationUser() { Email = "nikolainikolaev013@gmail.com", FirstName = "Nikolay", LastName = "Nikolaev", Id = "1", MiddleName = "Dimitrov" },
            };
        }
    }
}
