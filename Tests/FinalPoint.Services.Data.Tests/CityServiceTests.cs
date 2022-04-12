namespace FinalPoint.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FinalPoint.Data;
    using FinalPoint.Web.Business.Services;
    using FinalPoint.Web.ViewModels.Administration;
    using Xunit;

    public class CityServiceTests
    {

        private FakeData fakeData;
        private ApplicationDbContext db;
        private CityService cityService;

        public CityServiceTests()
        {
            this.fakeData = new FakeData();
            this.db = GetService.CreateDb(this.fakeData);
            this.cityService = GetService.City(this.db);
        }

        [Fact]
        public async Task CreateShouldWorkAsExpectedAsync()
        {
            // Arrange
            var city = this.fakeData.Cities[0];

            var model = new AddCityInputModel()
            {
                Name = city.Name,
                Postcode = city.Postcode,
            };

            // Act
            var result = await this.cityService.CreateAsync(model);

            // Assert
            Assert.Equal(result, this.fakeData.Cities.Count + 1);
            Assert.Equal(this.db.Cities.Local.Count - 1, this.fakeData.Cities.Count);
        }

        [Fact]
        public void GetAllCitiesAsKeyValuePairsShouldWorkCorrectly()
        {
            // Arrange

            // Act
            var result = this.cityService.GetAllCitiesAsKeyValuePairs().ToList();
            var expectedResult = new List<KeyValuePair<string, string>>();

            foreach (var city in this.fakeData.Cities)
            {
                expectedResult.Add(new KeyValuePair<string, string>(city.Id.ToString(), $"{city.Name}"));
            }

            // Assert
            Assert.Equal(result, expectedResult);
        }
    }
}
