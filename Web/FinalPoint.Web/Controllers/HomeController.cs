﻿namespace FinalPoint.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using FinalPoint.Services.Data;
    using FinalPoint.Services.Data.Mail;
    using FinalPoint.Services.Data.Office;
    using FinalPoint.Services.Data.User;
    using FinalPoint.Web.ViewModels;
    using FinalPoint.Web.ViewModels.DTOs;
    using FinalPoint.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IUserService userService;
        private readonly IOfficeService officeService;

        public HomeController(
            IUserService userService,
            IOfficeService officeService)
        {
            this.userService = userService;
            this.officeService = officeService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [AllowAnonymous]
        public IActionResult Assets()
        {
            var model = new LoginUsersAndOfficesShowViewModel();
            model.Users = this.userService.GetAllUsers();
            model.Offices = this.officeService.GetAllOfficesWithoutVirtual();
            return this.View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
