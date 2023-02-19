using System.Collections.Generic;

namespace Mihcelle.Hwavmvid.Pager
{
    public class PagerApiItem<TItemGeneric>
    {

        public List<TItemGeneric> Items { get; set; }
        public int Pages { get; set; }

    }
}
