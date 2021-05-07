namespace FinalPoint.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using FinalPoint.Data.Common.Models;

    public class Client : BaseDeletableModel<int>
    {
        public Client()
        {
            this.SentParcels = new HashSet<Parcel>();
            this.ReceivedParcels = new HashSet<Parcel>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [InverseProperty("SenderId")]
        public virtual ICollection<Parcel> SentParcels { get; set; }

        [InverseProperty("RecipentId")]
        public virtual ICollection<Parcel> ReceivedParcels { get; set; }
    }
}
