using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationmodulepackage : Applicationbase
    {

        public string Name { get; set; }
        public string Version { get; set; }
        public string Assemblytype { get; set; }
        public string Description { get; set; }        

    }
}
