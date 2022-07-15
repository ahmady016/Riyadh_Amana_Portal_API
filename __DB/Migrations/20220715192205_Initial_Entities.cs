using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Initial_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "advertisements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title_ar = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    title_en = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    content_ar = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    content_en = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    url = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    image_url = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    order = table.Column<int>(type: "int", nullable: true),
                    is_home_slider = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    is_popup = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "app_dev"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    activated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    activated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title_ar = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    title_en = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    content_ar = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    content_en = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    url = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "app_dev"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    activated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    activated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    title_en = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    source_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    source_en = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    brief_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    brief_en = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    content_ar = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    content_en = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    image_url = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    thumb_url = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    hijri_date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    tags = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    is_in_home = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "app_dev"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    activated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    activated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_news", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    first_name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    gender = table.Column<byte>(type: "tinyint", nullable: false),
                    birth_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    national_id = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    mobile = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "app_dev"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    activated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    activated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advertisements");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
