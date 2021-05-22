namespace FinalPoint.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRep;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(IDeletableEntityRepository<ApplicationUser> usersRep,
            UserManager<ApplicationUser> userManager)
        {
            this.usersRep = usersRep;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<int>> GetAllPersonalIds()
        {
            return this.usersRep
                .AllAsNoTracking()
                .Select(x => x.PersonalId)
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllUsersAsKeyValuePair()
        {
            return this.usersRep
                   .AllAsNoTracking()
                   .OrderBy(x => x.PersonalId)
                   .Select(x => new
                   {
                       x.FullName,
                       x.PersonalId,
                   }).ToList()
                   .Select(x => new KeyValuePair<string, string>(x.PersonalId.ToString(), x.FullName + " - " + x.PersonalId.ToString()));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllUsersWithoutCurrentAsKeyValuePair(ClaimsPrincipal currUser)
        {
            var currUserPersonalId = this.userManager.GetUserAsync(currUser).Result.PersonalId;

            return this.usersRep
                   .AllAsNoTracking()
                   .Where(x => x.PersonalId != currUserPersonalId)
                   .OrderBy(x => x.PersonalId)
                   .Select(x => new
                   {
                       x.FullName,
                       x.PersonalId,
                   }).ToList()
                   .Select(x => new KeyValuePair<string, string>(x.PersonalId.ToString(), x.FullName + " - " + x.PersonalId.ToString()));
        }

        public async Task<ApplicationUser> RemoveUser(int userPersonalId)
        {
            var userToDelete = this.userManager.Users.FirstOrDefault(x => x.PersonalId == userPersonalId);

            this.usersRep.Delete(userToDelete);
            await this.usersRep.SaveChangesAsync();
            return userToDelete;
        }
    }
}
