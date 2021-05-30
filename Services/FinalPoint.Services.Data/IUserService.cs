﻿namespace FinalPoint.Services.Data
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

        ApplicationUser GetUserById(string userId);

        ApplicationUser GetUserByPersonalId(int userPersonalId);
    }
}
