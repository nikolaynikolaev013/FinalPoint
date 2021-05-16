namespace FinalPoint.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class AddCityInputModel
    {
        [Display(Name = "Име:", Prompt = "Въведете името на града")]
        public string Name { get; set; }

        [Display(Name = "Пощенски код на града:", Prompt = "Въведете пощенския код")]
        public int? Postcode { get; set; }
    }
}
