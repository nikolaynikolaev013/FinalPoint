namespace FinalPoint.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum OfficeType
    {
        [Display(Name = "Офис")]
        Office = 10,
        [Display(Name = "Разпределителен център")]
        SortingCenter = 20,
    }
}
