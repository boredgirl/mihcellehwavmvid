namespace Mihcelle.Hwavmvid
{
    public partial class Programpartial : Mihcelle.Hwavmvid.Programextended
    {

        public override void Configure(IServiceCollection services)
        {

            //services.AddDbContext<Mihcelle.Hwavmvid.Modules.Htmleditor.Applicationdbcontext, Mihcelle.Hwavmvid.Modules.Htmleditor.Applicationdbcontext>();
            base.Configure(services);
        }

    }
}
