using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Add_Document_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    title_en = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description_en = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    url = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true),
                    base64_url = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
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
                    table.PrimaryKey("PK_documents", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documents");
        }
    }
}
