using AutoMapper;
using FinalPoint.Services.Mapping;

namespace FinalPoint.Web.ViewModels.DTOs.LoadUnload
{
    public class ParcelDto : IHaveCustomMappings
    {
        public string Id { get; set; }

        public int Parts { get; set; }

        public string Status { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            //configuration.CreateMap<>();
        }
    }
}
