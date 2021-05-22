﻿namespace FinalPoint.Web.ViewModels.LoadUnload
{
    using System;
    using System.Collections.Generic;
    using FinalPoint.Data.Models.Enums;

    public class LoadUnloadIndexViewModel
    {
        public LoadUnloadIndexViewModel()
        {
        }

        public ProtocolType Type { get; set; }

        public int LineToLoad { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Lines { get; set; }
    }
}
