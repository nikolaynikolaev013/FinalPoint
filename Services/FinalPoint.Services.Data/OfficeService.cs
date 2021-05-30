namespace FinalPoint.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.Administration;

    public class OfficeService : IOfficeService
    {
        private readonly IDeletableEntityRepository<Office> officeRep;
        private readonly ICityService cityService;
        private readonly IUserService userService;

        public OfficeService(
            IDeletableEntityRepository<Office> officeRep,
            ICityService cityService,
            IUserService userService)
        {
            this.officeRep = officeRep;
            this.cityService = cityService;
            this.userService = userService;
        }

        public async Task CreateAsync(AddOfficeInputModel model)
        {
            if (model.CityInputModel.Name != null
                && model.CityInputModel.Postcode != null)
            {
                var newCityId = await this.cityService.CreateNewCity(model.CityInputModel);
                model.CityId = newCityId;
            }

            var newOffice = new FinalPoint.Data.Models.Office();
            newOffice.PostCode = model.PostCode;
            newOffice.Name = model.Name;
            newOffice.OfficeType = model.OfficeType;
            newOffice.CityId = model.CityId;
            newOffice.Address = model.Address;
            newOffice.ResponsibleSortingCenterId = model.ResponsibleSortingCenter;
            newOffice.OwnerId = model.OwnerId;

            await this.officeRep.AddAsync(newOffice);
            await this.officeRep.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GeAllOfficesAndSortingCentersAsKeyValuePairs()
        {
            return this.officeRep
                   .AllAsNoTracking()
                   .Select(x => new
                   {
                       x.Id,
                       x.Name,
                       x.PostCode,
                       City = x.City.Name,
                   }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), $"{x.City} - {x.Name} - {x.PostCode}"));
        }

        public IEnumerable<KeyValuePair<string, string>> GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(int officePostcodeToSkip)
        {
            return this.officeRep
                      .AllAsNoTracking()
                      .Where(x=>x.PostCode != officePostcodeToSkip)
                      .Select(x => new
                      {
                          x.Id,
                          x.Name,
                          x.PostCode,
                          City = x.City.Name,
                      }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), $"{x.City} - {x.Name} - {x.PostCode}"));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllSortingCentersAsKeyValuePairs()
        {
            return this.officeRep
                .AllAsNoTracking()
                .Where(x => x.OfficeType == OfficeType.SortingCenter)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public Office GetOffice(int officeId)
        {
            return this.officeRep
                    .All()
                    .Where(x => x.Id == officeId)
                    .FirstOrDefault();
        }

        public async Task<Office> Remove(int officeId)
        {
            var officeToDelete = this.GetOffice(officeId);

            var officeEmployees = this.officeRep
                .All()
                .Where(x => x.Id == officeId)
                .Select(x => new { x.Employees })
                .FirstOrDefault();

            foreach (var employee in officeEmployees.Employees)
            {
                await this.userService.RemoveUser(employee.PersonalId);
            }

            this.officeRep.Delete(officeToDelete);
            await this.officeRep.SaveChangesAsync();
            return officeToDelete;
        }
    }
}
