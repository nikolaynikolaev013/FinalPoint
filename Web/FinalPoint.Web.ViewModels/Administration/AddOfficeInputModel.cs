namespace FinalPoint.Web.ViewModels.Administration
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FinalPoint.Data.Models.Enums;

    public class AddOfficeInputModel
    {

        [Display(Prompt = "Пощенски код")]
        [RegularExpression("[0-9]{4,}", ErrorMessage = "Полето трябва да съдържа поне 4 цифри")]
        public int PostCode { get; set; }

        [Display(Prompt = "Име на офиса")]
        public string Name { get; set; }

        [Display(Prompt = "Офис или РЦ")]
        [EnumDataType(typeof(OfficeType))]
        public OfficeType OfficeType { get; set; }

        [Display(Prompt = "Град")]
        public int CityId { get; set; }

        [Display(Prompt = "Адрес")]
        public string Address { get; set; }

        [Display(Prompt = "Пощенски код на обслужващо РЦ (ако е приложимо)")]
        //[RegularExpression("[0-9]{4,}", ErrorMessage = "Полето трябва да съдържа поне 4 цифри")]
        public int? ResponsibleSortingCenter { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CitiesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SortingCentersItems { get; set; }

    }
}
