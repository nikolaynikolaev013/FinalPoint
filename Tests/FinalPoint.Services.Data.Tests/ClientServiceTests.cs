namespace FinalPoint.Services.Data.Tests
{
    using System.Threading.Tasks;

    using FinalPoint.Data;
    using FinalPoint.Services.Data.Client;
    using FinalPoint.Web.ViewModels.AddDispose;
    using Xunit;

    public class ClientServiceTests
    {
        private FakeData fakeData;
        private ApplicationDbContext db;
        private ClientService clientService;

        public ClientServiceTests()
        {
            this.fakeData = new FakeData();
            this.db = GetService.CreateDb(this.fakeData);
            this.clientService = GetService.Client(this.db);
        }

        [Fact]
        public async Task CreateShouldWorkAsExpectedAsync()
        {
            // Arrange
            var testClient = this.fakeData.Clients[0];

            var model = new AddClientInputModel(FinalPoint.Data.Models.Enums.ClientType.Подател)
            {
                Address = testClient.Address,
                FirstName = testClient.FirstName,
                LastName = testClient.LastName,
                PhoneNumber = testClient.PhoneNumber,
            };

            // Act
            var result = await this.clientService.CreateAsync(model);

            // Assert
            Assert.Equal(result.FirstName, testClient.FirstName);
            Assert.Equal(result.LastName, testClient.LastName);
            Assert.Equal(result.PhoneNumber, testClient.PhoneNumber);
            Assert.Equal(this.db.Clients.Local.Count - 1, this.fakeData.Clients.Count);
        }
    }
}
