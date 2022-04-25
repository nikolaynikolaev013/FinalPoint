using System;
using System.Collections.Generic;
using System.Linq;
using FinalPoint.Common;
using FinalPoint.Data.Common.Repositories;
using FinalPoint.Data.Models;
using FinalPoint.Web.Business.Interfaces;

namespace FinalPoint.Web.Business.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IDeletableEntityRepository<Theme> themesRep;
        private readonly IUserService userService;
        private readonly IHttpFacade httpFacade;

        public ThemeService(
            IDeletableEntityRepository<FinalPoint.Data.Models.Theme> themesRep,
            IUserService userService,
            IHttpFacade httpFacade)
        {
            this.themesRep = themesRep;
            this.userService = userService;
            this.httpFacade = httpFacade;
        }

        public bool UpdateTheme()
        {
            var personalId = this.httpFacade.GetFromHttpContext(SessionKeys.PersonalId);

            if (personalId == null)
            {
                return false;
            }

            var themeId = this.userService
                .GetUserByPersonalId(int.Parse(personalId))?
                .WorkOffice
                .ThemeId;

            var themeName = this.themesRep
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Id == themeId)?
                .Name;

            if (themeId == null
                || themeName == null)
            {
                return false;
            }


            return this.httpFacade.AddToHttpContext(SessionKeys.ThemeName, themeName);
        }

        public string GetOfficeTheme()
        {
            var theme = this.httpFacade.GetFromHttpContext(SessionKeys.ThemeName);

            if (theme == null)
            {
                theme = this.themesRep
                    .AllAsNoTracking()
                    .FirstOrDefault()?
                    .Name;
            }

            return theme;
        }

        public List<Theme> GetAllThemes()
        {
            return this.themesRep
                .AllAsNoTracking()
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllThemesAsKeyValuePair()
        {
            return this.themesRep
                .AllAsNoTracking()
                .Select(x => new
                 {
                     x.Name,
                     x.Id,
                 }).ToList()
                 .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public FinalPoint.Data.Models.Theme GetThemeById(int id)
        {
            return this.themesRep
                .All()
                .FirstOrDefault(x => x.Id == id);
        }

    }
}

