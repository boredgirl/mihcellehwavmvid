using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server
{
    public interface Moduleinstallerinterface
    {

        Task Install();
        Task Deinstall();
        Applicationmodulepackage applicationmodulepackage { get; }

    }
}
