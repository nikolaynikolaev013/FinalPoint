using System.ComponentModel.DataAnnotations;

namespace FinalPoint.Data.Models.Enums
{
    public enum OfficeType
    {
        [Display(Name = "Офис")]
        Office = 10,
        [Display(Name = "Разпределителен център")]
        SortingCenter = 20,
    }
}
