namespace FinalPoint.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.Administration;

    public class OfficeService : IOfficeService
    {
        private readonly IDeletableEntityRepository<FinalPoint.Data.Models.Office> officeRep;

        public OfficeService(IDeletableEntityRepository<FinalPoint.Data.Models.Office> officeRep)
        {
            this.officeRep = officeRep;
        }

        public async Task CreateAsync(AddOfficeInputModel model)
        {
            var newOffice = new FinalPoint.Data.Models.Office();
            newOffice.PostCode = model.PostCode;
            newOffice.Name = model.Name;
            newOffice.OfficeType = model.OfficeType;
            //TODO OwnerId
            newOffice.CityId = model.CityId;
            newOffice.Address = model.Address;
            newOffice.ResponsibleSortingCenterId = model.ResponsibleSortingCenter;
            newOffice.OwnerId = model.OwnerId;

            await this.officeRep.AddAsync(newOffice);
            await this.officeRep.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GeAllOfficesAsKeyValuePairs()
        {
            return this.officeRep
                   .All()
                   .Select(x => new
                   {
                       x.Id,
                       x.Name,
                   }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllSortingCentersAsKeyValuePairs()
        {
            return this.officeRep
                .All()
                .Where(x => x.OfficeType == OfficeType.SortingCenter)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
