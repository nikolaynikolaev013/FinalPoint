// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalPoint.Web.Controllers
{
    using System;

    using FinalPoint.Data.Models.Enums;
    using FinalPoint.Web.ViewModels.DTOs.LoadUnload;
    using FinalPoint.Web.ViewModels.LoadUnload;
    using FinalPoint.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class LoadUnloadController : Controller
    {
        // GET: /<controller>/
        public IActionResult Load(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                LoadUnloadIndexViewModel model = new LoadUnloadIndexViewModel();
                model.Type = ProtocolType.Loading;
                model.Lines.Add("Белослав");
                model.Lines.Add("Цветен квартал");
                return this.View("LoadUnload", model);
            }
            else
            {
                LoadUnloadProtocolViewModel model = new LoadUnloadProtocolViewModel();
                model.ParcelInsertViewModel = new ParcelINsertPartialViewModel { ButtonText = "Добавяне", Controller = null };
                model.Id = "12321";
                model.Line = line;
                model.Type = ProtocolType.Loading;
                model.Date = new DateTime(2020, 05, 05);
                model.Parcels.Add(new ParcelDto { Id = "12322", Parts = 3, Status = "Проверена" });
                model.Parcels.Add(new ParcelDto { Id = "3432", Parts = 1, Status = "Непроверена" });
                model.Parcels.Add(new ParcelDto { Id = "456", Parts = 20, Status = "Добавена" });
                model.Parcels.Add(new ParcelDto { Id = "456", Parts = 20, Status = "Добавена" });
                return this.View("LoadUnloadProtocol", model);
            }
        }

        public IActionResult Unload(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                LoadUnloadIndexViewModel model = new LoadUnloadIndexViewModel();
                model.Type = ProtocolType.Unloading;
                model.Lines.Add("Христо Ботев");
                model.Lines.Add("Спартак");
                return this.View("LoadUnload", model);
            }
            else
            {
                LoadUnloadProtocolViewModel model = new LoadUnloadProtocolViewModel();
                model.ParcelInsertViewModel = new ParcelINsertPartialViewModel { ButtonText = "Добавяне", Controller = null };
                model.Id = "12321";
                model.Line = line;
                model.Type = ProtocolType.Unloading;
                model.Date = new DateTime(2020, 05, 05);
                model.Parcels.Add(new ParcelDto { Id = "12322", Parts = 3, Status = "Проверена" });
                model.Parcels.Add(new ParcelDto { Id = "3432", Parts = 1, Status = "Непроверена" });
                model.Parcels.Add(new ParcelDto { Id = "456", Parts = 20, Status = "Добавена" });
                model.Parcels.Add(new ParcelDto { Id = "456", Parts = 20, Status = "Добавена" });
                return this.View("LoadUnloadProtocol", model);
            }
        }
    }
}
