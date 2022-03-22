using System;
using AutoMapper;
using FinalPoint.Data.Models;
using FinalPoint.Services.Mapping;
using FinalPoint.Web.ViewModels.Shared;

namespace FinalPoint.Web.ViewModels.DTOs.LoadUnload
{
    public class ParcelsTableShowParcelViewModel : IHaveCustomMappings
    {
        public ParcelsTableShowParcelViewModel()
        {
        }

        public ProtocolParcel ProtocolParcel { get; set; }

        public SingleParcelSearchShowPartialViewModel Parcel { get; set; }

        public string TranslatedStatus { get; set; }

        public string BackgroundColorClass { get; set; }

        public string StatusIconName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            //configuration.CreateMap<ProtocolParcel, ParcelsTableShowParcelViewModel>();
        }
    }
}
