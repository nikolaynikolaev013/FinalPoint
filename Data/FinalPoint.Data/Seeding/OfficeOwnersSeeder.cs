using System;
using System.Linq;
using System.Threading.Tasks;
using FinalPoint.Common;
using FinalPoint.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FinalPoint.Data.Seeding
{
    public class OfficeOwnersSeeder : ISeeder
    {
        public OfficeOwnersSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            for (int i = 0; i < SeedingConstants.Users.Count(); i++)
            {
                var user = dbContext
                        .Users
                        .FirstOrDefault(x => x.PersonalId == SeedingConstants.Users[i].PersonalId);

                if (user == null)
                {
                    continue;
                }

                // if that's the owner of the company - all of the sorting centers are his
                if (await userManager.IsInRoleAsync(user, GlobalConstants.OwnerRoleName))
                {
                    foreach (var sortingCenter in SeedingConstants.SortingCenters)
                    {
                        var office = dbContext
                                .Offices
                                .FirstOrDefault(x => x.PostCode == sortingCenter.PostCode);

                        if (office != null && office.OwnerId == null)
                        {
                            office.OwnerId = user.Id;
                        }
                    }

                    continue;
                }

                var userOffices = SeedingConstants.Users[i].IgnoredOwnedOfficesPostcodes;

                foreach (var ownedOfficePostcode in userOffices)
                {
                    var office = dbContext
                            .Offices
                            .FirstOrDefault(x => x.PostCode == ownedOfficePostcode);

                    if (office != null && office.OwnerId == null)
                    {
                        office.OwnerId = user.Id;
                    }
                }
            }
        }
    }
}
