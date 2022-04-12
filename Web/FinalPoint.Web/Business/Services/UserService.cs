﻿namespace FinalPoint.Web.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.Business.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

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
                   .Select(x => new KeyValuePair<string, string>(x.PersonalId.ToString(), x.FullName + " - " + x.PersonalId.ToString()));
        }

        public async Task SetUserNewWorkOfficeByUserPersonalId(int personalId, int newWorkOfficeId)
        {
            var user = this.GetUserByPersonalId(personalId);

            if (user != null
                && user.WorkOffice.Name.ToLower() == "виртуален"
                && !await this.userManager.IsInRoleAsync(user, "Owner"))
            {
                user.WorkOfficeId = newWorkOfficeId;
                await this.usersRep.SaveChangesAsync();
            }
        }

        public async Task<bool> ChangeUserWorkOffice(string userId, int newWorkOfficeId)
        {
            var user = this.GetUserById(userId);

            if (user != null)
            {
                user.WorkOfficeId = newWorkOfficeId;
                await this.usersRep.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public ICollection<string> GetAllUsers()
        {
            return this.usersRep
                    .AllAsNoTracking()
                    .Include(x => x.WorkOffice)
                    .ThenInclude(x => x.City)
                    .Select(x => $"{x.FullName} ({x.PersonalId}) - Офис: {x.WorkOffice.Name} ({x.WorkOffice.PostCode}) - {x.WorkOffice.City.Name}")
                    .ToHashSet();
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
                    .Include(x => x.WorkOffice)
                    .Where(x => x.Id == user.FindFirst(ClaimTypes.NameIdentifier).Value)
                    .FirstOrDefault();
        }

        public ApplicationUser GetUserByPersonalId(int? userPersonalId)
        {
            return this.usersRep
                    .All()
                    .Where(x => x.PersonalId == userPersonalId)
                    .Include(x => x.WorkOffice)
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
