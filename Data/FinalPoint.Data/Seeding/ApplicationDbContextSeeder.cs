namespace FinalPoint.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(ApplicationDbContextSeeder));
            var dbService = serviceProvider.GetService<IDbService>();

            var seeders = new List<ISeeder>
                          {
                              new RolesSeeder(),
                              new SettingsSeeder(),
                              new ClientsSeeder(),
                              new CitiesSeeder(),
                              new SortingCentersSeeder(),
                              new OfficesSeeder(),
                              new UsersSeeder(),
                              new OfficeOwnersSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }

            if (!await dbContext.Parcels.AnyAsync())
            {
                try
                {
                    var query = new SqlCommand("DBCC CHECKIDENT (Parcels, RESEED, 112426)");
                    dbService.RunProcedure(query);
                }
                catch (Exception)
                {
                }
            }

            if (!await dbContext.Protocols.AnyAsync())
            {
                try
                {
                    var query = new SqlCommand("DBCC CHECKIDENT (Protocols, RESEED, 12392)");
                    dbService.RunProcedure(query);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
