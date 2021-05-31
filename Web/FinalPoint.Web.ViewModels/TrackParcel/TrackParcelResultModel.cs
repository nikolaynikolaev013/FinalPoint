using System;
using System.Collections.Generic;
using FinalPoint.Data.Models;

namespace FinalPoint.Web.ViewModels.TrackParcel
{
    public class TrackParcelResultModel
    {
        public TrackParcelResultModel()
        {
            this.Parcels = new HashSet<Parcel>();
        }

        public ICollection<Parcel> Parcels { get; set; }

        public int CurrUserWorkOfficeId { get; set; }
    }
}
