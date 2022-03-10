using System;
using System.Linq;
using System.Threading.Tasks;
using FinalPoint.Data.Models;

namespace FinalPoint.Data.Seeding
{
    public class CitiesSeeder : ISeeder
    {
        public CitiesSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cities.Any())
            {
                return;
            }

            for (int i = 0; i < SeedingConstants.Cities.Count(); i++)
            {
                await dbContext.Cities.AddAsync(SeedingConstants.Cities[i]);
            }
        }
    }
}
