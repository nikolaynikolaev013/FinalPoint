namespace FinalPoint.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FinalPoint.Data.Common.Models;
    using FinalPoint.Data.Models.Enums;

    public class Parcel : BaseDeletableModel<int>
    {
        public Parcel()
        {
            this.Protocols = new HashSet<Protocol>();
            this.HasCashOnDelivery = false;
            this.IsFragile = false;
            this.DontPaletize = false;
        }

        [Required]
        public string Description { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int SendingEmployeeId { get; set; }

        public virtual ApplicationUser SendingEmployee { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int? DeliveringEmployeeId { get; set; }

        public virtual ApplicationUser DeliveringEmployee { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public double Weight { get; set; }

        public bool HasCashOnDelivery { get; set; }

        public double? CashOnDeliveryPrice { get; set; }

        public bool IsFragile { get; set; }

        public bool DontPaletize { get; set; }

        public ParcelChargeType ChargeType { get; set; }

        public decimal DeliveryPrice { get; set; }

        [ForeignKey(nameof(Office))]
        public int SendingOfficeId { get; set; }

        public virtual Office SendingOffice { get; set; }

        [ForeignKey(nameof(Office))]
        public int ReceivingOfficeId { get; set; }

        public virtual Office ReceivingOffice { get; set; }

        [ForeignKey(nameof(Client))]
        public int SenderId { get; set; }

        public virtual Client Sender { get; set; }

        [ForeignKey(nameof(Client))]
        public int RecipentId { get; set; }

        public virtual Client Recipent { get; set; }

        [ForeignKey(nameof(Office))]
        public int CurrentOfficeId { get; set; }

        public virtual Office CurrentOffice { get; set; }

        public virtual ICollection<Protocol> Protocols { get; set; }
    }

}
