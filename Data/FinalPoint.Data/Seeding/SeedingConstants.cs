using System;
using System.Collections.Generic;
using System.Linq;
using FinalPoint.Common;
using FinalPoint.Data.Models;
using FinalPoint.Data.Models.Enums;

namespace FinalPoint.Data.Seeding
{
    public static class SeedingConstants
    {
        public static IList<City> Cities
        {
            get
            {
                return new City[]
                {
                    new City()
                    {
                        Name = "Варна",
                        Postcode = 9000,
                    },
                    new City()
                    {
                        Name = "София",
                        Postcode = 1000,
                    },
                };
            }
        }

        public static IList<Office> Offices
        {
            get
            {
                return new Office[]
                {
                    new Office
                    {
                        PostCode = 9008,
                        Name = "Чаталджа",
                        Address = "зад пазара за цветя, Район, ул. 'Княз Н. Николаевич' №34",
                        IgnoredCityPostCode = Cities.FirstOrDefault(x => x.Postcode == 9000).Postcode,
                        IgnoredResponsibleSortingCenterPostCode = SortingCenters.FirstOrDefault(x => x.PostCode == 9000).PostCode,
                    },
                    new Office
                    {
                        PostCode = 9009,
                        Name = "Технически Университет",
                        Address = "зад Банка ДСК, ул. 'Народен Юмрук' №3",
                        IgnoredCityPostCode = Cities.FirstOrDefault(x => x.Postcode == 9000).Postcode,
                        IgnoredResponsibleSortingCenterPostCode = SortingCenters.FirstOrDefault(x => x.PostCode == 9000).PostCode,
                    },
                    new Office
                    {
                        PostCode = 9015,
                        Name = "ЖП Гара",
                        Address = "ул. Шабла 1",
                        IgnoredCityPostCode = Cities.FirstOrDefault(x => x.Postcode == 9000).Postcode,
                        IgnoredResponsibleSortingCenterPostCode = SortingCenters.FirstOrDefault(x => x.PostCode == 9000).PostCode,
                    },

                    new Office
                    {
                        PostCode = 1012,
                        Name = "Раковски",
                        Address = "ул. 'Академик Методи Попов' №13",
                        IgnoredCityPostCode = Cities.FirstOrDefault(x => x.Postcode == 1000).Postcode,
                        IgnoredResponsibleSortingCenterPostCode = SortingCenters.FirstOrDefault(x => x.PostCode == 1000).PostCode,
                    },
                };
            }
        }

        public static IList<Office> SortingCenters
        {
            get
            {
                return new Office[]
                {
                    new Office
                    {
                        PostCode = 909090,
                        Name = "Виртуален",
                        Address = "Виртуален",
                        IgnoredCityPostCode = Cities.FirstOrDefault(x => x.Postcode == 9000).Postcode,
                    },
                    new Office
                    {
                        PostCode = 9000,
                        Name = "Варна НЛЦ",
                        Address = "бул. 'Република' 59",
                        IgnoredCityPostCode = Cities.FirstOrDefault(x => x.Postcode == 9000).Postcode,
                    },
                    new Office
                    {
                        PostCode = 1000,
                        Name = "Климент Охридски ЛЛЦ",
                        Address = "бул. 'Св. Климент Охридски' 148",
                        IgnoredCityPostCode = Cities.FirstOrDefault(x => x.Postcode == 1000).Postcode,
                    },
                    new Office
                    {
                        PostCode = 1200,
                        Name = "София Искър НЛЦ",
                        Address = "кв. Горубляне бул. 'Самоковско шосе' №2Л",
                        IgnoredCityPostCode = Cities.FirstOrDefault(x => x.Postcode == 1000).Postcode,
                    },
                };
            }
        }

        public static ApplicationUser[] Users
        {
            get
            {
                return new ApplicationUser[]
                {
                    new ApplicationUser()
                        {
                            FirstName = "Николай",
                            MiddleName = "Димитров",
                            LastName = "Николаев",
                            Email = "nikolainikolaev013@gmail.com",
                            UserName = "90001",
                            PersonalId = 90001,
                            DateOfBirth = new DateTime(1999, 05, 13),
                            IgnoredRole = GlobalConstants.OwnerRoleName,
                        },
                    new ApplicationUser()
                        {
                            FirstName = "Никола",
                            MiddleName = "Георгий",
                            LastName = "Николов",
                            Email = "Nikola.Nikolov@gmail.com",
                            UserName = "90081",
                            PersonalId = 90081,
                            IgnoredRole = GlobalConstants.OfficeOwnerRoleName,
                            DateOfBirth = new DateTime(1999, 08, 20),
                            IgnoredOwnedOfficesPostcodes = new int[]
                            {
                                9008,
                                9009,
                            },
                        },
                    new ApplicationUser()
                        {
                            FirstName = "Соня",
                            MiddleName = "Пламенова",
                            LastName = "Димовска",
                            Email = "soncheto79@gmail.com",
                            UserName = "90091",
                            PersonalId = 90091,
                            IgnoredRole = GlobalConstants.OfficeOwnerRoleName,
                            DateOfBirth = new DateTime(1979, 11, 13),
                            IgnoredOwnedOfficesPostcodes = new int[]
                            {
                                9015,
                            },
                        },
                    new ApplicationUser()
                        {
                            FirstName = "Милица",
                            MiddleName = "Красимирова",
                            LastName = "Кирилова",
                            Email = "MiliPili@gmail.com",
                            UserName = "90151",
                            PersonalId = 90151,
                            IgnoredRole = GlobalConstants.OfficeOwnerRoleName,
                            DateOfBirth = new DateTime(1999, 10, 13),
                            IgnoredOwnedOfficesPostcodes = new int[]
                            {
                                1012,
                            },
                        },
                    new ApplicationUser()
                        {
                            FirstName = "Никол",
                            MiddleName = "Хамуди",
                            LastName = "Сиди",
                            Email = "nikol_sidi@abv.bg",
                            UserName = "90002",
                            PersonalId = 90002,
                            IgnoredRole = GlobalConstants.SortingCenterAdminRoleName,
                            DateOfBirth = new DateTime(2000, 12, 04),
                            IgnoredWorkOfficePostcode = 9000,
                        },
                    new ApplicationUser()
                        {
                            FirstName = "Зейн",
                            MiddleName = "Хамуди",
                            LastName = "Сиди",
                            Email = "orangebg123@gmail.com",
                            UserName = "10002",
                            PersonalId = 10002,
                            IgnoredRole = GlobalConstants.SortingCenterAdminRoleName,
                            DateOfBirth = new DateTime(2008, 12, 19),
                            IgnoredWorkOfficePostcode = 1000,
                        },
                };
            }
        }

        public static Client[] Clients
        {
            get
            {
                return new Client[]
                {
                    new Client()
                    {
                        FirstName = "Мирела",
                        LastName = "Георгиева",
                        Address = "ж.к. Вл. Варненчик, бл.22",
                        PhoneNumber = "0889874823",
                    },
                    new Client()
                    {
                        FirstName = "Иван",
                        LastName = "Иванов",
                        Address = "ж.к. Изгрев, 33",
                        PhoneNumber = "0898782938",
                    },
                };
            }
        }

        public static string[] Themes
        {
            get
            {
                return new string[]
                {
                    "Default",
                    "Cyborg",
                    "Journal",
                    "Simplex",
                    "Yeti",
                };
            }
        }

        public static string UniversalUserPassword
        {
            get
            {
                return "N111111";
            }
        }

        public static int VirtualSortingCenterPostCode
        {
            get
            {
                return 909090;
            }
        }
    }
}
