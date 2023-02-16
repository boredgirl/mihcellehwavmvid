using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Migrations
{

    [DbContext(typeof(Applicationdbcontext))]
    [Migration("mihcelle.hwavmvid.01.00.00.00")]
    public class Applicationmigration : Migration
    {

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            base.BuildTargetModel(modelBuilder);
        }

        protected override void Up(MigrationBuilder migrationbuilder)
        {
            migrationbuilder.CreateTable(
                 name: "Applicationuser",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_applicationuserid", x => x.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationsite",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_applicationsiteid", x => x.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationtenant",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Databaseconnectionstring = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_applicationtenantid", x => x.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationpage",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Siteid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Url = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_applicationpageid", x => x.Id);
                 });
        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {
            migrationbuilder.DropColumn("Applicationuser", "Applicationuser");
            migrationbuilder.DropColumn("Applicationsite", "Applicationsite");
            migrationbuilder.DropColumn("Applicationtenant", "Applicationtenant");
            migrationbuilder.DropColumn("Applicationpage", "Applicationpage");
        }
    }
}
