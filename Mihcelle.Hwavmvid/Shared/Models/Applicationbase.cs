﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationbase
    {

        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

    }
}