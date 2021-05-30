using System;
using System.Linq;
using System.Threading.Tasks;
using FinalPoint.Data.Models;

namespace FinalPoint.Data.Seeding
{
    public class OfficesSeeder:ISeeder
    {
        public OfficesSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Offices.Any())
            {
                return;
            }
            return;
            // await dbContext.Offices.AddAsync(new Office() { PostCode = 9000, Name = "Варна НЛЦ", Address = "бул. Република 59", CityId = 1 });
        }
    }
}
