namespace Mihcelle.Hwavmvid.Pager
{
    public class Pagerevent<TPagerItem>
    {

        public int ApiQueryId { get; set; }
        public TPagerItem Item { get; set; }

    }
}
