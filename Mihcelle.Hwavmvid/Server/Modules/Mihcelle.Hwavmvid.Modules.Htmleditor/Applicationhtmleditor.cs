using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mihcelle.Hwavmvid.Modules.Htmleditor
{
    public class Applicationhtmleditor
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Moduleid { get; set; }
        public string Htmlstring { get; set; }
        public DateTime Createdon { get; set; }

    }
}
