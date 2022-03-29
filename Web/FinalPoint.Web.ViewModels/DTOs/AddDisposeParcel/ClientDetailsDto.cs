using System;
using AutoMapper;
using FinalPoint.Data.Models;
using FinalPoint.Services.Mapping;

namespace FinalPoint.Web.ViewModels.DTOs.AddDisposeParcel
{
	public class ClientDetailsDto : IMapFrom<Client>, IMapTo<ClientDetailsDto>, IHaveCustomMappings
	{
		public ClientDetailsDto()
		{
		}

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Client, ClientDetailsDto>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.FirstName, x => x.MapFrom(y => y.FirstName))
                .ForMember(x => x.LastName, x => x.MapFrom(y => y.LastName))
                .ForMember(x => x.Address, x => x.MapFrom(y => y.Address))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(y => y.PhoneNumber))
                .ForMember(x => x.Email, x => x.MapFrom(y => y.EmailAddress))
                .ForAllOtherMembers(x => x.Ignore());

            configuration.CreateMap<ClientDetailsDto, Client>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.FirstName, x => x.MapFrom(y => y.FirstName))
                .ForMember(x => x.LastName, x => x.MapFrom(y => y.LastName))
                .ForMember(x => x.Address, x => x.MapFrom(y => y.Address))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(y => y.PhoneNumber))
                .ForMember(x => x.EmailAddress, x => x.MapFrom(y => y.Email))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}