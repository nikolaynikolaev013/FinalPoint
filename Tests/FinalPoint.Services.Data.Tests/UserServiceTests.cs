using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalPoint.Data;
using FinalPoint.Web.Business.Services;
using Xunit;

namespace FinalPoint.Services.Data.Tests
{
    public class UserServiceTests
    {
        private FakeData fakeData;
        private ApplicationDbContext db;
        private UserService userService;

        public UserServiceTests()
        {
            this.fakeData = new FakeData();
            this.db = GetService.CreateDb(this.fakeData);
            this.userService = GetService.User(this.db);
        }

        [Fact]
        public void GetAllPersonalIdsShouldWorkCorrectly()
        {
            // Arrange
            var expected = this.fakeData.ApplicationUsers.Select(x => x.PersonalId);

            // Act
            var result = this.userService.GetAllPersonalIds();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAllUsersWithoutCurrentAsKeyValuePairShouldWorkCorrectly()
        {
            // Arrange
            var officeToSkip = this.fakeData.Offices[0];
            var expectedResult = this.fakeData.ApplicationUsers
                    .OrderBy(x => x.PersonalId)
                   .Select(x => new
                   {
                       x.FullName,
                       x.PersonalId,
                   }).ToList()
                   .Select(x => new KeyValuePair<string, string>(x.PersonalId.ToString(), x.FullName + " - " + x.PersonalId.ToString()));

            // Act
            var result = this.userService.GetAllUsersAsKeyValuePair();

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GetAllUsersAsKeyValuePairShouldWorkCorrectly()
        {
            // Arrange
            var currUserId = this.fakeData.ApplicationUsers[0].Id;
            var expectedResult = this.fakeData.ApplicationUsers
                            .Where(x => x.Id != currUserId)
                            .OrderBy(x => x.PersonalId)
                            .Select(x => new
                            {
                                x.FullName,
                                x.PersonalId,
                            }).ToList()
                            .Select(x => new KeyValuePair<string, string>(x.PersonalId.ToString(), x.FullName + " - " + x.PersonalId.ToString())); ;

            // Act
            var result = this.userService.GetAllUsersWithoutCurrentAsKeyValuePair(currUserId);

            // Assert
            Assert.Equal(result, expectedResult);
        }
    }
}
