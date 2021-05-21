namespace FinalPoint.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRep;

        public UserService(IDeletableEntityRepository<ApplicationUser> usersRep)
        {
            this.usersRep = usersRep;
        }

        public async Task<IEnumerable<int>> GetAllPersonalIds()
        {
            return usersRep
                .AllAsNoTracking()
                .Select(x => x.PersonalId)
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllUsersAsKeyValuePair()
        {
            return this.usersRep
                .AllAsNoTracking()
                .OrderBy(x=>x.PersonalId)
                .Select(x => new
                {
                    x.FullName,
                    x.PersonalId,
                }).ToList()
                .Select(x => new KeyValuePair<string, string>(x.PersonalId.ToString(), x.FullName + " - " + x.PersonalId.ToString()));
        }
    }
}
