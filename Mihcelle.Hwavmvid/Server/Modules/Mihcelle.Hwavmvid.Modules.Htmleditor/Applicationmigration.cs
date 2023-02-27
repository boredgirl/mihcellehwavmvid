using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Modules.Htmleditor
{

    [DbContext(typeof(Applicationdbcontext))]
    [Migration("mihcelle.hwavmvid.modules.htmleditor.01.00.00.00")]
    public class Applicationmigration : Migration
    {

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            base.BuildTargetModel(modelBuilder);
        }

        protected override void Up(MigrationBuilder migrationbuilder)
        {

            migrationbuilder.CreateTable(
                 name: "Applicationhtmleditors",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Htmlstring = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_htmleditorid", item => item.Id);
                 });
        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {

            migrationbuilder.DropColumn("Applicationhtmleditors", "Applicationhtmleditors");

        }
    }
}
