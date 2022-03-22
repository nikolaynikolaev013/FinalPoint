using System;
using AutoMapper;
using FinalPoint.Data.Models.Enums;
using FinalPoint.Services.Mapping;
using FinalPoint.Web.ViewModels.DTOs;

namespace FinalPoint.Web.ViewModels.ViewComponents
{
    public class CheckParcelResponseModel : IHaveCustomMappings
    {
        public CheckParcelResponseModel()
        {
        }

        public int ParcelId { get; set; }

        public int NumberOfParts { get; set; }

        public string Description { get; set; }

        public string ResultMessage { get; set; }

        public string TranslatedStatus { get; set; }

        public string OfficeNameFrom { get; set; }

        public ParcelStatus Status { get; set; }

        public string StatusClass { get; set; }

        public string AnimationClass { get; set; }

        public string OfficeNameTo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ParcelCheckResultDto, CheckParcelResponseModel>()
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Description))
                .ForMember(x => x.NumberOfParts, x => x.MapFrom(y => y.NumberOfParts))
                .ForMember(x => x.OfficeNameFrom, x => x.MapFrom(y => y.SendingOffice))
                .ForMember(x => x.OfficeNameTo, x => x.MapFrom(y => y.ReceivingOffice))
                .ForMember(x => x.ParcelId, x => x.MapFrom(y => y.Id));
        }
    }
}
