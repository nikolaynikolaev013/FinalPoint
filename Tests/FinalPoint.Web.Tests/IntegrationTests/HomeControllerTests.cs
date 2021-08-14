namespace FinalPoint.Web.Tests.IntegrationTests
{
    using FinalPoint.Web.Controllers;
    using FinalPoint.Web.ViewModels.Home;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void IndexTest()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                        .WithPath("/")
                        .WithUser())
                .To<HomeController>(x => x.Index())
                .Which()
                .ShouldReturn()
                .View();

        [Fact]
        public void AssetsTest()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Assets")
                .To<HomeController>(x => x.Assets())
                .Which()
                .ShouldReturn()
                .View(view => view.WithModelOfType<LoginUsersAndOfficesShowViewModel>());

    }
}
