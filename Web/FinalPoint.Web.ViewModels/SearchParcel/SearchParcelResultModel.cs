using System;
using System.Collections.Generic;
using FinalPoint.Data.Models;
using FinalPoint.Web.ViewModels.Shared;

namespace FinalPoint.Web.ViewModels.TrackParcel
{
    public class SearchParcelResultModel
    {
        public SearchParcelResultModel()
        {
            this.Parcels = new HashSet<SingleParcelSearchShowPartialViewModel>();
        }

        public ICollection<SingleParcelSearchShowPartialViewModel> Parcels { get; set; }

        public bool IsDispose { get; set; }

        public int CurrUserWorkOfficeId { get; set; }
    }
}
