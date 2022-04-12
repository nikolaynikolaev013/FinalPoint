namespace FinalPoint.Services.Data.Office
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Services.Data.City;
    using FinalPoint.Services.Data.Theme;
    using FinalPoint.Services.Data.User;
    using FinalPoint.Web.ViewModels.Administration;
    using Microsoft.EntityFrameworkCore;

    public class OfficeService : IOfficeService
    {
        private readonly IDeletableEntityRepository<Office> officeRep;
        private readonly ICityService cityService;
        private readonly IUserService userService;
        private readonly IThemeService themeService;
        private readonly IMapper mapper;

        public OfficeService(
            IDeletableEntityRepository<Office> officeRep,
            ICityService cityService,
            IUserService userService,
            IThemeService themeService,
            IMapper mapper)
        {
            this.officeRep = officeRep;
            this.cityService = cityService;
            this.userService = userService;
            this.themeService = themeService;
            this.mapper = mapper;
        }

        public async Task<Office> CreateAsync(AddOfficeInputModel model)
        {
            if (model.CityInputModel.Name != null
                && model.CityInputModel.Postcode != null)
            {
                var newCityId = await this.cityService.CreateAsync(model.CityInputModel);
                model.CityId = newCityId;
            }

            var newOffice = this.mapper.Map<Office>(model);

            var owner = this.userService.GetUserByPersonalId(model.OwnerId);
            newOffice.Owner = owner;

            if (model.OfficeType == OfficeType.Office)
            {
                newOffice.ResponsibleSortingCenterId = model.ResponsibleSortingCenter;
            }
            else
            {
                newOffice.ResponsibleSortingCenterId = null;
            }

            await this.officeRep.AddAsync(newOffice);
            await this.officeRep.SaveChangesAsync();

            if (newOffice.Owner != null && newOffice.Owner.Id != null)
            {
                await this.userService
                        .SetUserNewWorkOfficeByUserPersonalId(newOffice.Owner.PersonalId, newOffice.Id);
            }

            return newOffice;
        }

        public async Task<Office> Remove(int officeId)
        {
            var officeToDelete = this.GetOfficeById(officeId);

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
            await this.cityService.DeleteIfNoOfficeAssociatedToIt(officeToDelete.CityId, officeId);

            return officeToDelete;
        }

        public async Task<bool> ChangeOfficeTheme(int officeId, int themeId)
        {
            var theme = this.themeService.GetThemeById(themeId);
            var office = this.GetOfficeById(officeId);

            if (theme == null || office == null)
            {
                return false;
            }

            office.Theme = theme;
            await this.officeRep.SaveChangesAsync();

            return true;
        }

        public string GetOfficeAsStringById(int officeId)
        {
            var office = this.officeRep
                        .All()
                        .Where(x => x.Id == officeId)
                        .Select(x => new { cityName = x.City.Name,x.Name, x.PostCode })
                        .FirstOrDefault();

            return $"{office.cityName} - {office.Name} - {office.PostCode}";
        }

        public Office GetOfficeById(int officeId)
        {
            return this.officeRep
                    .All()
                    .Where(x => x.Id == officeId)
                    .Include(x => x.ResponsibleSortingCenter)
                    .FirstOrDefault();
        }

        public Office GetVirtualOffice()
        {
            return this.officeRep
                .AllAsNoTracking()
                .Where(x => x.Name.ToLower() == "виртуален")
                .FirstOrDefault();
        }

        public Office GetOfficeByPostcode(int officePostcode)
        {
            return this.officeRep
                    .All()
                    .Where(x => x.PostCode == officePostcode)
                    .FirstOrDefault();
        }

        public HashSet<int> GetAllOfficeIdsInRangeOfSortingCenterId(int sortingCenterId)
        {
            return this.officeRep
                    .All()
                    .Where(x => x.ResponsibleSortingCenterId == sortingCenterId)
                    .Select(x => x.Id)
                    .ToHashSet();
        }

        public HashSet<string> GetAllOfficesWithoutVirtual()
        {
            var virtualOffice = this.GetVirtualOffice();

            var offices = this.officeRep
                    .AllAsNoTracking()
                    .Include(x => x.Owner)
                    .Include(x => x.ResponsibleSortingCenter)
                    .Include(x=>x.City)
                    .Where(x => x != virtualOffice)
                    .ToHashSet();

            var result = new HashSet<string>();

            foreach (var office in offices)
            {
                if (office.OfficeType == OfficeType.Office)
                {
                    result.Add($"Офис: {office.City.Name} {office.Name} ({office.PostCode}) - обслужващо РЦ: {office.ResponsibleSortingCenter.Name} ({office.ResponsibleSortingCenter.PostCode}) - собственик: {office.Owner?.FullName} ({office.Owner?.PersonalId})");
                }
                else
                {
                    result.Add($"Разпределителен център: {office.Name} ({office.PostCode}) - собственик: {office.Owner?.FullName} ({office.Owner?.PersonalId})");
                }
            }

            return result;
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

        public IEnumerable<KeyValuePair<string, string>> GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(int officeIdToSkip)
        {
            var virtualOffice = this.GetVirtualOffice();

            return this.officeRep
                      .AllAsNoTracking()
                      .Where(x => x.Id != officeIdToSkip
                                && x != virtualOffice)
                      .Select(x => new
                      {
                          x.Id,
                          x.Name,
                          x.PostCode,
                          City = x.City.Name,
                      }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), $"{x.City} - {x.Name} - {x.PostCode}"));
        }

        public IEnumerable<KeyValuePair<string, string>> GetLoadUnloadOffices(Office currentOffice)
        {
            var virtualOffice = this.GetVirtualOffice();

            if (currentOffice.OfficeType == OfficeType.Office)
            {
                return this.officeRep
                      .AllAsNoTracking()
                      .Where(x => x.Id == currentOffice.ResponsibleSortingCenterId)
                      .Select(x => new
                      {
                          x.Id,
                          x.Name,
                          x.PostCode,
                          City = x.City.Name,
                      }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), $"{x.City} - {x.Name} - {x.PostCode}"));
            }
            else
            {
                return this.officeRep
                      .AllAsNoTracking()
                      .Where(x=>
                      (x.ResponsibleSortingCenterId == currentOffice.Id || x.OfficeType == OfficeType.SortingCenter)
                      && x != virtualOffice
                      && x.Id != currentOffice.Id)
                      .Select(x => new
                      {
                          x.Id,
                          x.Name,
                          x.PostCode,
                          City = x.City.Name,
                      }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), $"{x.City} - {x.Name} - {x.PostCode}"));
            }
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
    }
}
