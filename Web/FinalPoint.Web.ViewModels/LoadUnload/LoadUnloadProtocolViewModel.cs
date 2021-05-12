namespace FinalPoint.Web.ViewModels.LoadUnload
{
    using System;
    using System.Collections.Generic;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.DTOs.LoadUnload;
    using FinalPoint.Web.ViewModels.Shared;

    public class LoadUnloadProtocolViewModel
    {
        public LoadUnloadProtocolViewModel()
        {
            this.Parcels = new HashSet<ParcelDto>();
        }

        public ProtocolType Type { get; set; }

        public string Id { get; set; }

        public string Line { get; set; }

        public DateTime Date { get; set; }

        public ParcelINsertPartialViewModel ParcelInsertViewModel { get; set; }

        public ICollection<ParcelDto> Parcels { get; set; }
    }
}
