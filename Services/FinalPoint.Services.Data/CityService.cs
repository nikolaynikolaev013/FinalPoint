namespace FinalPoint.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.ViewModels.Administration;

    public class CityService : ICityService
    {
        private readonly IDeletableEntityRepository<City> citiesRep;

        public CityService(IDeletableEntityRepository<City> citiesRep)
        {
            this.citiesRep = citiesRep;
        }

        public async Task<int> CreateNewCity(AddCityInputModel model)
        {
            var newCity = new City();
            newCity.Name = model.Name;
            newCity.Postcode = (int)model.Postcode;

            await this.citiesRep.AddAsync(newCity);
            await this.citiesRep.SaveChangesAsync();
            return newCity.Id;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCitiesAsKeyValuePairs()
        {
            return this.citiesRep.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
