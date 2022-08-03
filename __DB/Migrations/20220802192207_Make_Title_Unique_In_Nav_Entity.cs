using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Make_Title_Unique_In_Nav_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "navs_title_ar_unique_index",
                table: "navs",
                column: "title_ar",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "navs_title_en_unique_index",
                table: "navs",
                column: "title_en",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "navs_title_ar_unique_index",
                table: "navs");

            migrationBuilder.DropIndex(
                name: "navs_title_en_unique_index",
                table: "navs");
        }
    }
}
