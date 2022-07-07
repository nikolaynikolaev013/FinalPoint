using System;
using AutoMapper;
using FinalPoint.Services.Mapping;
using FinalPoint.Web.ViewModels.AddDispose;

namespace FinalPoint.Web.ViewModels.DTOs
{
	public class CalculateDeliveryPriceDto : IMapFrom<AddParcelInputModel>, IHaveCustomMappings
	{
		public CalculateDeliveryPriceDto()
		{
		}

        public decimal Height { get; set; }

        public decimal Width { get; set; }

        public decimal Weight { get; set; }

        public decimal Length { get; set; }

        public bool IsFragile { get; set; }

        public bool DontPaletize { get; set; }

        public bool HasCashOnDelivery { get; set; }

        public decimal CashOnDeliveryPrice { get; set; }

        public int NumberOfParts { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AddParcelInputModel, CalculateDeliveryPriceDto>()
                .ForMember(x => x.Weight, x => x.MapFrom(y => y.Weight))
                .ForMember(x => x.HasCashOnDelivery, x => x.MapFrom(y => y.HasCashOnDelivery))
                .ForMember(x => x.IsFragile, x => x.MapFrom(y => y.IsFragile))
                .ForMember(x => x.DontPaletize, x => x.MapFrom(y => y.DontPaletize))
                .ForMember(x => x.CashOnDeliveryPrice, x => x.MapFrom(y => y.CashOnDeliveryPrice))
                .ForMember(x => x.NumberOfParts, x => x.MapFrom(y => y.NumberOfParts))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}

