namespace FinalPoint.Web.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Services.Mapping;
    using FinalPoint.Web.Business.Interfaces;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}
