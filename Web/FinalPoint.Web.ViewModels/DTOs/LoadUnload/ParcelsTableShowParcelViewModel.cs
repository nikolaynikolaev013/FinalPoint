using System;
using FinalPoint.Data.Models;
using FinalPoint.Web.ViewModels.Shared;

namespace FinalPoint.Web.ViewModels.DTOs.LoadUnload
{
    public class ParcelsTableShowParcelViewModel
    {
        public ParcelsTableShowParcelViewModel()
        {
        }

        public ProtocolParcel ProtocolParcel { get; set; }

        public SingleParcelSearchShowPartialViewModel Parcel { get; set; }

        public string TranslatedStatus { get; set; }
    }
}
