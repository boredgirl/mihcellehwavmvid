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
                 name: "Applicationusers",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_userid", item => item.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationsites",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Description = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Brandmark = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Favicon = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_siteid", item => item.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationtenants",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Siteid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Databaseconnectionstring = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_tenantid", item => item.Id);
                     dbtable.ForeignKey("fk_application_tenant_siteid", item => item.Siteid, "Applicationtenants");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationpages",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Siteid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Urlpath = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Isnavigation = dbtable.Column<bool>(type: "bit", nullable: false, unicode: null),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_pageid", item => item.Id);
                     dbtable.ForeignKey("fk_application_page_siteid", item => item.Siteid, "Applicationsites");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationcontainers",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Pageid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Containertype = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_containerid", item => item.Id);
                     dbtable.ForeignKey("fk_application_container_pageid", item => item.Pageid, "Applicationpages");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationcontainercolumns",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Containerid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Gridposition = dbtable.Column<int>(type: "int", nullable: false, unicode: null),
                     Columnwidth = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_containercolumnid", item => item.Id);
                     dbtable.ForeignKey("fk_application_containercolumn_containerid", item => item.Containerid, "Applicationcontainers");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationmodulepackages",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Siteid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Version = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Description = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_modulepackageid", item => item.Id);
                     dbtable.ForeignKey("fk_application_modulepackage_siteid", item => item.Siteid, "Applicationsites");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationmodules",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Packageid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Containercolumnid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 255),
                     Containercolumnposition = dbtable.Column<int>(type: "int", nullable: false, unicode: null),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_moduleid", item => item.Id);
                     dbtable.ForeignKey("fk_application_module_packageid", item => item.Packageid, "Applicationpackages");
                     dbtable.ForeignKey("fk_application_module_containercolumnid", item => item.Containercolumnid, "Applicationcontainercolumns");
                 });

        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {

            migrationbuilder.DropColumn("Applicationmodules", "Applicationmodules");
            migrationbuilder.DropColumn("Applicationmodulepackages", "Applicationmodulepackages");
            migrationbuilder.DropColumn("Applicationcontainercolumns", "Applicationcontainercolumns");
            migrationbuilder.DropColumn("Applicationcontainers", "Applicationcontainers");
            migrationbuilder.DropColumn("Applicationpages", "Applicationpages");
            migrationbuilder.DropColumn("Applicationtenants", "Applicationtenants");
            migrationbuilder.DropColumn("Applicationsites", "Applicationsites");
            migrationbuilder.DropColumn("Applicationusers", "Applicationusers");

        }
    }
}
