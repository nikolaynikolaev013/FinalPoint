using System;
using System.Collections.Generic;
using FinalPoint.Web.ViewModels.DTOs;

namespace FinalPoint.Web.ViewModels.Administration
{
	public class SettingsInputModel
	{
		public SettingsInputModel()
		{
			this.AvailableThemes = new List<AdministrationThemeDto>();
		}

        public ICollection<AdministrationThemeDto> AvailableThemes { get; set; }

        public int SelectedThemeId { get; set; }
    }
}