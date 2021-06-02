namespace FinalPoint.Web.ViewModels.LoadUnload
{
    using System;
    using System.Collections.Generic;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.DTOs.LoadUnload;
    using FinalPoint.Web.ViewModels.Shared;
    using FinalPoint.Web.ViewModels.ViewComponents;

    public class LoadUnloadProtocolViewModel
    {
        public LoadUnloadProtocolViewModel()
        {
            this.Parcels = new HashSet<ParcelDto>();
            this.IsClosed = false;
        }

        public ProtocolType Type { get; set; }

        public ParcelsTableShowModel ParcelTableShowViewData { get; set; }

        public string TranslatedType { get; set; }

        public int Id { get; set; }

        public int Line { get; set; }

        public bool IsClosed { get; set; }

        public DateTime Date { get; set; }

        public ParcelInsertPartialViewModel ParcelInsertViewModel { get; set; }

        public ICollection<ParcelDto> Parcels { get; set; }

        public string Message { get; set; }

        public string TypeOfMessage { get; set; }

        public int RecipentOfficeId { get; set; }
    }
}
