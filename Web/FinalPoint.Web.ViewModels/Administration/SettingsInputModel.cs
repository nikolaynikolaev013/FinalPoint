using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinalPoint.Web.ViewModels.DTOs;

namespace FinalPoint.Web.ViewModels.Administration
{
	public class SettingsInputModel
	{
		public SettingsInputModel()
		{
			this.AvailableThemes = new List<KeyValuePair<string, string>>();
		}

		//public ICollection<AdministrationThemeDto> AvailableThemes { get; set; }

		public ICollection<KeyValuePair<string, string>> AvailableThemes { get; set; }

		[Display(Name = "Моля изберете тема: ")]
		public int SelectedThemeId { get; set; }
    }
}