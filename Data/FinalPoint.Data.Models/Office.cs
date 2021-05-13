namespace FinalPoint.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FinalPoint.Data.Common.Models;
    using FinalPoint.Data.Models.Enums;

    public class Office : BaseDeletableModel<int>
    {
        public Office()
        {
            this.Employees = new HashSet<ApplicationUser>();
            this.Parcels = new HashSet<Parcel>();
        }

        public int PostCode { get; set; }

        [Required]
        public string Name { get; set; }

        public OfficeType OfficeType { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int? OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public string Address { get; set; }

        [ForeignKey(nameof(Office))]
        public int? ResponsibleSortingCenterId { get; set; }

        public virtual Office ResponsibleSortingCenter { get; set; }

        public virtual ICollection<ApplicationUser> Employees { get; set; }

        [InverseProperty("CurrentOffice")]
        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}
