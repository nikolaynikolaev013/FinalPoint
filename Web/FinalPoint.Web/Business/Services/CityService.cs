namespace FinalPoint.Web.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.Business.Interfaces;
    using FinalPoint.Web.ViewModels.Administration;
    using Microsoft.EntityFrameworkCore;

    public class CityService : ICityService
    {
        private readonly IDeletableEntityRepository<City> citiesRep;

        public CityService(IDeletableEntityRepository<City> citiesRep) => this.citiesRep = citiesRep;

        public async Task<int> CreateAsync(AddCityInputModel model)
        {
            var newCity = new City();
            newCity.Name = model.Name;
            newCity.Postcode = (int)model.Postcode;

            await this.citiesRep.AddAsync(newCity);
            await this.citiesRep.SaveChangesAsync();
            return newCity.Id;
        }

        public async Task<bool> DeleteIfNoOfficeAssociatedToIt(int cityId, int officeOffsetId)
        {
            var city = this.citiesRep
                .All()
                .Include(x => x.Offices.Where(x => x.IsDeleted == false))
                .FirstOrDefault(x => x.Id == cityId);

            if (city.Offices?.Count == 0
                    || (city.Offices?.Count == 1 && city.Offices?.FirstOrDefault().Id == officeOffsetId))
            {
                this.citiesRep.Delete(city);
                await this.citiesRep.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCitiesAsKeyValuePairs()
        {
            return this.citiesRep
                .AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
