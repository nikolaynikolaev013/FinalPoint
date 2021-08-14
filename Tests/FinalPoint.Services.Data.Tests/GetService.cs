namespace FinalPoint.Services.Data.Tests
{
    using FinalPoint.Data;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Repositories;
    using FinalPoint.Services.Data.Tests.Mocks;

    public static class GetService
    {
        public static UserService User(ApplicationDbContext db) => new UserService(new EfDeletableEntityRepository<ApplicationUser>(db), null);

        public static CityService City(ApplicationDbContext db) => new CityService(new EfDeletableEntityRepository<City>(db));

        public static OfficeService Office(ApplicationDbContext db) => new OfficeService(new EfDeletableEntityRepository<Office>(db), City(db), User(db));

        public static ClientService Client(ApplicationDbContext db) => new ClientService(new EfDeletableEntityRepository<Client>(db));

        public static ApplicationDbContext CreateDb(FakeData fakeData)
        {
            var data = DbMock.Instance;

            foreach (var city in fakeData.Cities)
            {
                data.Cities.Add(city);
            }

            foreach (var client in fakeData.Clients)
            {
                data.Clients.Add(client);
            }

            foreach (var office in fakeData.Offices)
            {
                data.Offices.Add(office);
            }

            foreach (var user in fakeData.ApplicationUsers)
            {
                data.Users.Add(user);
            }

            data.SaveChanges();

            return data;
        }
    }
}
