﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationpage : Applicationbase
    {

        public string Siteid { get; set; }
        public string Pagemoduleid { get; set; }
        public string Urlpath { get; set; }
        public string Name { get; set; }
        public bool Isnavigation { get; set; }

    }
}
