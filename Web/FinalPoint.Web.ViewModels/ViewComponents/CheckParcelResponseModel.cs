using System;
using FinalPoint.Data.Models.Enums;

namespace FinalPoint.Web.ViewModels.ViewComponents
{
    public class CheckParcelResponseModel
    {
        public CheckParcelResponseModel()
        {
        }

        public int ParcelId { get; set; }

        public int NumberOfParts { get; set; }

        public string Description { get; set; }

        public string ResultMessage { get; set; }

        public string TranslatedStatus { get; set; }

        public string OfficeNameFrom { get; set; }

        public ParcelStatus Status { get; set; }

        public string StatusClass { get; set; }

        public string AnimationClass { get; set; }

        public string OfficeNameTo { get; set; }
    }
}
