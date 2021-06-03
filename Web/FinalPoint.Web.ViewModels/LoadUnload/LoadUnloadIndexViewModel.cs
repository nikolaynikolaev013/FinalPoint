namespace FinalPoint.Web.ViewModels.LoadUnload
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FinalPoint.Data.Models.Enums;

    public class LoadUnloadIndexViewModel
    {
        public LoadUnloadIndexViewModel()
        {
        }

        public ProtocolType Type { get; set; }

        public string TranslatedType { get; set; }

        [Required]
        public int LineToLoad { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Lines { get; set; }
    }
}
