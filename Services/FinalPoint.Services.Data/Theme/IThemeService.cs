using System;
using System.Collections.Generic;

namespace FinalPoint.Services.Data.Theme
{
	public interface IThemeService
	{
		List<FinalPoint.Data.Models.Theme> GetAllThemes();

		FinalPoint.Data.Models.Theme GetThemeById(int id);
	}
}

