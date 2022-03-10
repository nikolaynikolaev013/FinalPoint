﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FinalPoint.Common;
using FinalPoint.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FinalPoint.Data.Seeding
{
    public class UsersSeeder : ISeeder
    {
        public UsersSeeder()
        {
        }

        public async Task SeedAsync(
            ApplicationDbContext dbContext,
            IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            for (int i = 0; i < SeedingConstants.Users.Count(); i++)
            {
                var user = SeedingConstants.Users[i];

                if (dbContext.Users.FirstOrDefault(x => x.PersonalId == user.PersonalId) != null)
                {
                    continue;
                }

                var isOwner = user.IgnoredRole == GlobalConstants.OwnerRoleName;

                user.WorkOfficeId = isOwner ?

                    dbContext.Offices
                        .FirstOrDefault(x => x.PostCode == SeedingConstants.VirtualSortingCenterPostCode).Id

                    : dbContext.Offices
                        .FirstOrDefault(x => x.PostCode == user.IgnoredOwnedOfficesPostcodes.FirstOrDefault()).Id;

                await userManager.CreateAsync(user, SeedingConstants.UniversalUserPassword);
                await userManager.AddToRoleAsync(user, user.IgnoredRole);
            }
        }
    }
}
