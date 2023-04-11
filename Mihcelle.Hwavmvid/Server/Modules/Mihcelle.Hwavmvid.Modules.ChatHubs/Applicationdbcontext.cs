using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mihcelle.Hwavmvid.Server;
using Mihcelle.Hwavmvid.Shared.Models;
using Oqtane.ChatHubs.Models;
using System.Reflection;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs
{

    public class Applicationdbcontext : Mihcelle.Hwavmvid.Server.Data.Applicationdbcontext, Moduleinstallerinterface
    {

        public virtual DbSet<ChatHubUser> ChatHubUser { get; set; }
        public virtual DbSet<ChatHubRoom> ChatHubRoom { get; set; }
        public virtual DbSet<ChatHubRoomChatHubUser> ChatHubRoomChatHubUser { get; set; }
        public virtual DbSet<ChatHubMessage> ChatHubMessage { get; set; }
        public virtual DbSet<ChatHubConnection> ChatHubConnection { get; set; }
        public virtual DbSet<ChatHubPhoto> ChatHubPhoto { get; set; }
        public virtual DbSet<ChatHubSettings> ChatHubSetting { get; set; }
        public virtual DbSet<ChatHubDevice> ChatHubDevice { get; set; }
        public virtual DbSet<ChatHubInvitation> ChatHubInvitation { get; set; }
        public virtual DbSet<ChatHubCam> ChatHubCam { get; set; }
        public virtual DbSet<ChatHubCamSequence> ChatHubCamSequence { get; set; }
        public virtual DbSet<ChatHubIgnore> ChatHubIgnore { get; set; }
        public virtual DbSet<ChatHubModerator> ChatHubModerator { get; set; }
        public virtual DbSet<ChatHubRoomChatHubModerator> ChatHubRoomChatHubModerator { get; set; }
        public virtual DbSet<ChatHubWhitelistUser> ChatHubWhitelistUser { get; set; }
        public virtual DbSet<ChatHubRoomChatHubWhitelistUser> ChatHubRoomChatHubWhitelistUser { get; set; }
        public virtual DbSet<ChatHubBlacklistUser> ChatHubBlacklistUser { get; set; }
        public virtual DbSet<ChatHubRoomChatHubBlacklistUser> ChatHubRoomChatHubBlacklistUser { get; set; }
        public virtual DbSet<ChatHubGeolocation> ChatHubGeolocation { get; set; }


        public Applicationdbcontext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            try { base.OnModelCreating(builder); } catch { }
        }

        public async Task Removemodule(string moduleid)
        {
            await this.ChatHubRoom.Where(item => item.ModuleId == moduleid).ExecuteDeleteAsync();
            await this.SaveChangesAsync();            
        }

        public async Task Install()
        {
            try
            {
                await this.Database.MigrateAsync();
            }
            catch (Exception message)
            {
                Console.WriteLine(message);
            }
        }

        public async Task Deinstall()
        {
            await this.Database.RollbackTransactionAsync();
        }
        
        public Applicationmodulepackage applicationmodulepackage 
        {
            get
            {
                var package = new Applicationmodulepackage()
                {
                    Name = "ChatHubs",
                    Version = "1.0.0",
                    Assemblytype = "Mihcelle.Hwavmvid.Modules.ChatHubs.Index, Mihcelle.Hwavmvid.Client",
                    Description = string.Empty,
                    Createdon = DateTime.Now,
                };

                return package;
            }
        }

    }
}
