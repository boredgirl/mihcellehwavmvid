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
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_userid", item => item.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationsite",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Description = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Brandmark = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Favicon = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_siteid", item => item.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationtenant",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Siteid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Databaseconnectionstring = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_tenantid", item => item.Id);
                     dbtable.ForeignKey("fk_application_tenantid_siteid", item => item.Siteid, "Applicationtenant");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationpage",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Siteid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Urlpath = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Name = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Isnavigation = dbtable.Column<bool>(type: "bit", nullable: false, unicode: null),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_pageid", item => item.Id);
                     dbtable.ForeignKey("fk_application_siteid", item => item.Siteid, "Applicationsite");
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationmodule",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Createdon = dbtable.Column<DateTime>(type: "date", nullable: false, unicode: null),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_moduleid", item => item.Id);
                 });

            migrationbuilder.CreateTable(
                 name: "Applicationpagemodule",
                 columns: dbtable => new
                 {
                     Id = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Pageid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                     Moduleid = dbtable.Column<string>(type: "nvarchar", nullable: false, unicode: null, maxLength: 410),
                 },
                 constraints: dbtable =>
                 {
                     dbtable.PrimaryKey("pk_application_pagemoduleid", item => item.Id);
                     dbtable.ForeignKey("fk_application_pagedid_pagemoduleid", item => item.Pageid, "Applicationpage");
                     dbtable.ForeignKey("fk_application_moduleid_pagemoduleid", item => item.Moduleid, "Applicationmodule");
                 });

        }

        protected override void Down(MigrationBuilder migrationbuilder)
        {

            migrationbuilder.DropColumn("Applicationpagemodule", "Applicationpagemodule");
            migrationbuilder.DropColumn("Applicationmodule", "Applicationmodule");
            migrationbuilder.DropColumn("Applicationpage", "Applicationpage");
            migrationbuilder.DropColumn("Applicationtenant", "Applicationtenant");
            migrationbuilder.DropColumn("Applicationsite", "Applicationsite");
            migrationbuilder.DropColumn("Applicationuser", "Applicationuser");

        }
    }
}
