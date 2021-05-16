﻿namespace FinalPoint.Web.ViewModels.Administration
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FinalPoint.Data.Models.Enums;

    public class AddOfficeInputModel
    {

        [RegularExpression("[0-9]{4,}", ErrorMessage = "Полето трябва да съдържа поне 4 цифри")]
        [Display(Name = "Пощенски код", Prompt = "Въведете пощенския код")]
        public int PostCode { get; set; }

        [Display(Name = "Име на офиса:", Prompt = "Въведете име на офиса")]
        public string Name { get; set; }

        [Display(Name = "Офис или РЦ")]
        [EnumDataType(typeof(OfficeType))]
        public OfficeType OfficeType { get; set; }

        [Display(Name = "Град")]
        public int CityId { get; set; }

        public AddCityInputModel CityInputModel { get; set; }

        [Required]
        [Display(Name = "Адрес:", Prompt = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Обслужващо РЦ (ако е приложимо)")]
        public int? ResponsibleSortingCenter { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CitiesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SortingCentersItems { get; set; }

    }
}