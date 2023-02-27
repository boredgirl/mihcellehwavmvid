using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Shared.Models
{
    public class Applicationcontainer : Applicationbase
    {


        public string Pageid { get; set; }
        public string Containertype { get; set; }

        [NotMapped] public List<Applicationcontainercolumn>? Columns { get; set; }

    }
}
