using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Add_News_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    title_en = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    source_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    source_en = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    brief_ar = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    brief_en = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    content_ar = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    content_en = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    image_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    thumb_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    hijri_date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    tags = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    is_in_home = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
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
                    table.PrimaryKey("PK_news", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "news");
        }
    }
}
