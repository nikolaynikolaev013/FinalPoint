namespace FinalPoint.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FinalPoint.Data.Models;

    public class OfficesSeeder : ISeeder
    {
        public OfficesSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            for (int i = 0; i < SeedingConstants.Offices.Count(); i++)
            {
                var office = SeedingConstants.Offices[i];

                if (dbContext.Offices.FirstOrDefault(x => x.PostCode == office.PostCode) != null)
                {
                    continue;
                }

                office.OfficeType = Models.Enums.OfficeType.Office;

                office.ResponsibleSortingCenterId = dbContext
                        .Offices
                        .FirstOrDefault(x => x.PostCode == office.IgnoredResponsibleSortingCenterPostCode)?
                        .Id;

                office.CityId = dbContext.Cities.FirstOrDefault(x => x.Postcode == office.IgnoredCityPostCode).Id;

                await dbContext.Offices.AddAsync(office);
            }
        }
    }
}
