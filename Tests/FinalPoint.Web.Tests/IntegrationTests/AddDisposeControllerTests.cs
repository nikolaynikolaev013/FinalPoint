namespace FinalPoint.Web.Tests.IntegrationTests
{
    using FinalPoint.Web.Controllers;
    using FinalPoint.Web.ViewModels.AddDispose;
    using FinalPoint.Web.ViewModels.Home;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AddDisposeControllerTests
    {
        [Fact]
        public void GetAddParcelShouldReturnViewWithModel()
            => MyMvc
                .Pipeline()
                .ShouldMap(req => req
                    .WithPath("/Add")
                    .WithUser())
                .To<AddDisposeController>(x => x.AddParcel())
                .Which()
                .ShouldReturn()
                .View(view => view.WithModelOfType<AddParcelInputModel>());

        [Fact]
        public void PostAddParcelShouldAddSuccessfullyAndReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap(req => req
                    .WithPath("/Add")
                    .WithUser());
    }
}
