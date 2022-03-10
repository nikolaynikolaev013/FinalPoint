using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinalPoint.Data.Seeding
{
    public class SortingCentersSeeder : ISeeder
    {
        public SortingCentersSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            for (int i = 0; i < SeedingConstants.SortingCenters.Count(); i++)
            {
                var office = SeedingConstants.SortingCenters[i];

                if (dbContext.Offices.FirstOrDefault(x => x.PostCode == office.PostCode) != null)
                {
                    continue;
                }

                office.OfficeType = Models.Enums.OfficeType.SortingCenter;

                office.ResponsibleSortingCenterId = dbContext
                        .Offices
                        .FirstOrDefault(x => x.PostCode == SeedingConstants.VirtualSortingCenterPostCode)?
                        .Id;

                office.CityId = dbContext.Cities.FirstOrDefault(x => x.Postcode == office.IgnoredCityPostCode).Id;

                await dbContext.Offices.AddAsync(office);

                if (office.PostCode == SeedingConstants.VirtualSortingCenterPostCode)
                {
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
