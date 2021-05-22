namespace FinalPoint.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FinalPoint.Data.Models;

    public interface IUserService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllUsersAsKeyValuePair();

        IEnumerable<KeyValuePair<string, string>> GetAllUsersWithoutCurrentAsKeyValuePair(ClaimsPrincipal currUser);

        Task<IEnumerable<int>> GetAllPersonalIds();

        Task<ApplicationUser> RemoveUser(int userPersonalId);
    }
}
