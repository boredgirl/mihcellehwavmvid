using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
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
                     Id = dbtable.Column<int>(type: "int", nullable: false),
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("pk_applicationuserid", x => x.Id);
                 });
        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {
            migrationbuilder.DropColumn("Applicationuser", "Applicationuser");
        }
    }
}
