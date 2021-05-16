namespace FinalPoint.Services.Data.Misc
{
    using System.Collections.Generic;
    using System.Linq;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;

    public class GetBasicData : IGetBasicData
    {
        private readonly IDeletableEntityRepository<Office> officesRep;
        private readonly IDeletableEntityRepository<City> citiesRep;

        public GetBasicData(
            IDeletableEntityRepository<Office> officesRep,
            IDeletableEntityRepository<City> citiesRep)
        {
            this.officesRep = officesRep;
            this.citiesRep = citiesRep;
        }

        public IEnumerable<KeyValuePair<string, string>> GeAllOfficesAsKeyValuePairs()
        {
            return this.officesRep
                   .All()
                   .Select(x => new
                   {
                       x.Id,
                       x.Name,
                   }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCitiesAsKeyValuePairs()
        {
            return this.citiesRep.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllSortingCentersAsKeyValuePairs()
        {
            return this.officesRep
                .All()
                .Where(x=>x.OfficeType == OfficeType.SortingCenter)
                .Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
