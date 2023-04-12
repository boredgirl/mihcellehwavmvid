﻿using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mihcelle.Hwavmvid.Server;
using Mihcelle.Hwavmvid.Shared.Models;
using Oqtane.ChatHubs.Models;
using System.Reflection;
using System.Reflection.Emit;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Discriminator Oqtane User Model
            //modelBuilder.Entity<User>().HasDiscriminator<string>("UserType").HasValue<User>("User").HasValue<ChatHubUser>("ChatHubUser");

            modelBuilder.Entity<Applicationuser>().ToTable<Applicationuser>("Applicationusers");
            modelBuilder.Entity<ChatHubUser>().HasBaseType<Applicationuser>().ToTable<ChatHubUser>("ChatHubUser");

            // Relations
            // Many-to-many
            // ChatHubRoom / ChatHubUser
            modelBuilder.Entity<ChatHubRoomChatHubUser>()
                .HasKey(item => new { item.ChatHubRoomId, item.ChatHubUserId });

            modelBuilder.Entity<ChatHubRoomChatHubUser>()
                .HasOne(room_user => room_user.Room)
                .WithMany(room => room.RoomUsers)
                .HasForeignKey(room_user => room_user.ChatHubRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatHubRoomChatHubUser>()
                .HasOne(room_user => room_user.User)
                .WithMany(user => user.UserRooms)
                .HasForeignKey(room_user => room_user.ChatHubUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubMessage / ChatHubRoom
            modelBuilder.Entity<ChatHubMessage>()
                .HasOne(m => m.Room)
                .WithMany(r => r.Messages)
                .HasForeignKey(m => m.ChatHubRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubConnection / ChatHubUser
            modelBuilder.Entity<ChatHubUser>()
                .HasMany(u => u.Connections)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.ChatHubUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubDevice / ChatHubUser
            modelBuilder.Entity<ChatHubUser>()
                .HasMany(u => u.Devices)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.ChatHubUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubDevice / ChatHubRoom
            modelBuilder.Entity<ChatHubRoom>()
                .HasMany(u => u.Devices)
                .WithOne(r => r.Room)
                .HasForeignKey(d => d.ChatHubRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubGeolocation / ChatHubConnection
            modelBuilder.Entity<ChatHubGeolocation>()
                .HasOne(g => g.Connection)
                .WithMany(c => c.Geolocations)
                .HasForeignKey(g => g.ChatHubConnectionId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubMessage / ChatHubPhotos
            modelBuilder.Entity<ChatHubPhoto>()
                .HasOne(p => p.Message)
                .WithMany(m => m.Photos)
                .HasForeignKey(p => p.ChatHubMessageId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubUser / ChatHubIgnore
            modelBuilder.Entity<ChatHubIgnore>()
                .HasOne(i => i.User)
                .WithMany(u => u.Ignores)
                .HasForeignKey(i => i.ChatHubUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-one
            // ChatHubSetting / ChatHubUser
            modelBuilder.Entity<ChatHubSettings>()
                .HasOne(s => s.User)
                .WithOne(u => u.Settings)
                .HasForeignKey<ChatHubSettings>(s => s.ChatHubUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-Many
            // ChatHubInvitation / ChatHubUser
            modelBuilder.Entity<ChatHubInvitation>()
                .HasOne(i => i.User)
                .WithMany(u => u.Invitations)
                .HasForeignKey(i => i.ChatHubUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubRoom / ChatHubCam
            modelBuilder.Entity<ChatHubCam>()
                .HasOne(c => c.Room)
                .WithMany(r => r.Cams)
                .HasForeignKey(c => c.ChatHubRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubCamSequence / ChatHubCam
            modelBuilder.Entity<ChatHubCamSequence>()
                .HasOne(s => s.Cam)
                .WithMany(c => c.Sequences)
                .HasForeignKey(s => s.ChatHubCamId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation
            // One-to-many
            // ChatHubCam / ChatHubConnection
            modelBuilder.Entity<ChatHubCam>()
                .HasOne(c => c.Connection)
                .WithMany(c => c.Cams)
                .HasForeignKey(c => c.ChatHubConnectionId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relations
            // Many-to-many
            // ChatHubRoom / ChatHubModerator
            modelBuilder.Entity<ChatHubRoomChatHubModerator>()
                .HasKey(item => new { item.ChatHubRoomId, item.ChatHubModeratorId });

            modelBuilder.Entity<ChatHubRoomChatHubModerator>()
                .HasOne(room_moderator => room_moderator.Room)
                .WithMany(room => room.RoomModerators)
                .HasForeignKey(room_moderator => room_moderator.ChatHubRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatHubRoomChatHubModerator>()
                .HasOne(room_moderator => room_moderator.Moderator)
                .WithMany(moderator => moderator.ModeratorRooms)
                .HasForeignKey(room_moderator => room_moderator.ChatHubModeratorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relations
            // Many-to-many
            // ChatHubRoom / ChatHubWhitelistUser
            modelBuilder.Entity<ChatHubRoomChatHubWhitelistUser>()
                .HasKey(item => new { item.ChatHubRoomId, item.ChatHubWhitelistUserId });

            modelBuilder.Entity<ChatHubRoomChatHubWhitelistUser>()
                .HasOne(room_whitelistuser => room_whitelistuser.Room)
                .WithMany(room => room.RoomWhitelistUsers)
                .HasForeignKey(room_whitelistuser => room_whitelistuser.ChatHubRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatHubRoomChatHubWhitelistUser>()
                .HasOne(room_whitelistuser => room_whitelistuser.WhitelistUser)
                .WithMany(whitelistuser => whitelistuser.WhitelistUserRooms)
                .HasForeignKey(room_whitelistuser => room_whitelistuser.ChatHubWhitelistUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relations
            // Many-to-many
            // ChatHubRoom / ChatHubBlacklistUser
            modelBuilder.Entity<ChatHubRoomChatHubBlacklistUser>()
                .HasKey(item => new { item.ChatHubRoomId, item.ChatHubBlacklistUserId });

            modelBuilder.Entity<ChatHubRoomChatHubBlacklistUser>()
                .HasOne(room_blacklistuser => room_blacklistuser.Room)
                .WithMany(room => room.RoomBlacklistUsers)
                .HasForeignKey(room_blacklistuser => room_blacklistuser.ChatHubRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatHubRoomChatHubBlacklistUser>()
                .HasOne(room_blacklistuser => room_blacklistuser.BlacklistUser)
                .WithMany(blacklistuser => blacklistuser.BlacklistUserRooms)
                .HasForeignKey(room_blacklistuser => room_blacklistuser.ChatHubBlacklistUserId)
                .OnDelete(DeleteBehavior.NoAction);

            try { base.OnModelCreating(modelBuilder); } catch { }
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
