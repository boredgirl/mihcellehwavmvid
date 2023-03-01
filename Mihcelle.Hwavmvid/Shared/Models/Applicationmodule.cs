using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationmodule : Applicationbase
    {

        [StringLength(410)]
        public string Packageid { get; set; }
        [StringLength(410)]
        public string Containercolumnid { get; set; }
        public int Containercolumnposition { get; set; }

    }
}
