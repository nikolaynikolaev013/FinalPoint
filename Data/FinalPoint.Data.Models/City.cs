namespace FinalPoint.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FinalPoint.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
           this.Offices = new HashSet<Office>();
        }

        [Required]
        public string Name { get; set; }

        public int Postcode { get; set; }

        public virtual ICollection<Office> Offices { get; set; }
    }
}
