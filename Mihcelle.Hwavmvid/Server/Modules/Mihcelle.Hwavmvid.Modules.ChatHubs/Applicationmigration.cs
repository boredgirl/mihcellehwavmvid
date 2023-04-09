﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;
using Mihcelle.Hwavmvid.Shared;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs
{

    [DbContext(typeof(Applicationdbcontext))]
    [Migration("mihcelle.hwavmvid.modules.chathubs.01.00.00.00")]
    public class Applicationmigration : Migration
    {

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            base.BuildTargetModel(modelBuilder);
        }

        protected override void Up(MigrationBuilder migrationbuilder)
        {


            migrationbuilder.CreateTable(
                 name: "ChatHubUser",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     UserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     FrameworkUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     UserType = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubUser", item => item.UserId);
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubRoom",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ModuleId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     CreatorId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Title = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Content = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     BackgroundColor = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ImageUrl = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     SnapshotUrl = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Type = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Status = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     OneVsOneId = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubRoom", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubRoom_Module", item => item.ModuleId, "Applicationmodules", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubRoomChatHubUser",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubRoomChatHubUser", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubUser_ChatHubUser", item => item.ChatHubUserId, "Applicationusers", "Id");
                     dbtable.ForeignKey("FK_ChatHubRoomChatHubUser_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubMessage",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Content = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Type = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubMessage", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubMessage_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                     dbtable.ForeignKey("FK_ChatHubMessage_User", item => item.ChatHubUserId, "Applicationusers", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubConnection",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ConnectionId = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     IpAddress = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     UserAgent = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Status = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubConnection", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubConnection_ChatHubUser", item => item.ChatHubUserId, "ChatHubUser", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubPhoto",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubMessageId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Source = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Thumb = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Caption = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Size = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Width = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Height = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubPhoto", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubPhoto_ChatHubMessage", item => item.ChatHubMessageId, "ChatHubMessage", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubSetting",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     UsernameColor = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     MessageColor = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubSetting", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubSetting_User", item => item.ChatHubUserId, "Applicationusers", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubDevice",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubRoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     UserAgent = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     Type = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     DefaultDeviceId = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     DefaultDeviceName = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubSetting", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubDevice_ChatHubUser", item => item.ChatHubUserId, "ChatHubUser", "Id");
                     dbtable.ForeignKey("FK_ChatHubDevice_ChatHubRoom", item => item.ChatHubRoomId, "ChatHubRoom", "Id");
                 });

            migrationbuilder.CreateTable(
                 name: "ChatHubInvitation",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     ChatHubUserId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     RoomId = dbtable.Column<string>(type: "nvarchar(410)", nullable: false, unicode: null),
                     Hostname = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     CreatedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     CreatedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                     ModifiedOn = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                     ModifiedBy = dbtable.Column<string>(type: "nvarchar(841)", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("PK_ChatHubInvitation", item => item.Id);
                     dbtable.ForeignKey("FK_ChatHubInvitation_User", item => item.ChatHubUserId, "Applicationusers", "Id");
                 });

        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {
            migrationbuilder.DropColumn("Applicationchathubs", "Applicationchathubs");
        }
    }
}
