using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Add_Nav_And_NavLink_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "navs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    title_en = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description_en = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    icon_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "app_dev"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    deleted_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    activated_at = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    activated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_navs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "navs_links",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    title_en = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description_en = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    nav_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "app_dev"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    deleted_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    activated_at = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    activated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_navs_links", x => x.id);
                    table.ForeignKey(
                        name: "navs_navs_links_fk",
                        column: x => x.nav_id,
                        principalTable: "navs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "navs_links_nav_index",
                table: "navs_links",
                column: "nav_id");

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
            migrationBuilder.DropTable(
                name: "navs_links");

            migrationBuilder.DropTable(
                name: "navs");
        }
    }
}
