using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Add_Partner_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "partners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    partnership_title_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    partnership_title_en = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    partner_title_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    partner_title_en = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    content_ar = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    content_en = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    partner_address_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    partner_address_en = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    rm_department_en = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    rm_department_ar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    icon_url = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true),
                    icon_base64_url = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    contract_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active_contract = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
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
                    table.PrimaryKey("PK_partners", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "partners");
        }
    }
}
