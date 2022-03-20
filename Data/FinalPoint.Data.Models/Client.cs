namespace FinalPoint.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FinalPoint.Data.Common.Models;

    public class Client : BaseDeletableModel<int>
    {
        public Client()
        {
            this.SentParcels = new HashSet<Parcel>();
            this.ReceivedParcels = new HashSet<Parcel>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Parcel> SentParcels { get; set; }

        [InverseProperty("Recipent")]
        public virtual ICollection<Parcel> ReceivedParcels { get; set; }
    }
}
