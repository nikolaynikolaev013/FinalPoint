namespace FinalPoint.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using FinalPoint.Data.Common.Models;

    public class ProtocolParcel : BaseDeletableModel<int>
    {
        public ProtocolParcel()
        {
        }

        [ForeignKey(nameof(Parcel))]
        public int ParcelId { get; set; }

        public virtual Parcel Parcel { get; set; }


        [ForeignKey(nameof(ApplicationUser))]
        public int ResponsibleUserId { get; set; }

        public virtual ApplicationUser ResponsibleUser { get; set; }

        public DateTime TimeEdited { get; set; }

        [ForeignKey(nameof(Office))]

        public int OfficeStorageFromId { get; set; }

        public virtual Office OfficeStorageFrom { get; set; }

        [ForeignKey(nameof(Office))]
        public int OfficeStorageToId { get; set; }

        public virtual Office OfficeStorageTo { get; set; }
    }
}
