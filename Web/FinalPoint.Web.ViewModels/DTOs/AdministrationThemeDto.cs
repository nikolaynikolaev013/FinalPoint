using System;
using AutoMapper;
using FinalPoint.Data.Models;
using FinalPoint.Services.Mapping;

namespace FinalPoint.Web.ViewModels.DTOs
{
	public class AdministrationThemeDto : IMapFrom<Theme>, IMapTo<AdministrationThemeDto>, IHaveCustomMappings
	{
		public AdministrationThemeDto()
		{
		}

        public string Name { get; set; }

        public string ImgSrc { get; set; }

        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Theme, AdministrationThemeDto>()
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Name))
                .ForMember(x => x.ImgSrc, x => x.MapFrom(y => y.ImgSrc))
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}

