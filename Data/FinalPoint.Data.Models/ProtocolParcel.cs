namespace FinalPoint.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using FinalPoint.Data.Common.Models;
    using FinalPoint.Data.Models.Enums;

    public class ProtocolParcel : BaseDeletableModel<int>
    {
        public ProtocolParcel()
        {
        }

        [ForeignKey(nameof(Parcel))]
        public int ParcelId { get; set; }

        public virtual Parcel Parcel { get; set; }

        [ForeignKey(nameof(Protocol))]
        public int ProtocolId { get; set; }

        public virtual Protocol Protocol { get; set; }

        public ParcelStatus Status { get; set; }

        [ForeignKey(nameof(ResponsibleUser))]
        public string ResponsibleUserId { get; set; }

        public virtual ApplicationUser ResponsibleUser { get; set; }

        public DateTime TimeEdited { get; set; }
    }
}
