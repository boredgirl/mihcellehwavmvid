namespace Mihcelle.Hwavmvid.Server
{
    public interface Moduleinstallerinterface : IServiceProvider
    {

        Task Install();
        Task Deinstall();

    }
}
