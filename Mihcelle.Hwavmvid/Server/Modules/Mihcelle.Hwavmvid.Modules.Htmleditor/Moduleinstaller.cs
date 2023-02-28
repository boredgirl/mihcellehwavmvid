using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Modules.Htmleditor;

namespace Mihcelle.Hwavmvid.Server.Modules.Htmleditor
{
    public class Moduleinstaller : Mihcelle.Hwavmvid.Server.Moduleinstallerinterface
    {


        public Applicationdbcontext _dbcontext { get; set; }

        public Moduleinstaller(Applicationdbcontext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public async Task Install()
        {
            await this._dbcontext.Database.MigrateAsync();
        }

        public async Task Deinstall()
        {
            await this._dbcontext.Database.RollbackTransactionAsync();
        }

    }
}
