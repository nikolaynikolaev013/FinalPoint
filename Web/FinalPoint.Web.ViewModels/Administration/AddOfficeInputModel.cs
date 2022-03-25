namespace FinalPoint.Web.ViewModels.Administration
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Services.Mapping;
    using FinalPoint.Web.ViewModels.CustomAttributes;

    public class AddOfficeInputModel : IHaveCustomMappings
    {
        [CustomRequired]
        [Range(1000, int.MaxValue, ErrorMessage = "Полето трябва да съдържа поне 4 цифри")]
        [Display(Name = "Пощенски код", Prompt = "Въведете пощенския код")]
        public int PostCode { get; set; }

        [Display(Name = "Име на офиса:", Prompt = "Въведете име на офиса")]
        [CustomRequired]
        public string Name { get; set; }

        [CustomRequired]
        [Display(Name = "Офис или РЦ")]
        [EnumDataType(typeof(OfficeType))]
        public OfficeType OfficeType { get; set; }

        [CustomRequired]
        [Display(Name = "Град")]
        public int CityId { get; set; }

        public AddCityInputModel CityInputModel { get; set; }

        [CustomRequired]
        [Display(Name = "Адрес:", Prompt = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Обслужващо РЦ (ако е приложимо)")]
        public int ResponsibleSortingCenter { get; set; }

        //[CustomRequired]
        [Display(Name = "Собственик")]
        public int OwnerId { get; set; }

        public string ResultMessage { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CitiesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SortingCentersItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AllUsers { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Office, AddOfficeInputModel>()
                .ForMember(x => x.PostCode, x => x.MapFrom(y => y.PostCode))
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Name))
                .ForMember(x => x.OfficeType, x => x.MapFrom(y => y.OfficeType))
                .ForMember(x => x.CityId, x => x.MapFrom(y => y.CityId))
                .ForMember(x => x.Address, x => x.MapFrom(y => y.Address));
        }
    }
}