namespace FinalPoint.Web.ViewModels.LoadUnload
{
    using System;
    using System.Collections.Generic;
    using FinalPoint.Data.Models.Enums;

    public class LoadUnloadIndexViewModel
    {
        public LoadUnloadIndexViewModel()
        {
            this.Lines = new HashSet<string>();
        }

        public ProtocolType Type { get; set; }

        public ICollection<string> Lines { get; set; }
    }
}
