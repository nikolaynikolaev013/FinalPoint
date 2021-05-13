namespace FinalPoint.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FinalPoint.Data.Common.Models;
    using FinalPoint.Data.Models.Enums;

    public class Protocol : BaseDeletableModel<int>
    {
        public Protocol()
        {
            this.Parcels = new HashSet<ProtocolParcel>();
        }

        [ForeignKey(nameof(ApplicationUser))]
        public int CreatedByEmployeeId { get; set; }

        public virtual ApplicationUser CreatedByEmployee { get; set; }

        public bool IsClosed { get; set; }

        public ProtocolType Type { get; set; }

        public virtual ICollection<ProtocolParcel> Parcels { get; set; }

    }
}
