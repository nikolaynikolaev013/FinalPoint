using System;
using System.Collections.Generic;

namespace FinalPoint.Web.Business.Interfaces
{
	public interface IThemeService
	{
		List<FinalPoint.Data.Models.Theme> GetAllThemes();

		FinalPoint.Data.Models.Theme GetThemeById(int id);
	}
}