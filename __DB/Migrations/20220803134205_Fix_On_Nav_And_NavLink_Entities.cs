using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Fix_On_Nav_And_NavLink_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "nav_id",
                table: "navs_links",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "navs_links_title_ar_unique_index",
                table: "navs_links",
                column: "title_ar",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "navs_links_title_en_unique_index",
                table: "navs_links",
                column: "title_en",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "navs_links_title_ar_unique_index",
                table: "navs_links");

            migrationBuilder.DropIndex(
                name: "navs_links_title_en_unique_index",
                table: "navs_links");

            migrationBuilder.AlterColumn<Guid>(
                name: "nav_id",
                table: "navs_links",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
