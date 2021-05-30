using System;
using FinalPoint.Data.Models;

namespace FinalPoint.Web.ViewModels.DTOs.LoadUnload
{
    public class ParcelsTableShowParcelViewModel
    {
        public ParcelsTableShowParcelViewModel()
        {
        }

        public ProtocolParcel ProtocolParcel { get; set; }

        public string TranslatedStatus { get; set; }
    }
}
