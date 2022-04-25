using System;
using System.Collections.Generic;
using FinalPoint.Data.Models;

namespace FinalPoint.Web.Business.Interfaces
{
	public interface IThemeService
	{
		bool UpdateTheme();

		string GetOfficeTheme();

		List<Theme> GetAllThemes();

		IEnumerable<KeyValuePair<string, string>> GetAllThemesAsKeyValuePair();

		Theme GetThemeById(int id);
	}
}