using System;
using System.Collections.Generic;
using FinalPoint.Data.Models;
using FinalPoint.Web.ViewModels.DTOs.LoadUnload;

namespace FinalPoint.Web.ViewModels.ViewComponents
{
    public class ParcelsTableShowModel
    {
        public ParcelsTableShowModel()
        {
            this.Parcels = new HashSet<ParcelsTableShowParcelViewModel>();
        }

        public ICollection<ParcelsTableShowParcelViewModel> Parcels { get; set; }

        public Protocol Protocol { get; set; }

        public ICollection<string> TransltedStatus { get; set; }
    }
}
