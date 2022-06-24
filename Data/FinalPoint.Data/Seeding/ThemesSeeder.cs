using System;
using System.Linq;
using System.Threading.Tasks;
using FinalPoint.Data.Models;

namespace FinalPoint.Data.Seeding
{
	public class ThemesSeeder : ISeeder
	{
		public ThemesSeeder()
		{
		}

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            for (int i = 0; i < SeedingConstants.Themes.Length; i++)
            {
                var themeName = SeedingConstants.Themes[i];

                if (dbContext.Themes.FirstOrDefault(x => x.Name == themeName) != null)
                {
                    continue;
                }

                var newThemeToInsert = new Theme() { Name = themeName };
                await dbContext.Themes.AddAsync(newThemeToInsert);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

