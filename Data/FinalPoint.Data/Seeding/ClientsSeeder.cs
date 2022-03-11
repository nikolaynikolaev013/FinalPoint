using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinalPoint.Data.Seeding
{
    public class ClientsSeeder : ISeeder
    {
        public ClientsSeeder()
        {
        }


        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            for (int i = 0; i < SeedingConstants.Clients.Length; i++)
            {
                if (dbContext.Clients.FirstOrDefault(x => x.PhoneNumber == SeedingConstants.Clients[i].PhoneNumber) != null)
                {
                    continue;
                }

                await dbContext.Clients.AddAsync(SeedingConstants.Clients[i]);
            }
        }
    }
}
