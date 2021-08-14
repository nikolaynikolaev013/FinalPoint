namespace FinalPoint.Services.Data.Tests.Mocks
{
    using System;

    using FinalPoint.Data;
    using Microsoft.EntityFrameworkCore;

    public static class DbMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ApplicationDbContext(dbContextOptions);
            }
        }
    }
}
