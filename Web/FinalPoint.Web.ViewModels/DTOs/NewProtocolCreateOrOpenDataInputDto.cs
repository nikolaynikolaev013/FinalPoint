namespace FinalPoint.Web.ViewModels.DTOs
{
    using System.Security.Claims;

    using FinalPoint.Data.Models.Enums;

    public class NewProtocolCreateOrOpenDataInputDto
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
    }
}
