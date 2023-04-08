using Microsoft.EntityFrameworkCore;
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

        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {
            migrationbuilder.DropColumn("Applicationchathubs", "Applicationchathubs");
        }
    }
}
