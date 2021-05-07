namespace FinalPoint.Data.Models
{
    using System.Collections.Generic;

    using FinalPoint.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
           this.Offices = new HashSet<Office>();
        }

        public string Name { get; set; }

        public virtual ICollection<Office> Offices { get; set; }
    }
}
