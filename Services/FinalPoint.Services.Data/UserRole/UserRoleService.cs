namespace FinalPoint.Services.Data.UserRole
{
    using System.Collections.Generic;
    using System.Linq;

    using FinalPoint.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class UserRoleService : IUserRoleService
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserRoleService(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllUserRolesAsKeyValuePairs()
        {
            var roles = this.roleManager
                    .Roles
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.Name, this.TranslateRole(x.Name)));

            return roles;
        }

        private string TranslateRole(string role)
        {
            var translatedRole = string.Empty;

            switch (role)
            {
                case "Administrator":
                    translatedRole = "Администратор";
                    break;
                case "OfficeAdmin":
                    translatedRole = "Администратор на офис";
                    break;
                case "SortingCenterAdmin":
                    translatedRole = "Администратор на разпределителен център";
                    break;
                case "Owner":
                    translatedRole = "Собственик";
                    break;
                case "SortingCenterManager":
                    translatedRole = "Управител на разпределителен център";
                    break;
                case "OfficeManager":
                    translatedRole = "Управител на офис";
                    break;
                case "Driver":
                    translatedRole = "Шофьор";
                    break;
                case "OfficeOwner":
                    translatedRole = "Собственик на офис";
                    break;
            }

            return translatedRole;
        }
    }
}
