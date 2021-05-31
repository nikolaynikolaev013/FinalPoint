namespace FinalPoint.Services.Data
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

        public UserService(
            IDeletableEntityRepository<ApplicationUser> usersRep,
            UserManager<ApplicationUser> userManager)
        {
            this.usersRep = usersRep;
            this.userManager = userManager;
        }

        public ICollection<int> GetAllPersonalIds()
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

        public IEnumerable<KeyValuePair<string, string>> GetAllUsersWithoutCurrentAsKeyValuePair(string currUserId)
        {
            return this.usersRep
                            .All()
                            .Where(x => x.Id != currUserId)
                            .OrderBy(x => x.PersonalId)
                            .Select(x => new
                            {
                                x.FullName,
                                x.PersonalId,
                            }).ToList()
                   .Select(x => new KeyValuePair<string, string>(x.PersonalId.ToString(), x.FullName + " - " + x.PersonalId.ToString())); ;
        }

        public async Task SetUserNewWorkOfficeByUserPersonalId(int personalId, int newWorkOfficeId)
        {
            var user = this.usersRep
                        .All()
                        .Where(x => x.PersonalId == personalId)
                        .FirstOrDefault();

            if (user != null
                && user.WorkOfficeId == 13
                && user.PersonalId != 10001)
            {
                user.WorkOfficeId = newWorkOfficeId;
                await this.usersRep.SaveChangesAsync();
            }
        }

        public ApplicationUser GetUserById(string userId)
        {
            return this.usersRep
                    .All()
                    .Where(x => x.Id == userId)
                    .FirstOrDefault();
        }

        public ApplicationUser GetUserByClaimsPrincipal(ClaimsPrincipal user)
        {
            return this.usersRep
                    .All()
                    .Where(x => x.Id == user.FindFirst(ClaimTypes.NameIdentifier).Value)
                    .FirstOrDefault();
        }

        public ApplicationUser GetUserByPersonalId(int userPersonalId)
        {
            return this.usersRep
                    .All()
                    .Where(x => x.PersonalId == userPersonalId)
                    .FirstOrDefault();
        }

        public async Task<ApplicationUser> RemoveUser(int userPersonalId)
        {
            var userToDelete = this.GetUserByPersonalId(userPersonalId);

            if (userToDelete != null)
            {
                this.usersRep.Delete(userToDelete);
                await this.usersRep.SaveChangesAsync();
            }
            return userToDelete;
        }
    }
}
