// ReSharper disable VirtualMemberCallInConstructor
namespace FinalPoint.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FinalPoint.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.OwnOffices = new HashSet<Office>();
            this.IgnoredOwnedOfficesPostcodes = new int[0];
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FullName { get; set; }

        [RegularExpression("[0-9]{4,}")]
        public int PersonalId { get; set; }

        [ForeignKey(nameof(Office))]
        public int WorkOfficeId { get; set; }

        public virtual Office WorkOffice { get; set; }

        [InverseProperty("Owner")]
        public virtual ICollection<Office> OwnOffices { get; set; }

        [InverseProperty("ResponsibleUser")]
        public virtual ICollection<ProtocolParcel> ResponsibleForProtocolRecords { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        [NotMapped]
        public int[] IgnoredOwnedOfficesPostcodes { get; set; }

        [NotMapped]
        public string IgnoredRole { get; set; }
    }
}
