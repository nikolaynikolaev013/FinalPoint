namespace FinalPoint.Services.Data.Administration
{
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
    }
}
