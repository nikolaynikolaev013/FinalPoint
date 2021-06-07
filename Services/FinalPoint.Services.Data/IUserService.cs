namespace FinalPoint.Services.Data
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Data.Models;

    public interface IUserService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllUsersAsKeyValuePair();

        IEnumerable<KeyValuePair<string, string>> GetAllUsersWithoutCurrentAsKeyValuePair(string currUserId);

        ICollection<int> GetAllPersonalIds();

        Task<ApplicationUser> RemoveUser(int userPersonalId);

        ICollection<string> GetAllUsers();

        ApplicationUser GetUserById(string userId);

        ApplicationUser GetUserByPersonalId(int? userPersonalId);

        ApplicationUser GetUserByClaimsPrincipal(ClaimsPrincipal user);

        Task SetUserNewWorkOfficeByUserPersonalId(int personalId, int newWorkOfficeId);
    }
}
