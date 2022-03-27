namespace FinalPoint.Web.ViewModels.DTOs
{
    using System.Security.Claims;
    using AutoMapper;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Services.Mapping;

    public class NewProtocolCreateOrOpenDataInputDto : IMapTo<Protocol>, IMapFrom<NewProtocolCreateOrOpenDataInputDto>, IHaveCustomMappings
    {
        public NewProtocolCreateOrOpenDataInputDto()
        {
        }

        public int Id { get; set; }

        public ProtocolType Type { get; set; }

        public string TranslatedType { get; set; }

        public ClaimsPrincipal User { get; set; }

        public int RecipentOfficeId { get; set; }

        public string Message { get; set; }

        public string TypeOfMessage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<NewProtocolCreateOrOpenDataInputDto, Protocol>()
                .ForMember(x => x.Type, x => x.MapFrom(y => y.Type))
                .ForMember(x => x.OfficeToId, x => x.MapFrom(y => y.RecipentOfficeId))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}