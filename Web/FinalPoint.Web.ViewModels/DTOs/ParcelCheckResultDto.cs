using System;
using AutoMapper;
using FinalPoint.Data.Models;
using FinalPoint.Services.Mapping;

namespace FinalPoint.Web.ViewModels.DTOs
{
    public class ParcelCheckResultDto : IMapTo<Parcel>, IMapFrom<ParcelCheckResultDto>, IHaveCustomMappings
    {
        public ParcelCheckResultDto()
        {
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public int NumberOfParts { get; set; }

        public string SendingOffice { get; set; }

        public string ReceivingOffice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Parcel, ParcelCheckResultDto>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Description))
                .ForMember(x => x.NumberOfParts, x => x.MapFrom(y => y.NumberOfParts))
                .ForMember(x => x.SendingOffice, x => x.MapFrom(y => y.SendingOffice.Name))
                .ForMember(x => x.ReceivingOffice, x => x.MapFrom(y => y.ReceivingOffice.Name))
                .ForAllMembers(x => x.Ignore());
        }
    }
}