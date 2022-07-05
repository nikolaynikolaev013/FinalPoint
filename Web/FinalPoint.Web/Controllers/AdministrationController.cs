﻿namespace FinalPoint.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using FinalPoint.Common;
    using FinalPoint.Data.Models;
    using FinalPoint.Web.Business.Interfaces;
    using FinalPoint.Web.ViewModels;
    using FinalPoint.Web.ViewModels.Administration;
    using FinalPoint.Web.ViewModels.DTOs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + ", " + GlobalConstants.OwnerRoleName + ", " + GlobalConstants.OfficeOwnerRoleName)]
    public class AdministrationController : BaseController
    {
        private readonly IOfficeService officeService;
        private readonly ICityService cityService;
        private readonly IUserService userService;
        private readonly IThemeService themeService;

        public AdministrationController(
            IOfficeService officeService,
            ICityService cityService,
            IUserService userService,
            IThemeService themeService)
        {
            this.officeService = officeService;
            this.cityService = cityService;
            this.userService = userService;
            this.themeService = themeService;
        }

        public IActionResult Index(Result model)
        {
            return this.View(model);
        }

        public IActionResult FireEmployee()
        {
            FireEmployeeInputModel model = new FireEmployeeInputModel();

            this.LoadFireEmployeeViewModel(model);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FireEmployee(FireEmployeeInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var removedUser = await this.userService
                        .RemoveUserAsync(input.EmployeeToFire);

                if (removedUser != null)
                {
                    input.ResultMessage = $"Служител - {removedUser.FirstName} {removedUser.MiddleName} {removedUser.LastName} - {removedUser.PersonalId} - беше уволнен успешно.";
                }
            }

            this.LoadFireEmployeeViewModel(input);

            return this.View(input);
        }

        public IActionResult AddOffice()
        {
            AddOfficeInputModel model = new AddOfficeInputModel();
            this.LoadAddOfficeViewModel(model);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOffice(AddOfficeInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.officeService.CreateAsync(input);

                var result = new Result() { Success = true, Message = input.ResultMessage = $"Офис {input.Name} беше регистриран успешно!" };

                return this.RedirectToAction("Index", result);
            }

            this.LoadAddOfficeViewModel(input);
            return this.View(input);
        }

        public IActionResult RemoveOffice()
        {
            RemoveOfficeInputModel model = new RemoveOfficeInputModel();
            model.AvailableOfficesToRemove = this.officeService.GetOnlyAllOfficesAsKeyValueParis();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveOffice(RemoveOfficeInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var removedOffice = await this.officeService.RemoveAsync(input.OfficeToRemove);
                input.ResultMessage = $"Офис - {removedOffice.Name} - {removedOffice.PostCode} - беше прекратен успешно.";
            }

            input.AvailableOfficesToRemove = this.officeService.GetOnlyAllOfficesAsKeyValueParis();

            return this.View(input);
        }

        public IActionResult Settings()
        {
            var model = this.CreateSettingsViewModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(SettingsInputModel input)
        {
            Result vm = null;

            if (this.ModelState.IsValid)
            {
                var officeId = this.userService.GetUserOfficeByClaimsPrincipal(this.User);

                var result = await this.officeService.ChangeOfficeThemeAsync(officeId, input.SelectedThemeId);

                if (result)
                {
                    vm = new Result() { Success = true, Message = "Темата беше зададена успешно." };
                }
            }

            if (vm == null)
            {
                vm = new Result() { Success = false, Message = GlobalErrorMessages.WeHadAProblemPleaseLogInAgain };
            }

            return this.RedirectToAction("Index", vm);
        }

        private SettingsInputModel CreateSettingsViewModel()
        {
            var model = new SettingsInputModel();
            model.AvailableThemes = this.themeService
                    .GetAllThemesAsKeyValuePair()
                    .ToList();

            return model;
        }

        private void LoadAddOfficeViewModel(AddOfficeInputModel input)
        {
            input.CitiesItems = this.cityService.GetAllCitiesAsKeyValuePairs();
            input.SortingCentersItems = this.officeService.GetAllSortingCentersAsKeyValuePairs();
            input.AllUsers = this.userService.GetAllUsersAsKeyValuePair();
        }

        private void LoadFireEmployeeViewModel(FireEmployeeInputModel input)
        {
            var currUserId = this.User
                            .FindFirst(ClaimTypes.NameIdentifier)
                            .Value;

            input.AvailableEmployeesToDelete = this.userService.GetAllUsersWithoutCurrentAsKeyValuePair(currUserId);
        }
    }
}
