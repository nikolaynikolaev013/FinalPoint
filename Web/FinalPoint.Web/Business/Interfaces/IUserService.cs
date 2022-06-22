namespace FinalPoint.Web.Business.Interfaces
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

        Task<ApplicationUser> RemoveUserAsync(int userPersonalId);

        ICollection<string> GetAllUsers();

        ApplicationUser GetUserById(string userId);

        ApplicationUser GetUserByPersonalId(int? userPersonalId);

        ApplicationUser GetUserByClaimsPrincipal(ClaimsPrincipal user);

        Task SetUserNewWorkOfficeByUserPersonalIdAsync(int personalId, int newWorkOfficeId);

        int GetUserOfficeByClaimsPrincipal(ClaimsPrincipal user);

        Task<bool> SetUserNewWorkOfficeByUserIdAsync(string userId, int newWorkOfficeId);
    }
}
