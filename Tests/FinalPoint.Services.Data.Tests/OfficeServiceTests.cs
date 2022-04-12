namespace FinalPoint.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinalPoint.Data;
    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.Business.Services;
    using FinalPoint.Web.ViewModels.Administration;
    using Xunit;

    public class OfficeServiceTests
    {
        private FakeData fakeData;
        private ApplicationDbContext db;
        private OfficeService officeService;

        public OfficeServiceTests()
        {
            this.fakeData = new FakeData();
            this.db = GetService.CreateDb(this.fakeData);
            this.officeService = GetService.Office(this.db);
        }

        [Fact]
        public async Task CreateShouldWorkAsExpectedAsync()
        {
            // Arrange
            var office = this.fakeData.Offices[0];

            var model = new AddOfficeInputModel()
            {
                Address = office.Address,
                Name = office.Name,
                PostCode = office.PostCode,
                CityId = office.City.Id,
                CityInputModel = new AddCityInputModel
                {
                    Name = office.City.Name,
                    Postcode = office.City.Postcode,
                },
            };

            // Act
            var result = await this.officeService.CreateAsync(model);

            // Assert
            Assert.Equal(result.Name, office.Name);
            Assert.Equal(result.PostCode, office.PostCode);
            Assert.Equal(db.Offices.Local.Count - 1, this.fakeData.Offices.Count);
        }

        [Fact]
        public async Task RemoveShouldWorkCorrectlyWithCorrectIdAsync()
        {
            // Arrange

            // Act
            var removedOffice = await officeService.Remove(this.fakeData.Offices[0].Id);

            // Assert
            Assert.True(removedOffice.IsDeleted);
        }

        [Fact]
        public void GetOfficeAsStringByIdShouldReturnAStringInTheCorrectFormat()
        {
            // Arrange

            // Act
            var result = officeService.GetOfficeAsStringById(1);

            // Assert
            Assert.Equal($"{this.fakeData.Offices[0].City.Name} - {this.fakeData.Offices[0].Name} - {this.fakeData.Offices[0].PostCode}", result);
        }

        [Fact]
        public void GetOfficeByIdShouldWorkCorrectly()
        {
            // Arrange

            // Act
            var office = this.fakeData.Offices[0];

            // Assert
            var result = officeService.GetOfficeById(office.Id);

            Assert.Equal(result, office);
        }

        [Fact]
        public void GetVirtualOfficeShouldWorkCorrectly()
        {
            // Arrange
            var office = this.fakeData.Offices.Where(x=>x.Name.ToLower() == "виртуален").FirstOrDefault();

            // Act
            var result = officeService.GetVirtualOffice();

            // Assert
            Assert.Equal(result.Id, office.Id);
            Assert.Equal(result.Name, office.Name);
        }

        [Fact]
        public void GetOfficeByPostcodeShouldWorkProperly()
        {
            // Arrange
            var office = this.fakeData.Offices[0];

            // Act
            var result = officeService.GetOfficeByPostcode(office.PostCode);

            // Assert
            Assert.Equal(result, office);
        }

        [Fact]
        public void GetAllOfficeIdsInRangeOfSortingCenterIdShouldWorkProperly()
        {
            // Arrange

            // Act
            var result = officeService.GetAllOfficeIdsInRangeOfSortingCenterId(this.fakeData.Offices[0].Id);
            var expectedResult = this.fakeData.Offices
                .Where(x => x.ResponsibleSortingCenterId == this.fakeData.Offices[0].Id)
                .Select(x => x.Id)
                .ToHashSet();

            // Assert
            Assert.Equal(result, expectedResult);
        }

        //[Fact]
        //public void GetAllOfficesWithoutVirtualShouldWorkCorrectly()
        //{
        //    // Arrange
        //    var db = this.CreateDb();
        //    var officeService = GetService.Office(db);

        //    // Act
        //    var result = officeService.GetAllOfficesWithoutVirtual();

        //    var allOffices = this.fakeData.Offices.Where(x => x.Name.ToLower() != "виртуален") ;

        //    var allOfficesAsStrings = new HashSet<string>();

        //    foreach (var office in allOffices)
        //    {
        //        allOfficesAsStrings.Add($"Офис: {office.City.Name} {office.Name} ({office.PostCode}) - обслужващо РЦ: {office.ResponsibleSortingCenter.Name} ({office.ResponsibleSortingCenter.PostCode}) - собственик: {office.Owner?.FullName} ({office.Owner?.PersonalId})");
        //    }

        //    // Assert
        //    Assert.Equal(result, allOfficesAsStrings);
        //}

        [Fact]
        public void GeAllOfficesAndSortingCentersAsKeyValuePairsShouldWorkProperly()
        {
            // Arrange

            // Act
            var result = officeService.GeAllOfficesAndSortingCentersAsKeyValuePairs();
            var expectedResult = new List<KeyValuePair<string, string>>();

            foreach (var office in this.fakeData.Offices)
            {
                expectedResult.Add(new KeyValuePair<string, string>(office.Id.ToString(), $"{office.City.Name} - {office.Name} - {office.PostCode}"));
            }

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairsShouldWorkProperly()
        {
            // Arrange
            var officeToSkip = this.fakeData.Offices[2];

            // Act
            var result = officeService.GeAllOfficesAndSortingCentersWithoutCurrOneAsKeyValuePairs(officeToSkip.Id);
            var expectedResult = new List<KeyValuePair<string, string>>();

            foreach (var office in this.fakeData.Offices.Where(x => x.Id != officeToSkip.Id && x.Name.ToLower() != "виртуален"))
            {
                expectedResult.Add(new KeyValuePair<string, string>(office.Id.ToString(), $"{office.City.Name} - {office.Name} - {office.PostCode}"));
            }

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GetLoadUnloadOfficesShouldWorkProperlyForOffice()
        {
            // Arrange
            var officeToSkip = this.fakeData.Offices[2];

            // Act
            var result = officeService.GetLoadUnloadOffices(officeToSkip);
            var expectedResult = new List<KeyValuePair<string, string>>();

            foreach (var office in this.fakeData.Offices.Where(x => x.Id == officeToSkip.ResponsibleSortingCenterId))
            {
                expectedResult.Add(new KeyValuePair<string, string>(office.Id.ToString(), $"{office.City.Name} - {office.Name} - {office.PostCode}"));
            }

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GetLoadUnloadOfficesShouldWorkProperlyForSortingCenter()
        {
            // Arrange
            var officeToSkip = this.fakeData.Offices[0];

            // Act
            var result = officeService.GetLoadUnloadOffices(officeToSkip);
            var expectedResult = new List<KeyValuePair<string, string>>();

            foreach (var office in this.fakeData.Offices.Where(
                    x => x.ResponsibleSortingCenterId == officeToSkip.Id
                        || (x.OfficeType == OfficeType.SortingCenter
                        && x.Name.ToLower() != "виртуален"
                        && x.Id != officeToSkip.Id)))
            {
                expectedResult.Add(new KeyValuePair<string, string>(office.Id.ToString(), $"{office.City.Name} - {office.Name} - {office.PostCode}"));
            }

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GetAllSortingCentersAsKeyValuePairsShouldWorkCorrectly()
        {
            // Arrange
            var officeToSkip = this.fakeData.Offices[0];

            // Act
            var result = this.officeService.GetAllSortingCentersAsKeyValuePairs().ToList();
            var expectedResult = new List<KeyValuePair<string, string>>();

            foreach (var office in this.fakeData.Offices.Where(
                    x => x.OfficeType == OfficeType.SortingCenter))
            {
                expectedResult.Add(new KeyValuePair<string, string>(office.Id.ToString(), $"{office.Name}"));
            }

            // Assert
            Assert.Equal(result, expectedResult);
        }
    }
}
