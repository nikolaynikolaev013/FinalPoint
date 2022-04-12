using System;
using System.Collections.Generic;
using System.Linq;
using FinalPoint.Data.Common.Repositories;

namespace FinalPoint.Services.Data.Theme
{
	public class ThemeService : IThemeService
	{
        private readonly IDeletableEntityRepository<FinalPoint.Data.Models.Theme> themesRep;

        public ThemeService(
            IDeletableEntityRepository<FinalPoint.Data.Models.Theme> themesRep)
		{
            this.themesRep = themesRep;
        }

        public List<FinalPoint.Data.Models.Theme> GetAllThemes()
        {
            return this.themesRep
                .AllAsNoTracking()
                .ToList();
        }

        public FinalPoint.Data.Models.Theme GetThemeById(int id)
        {
            return this.themesRep
                .All()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}

