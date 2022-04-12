using System;
using System.Collections.Generic;
using FinalPoint.Data.Common.Models;

namespace FinalPoint.Data.Models
{
	public class Theme : BaseDeletableModel<int>
	{
		public Theme()
		{
		}

		public string Name { get; set; }

		public string ImgSrc { get; set; }

        public virtual ICollection<ApplicationUser> Offices { get; set; }
    }
}

