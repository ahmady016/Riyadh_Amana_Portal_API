using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Add_Partners_TPH_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "partners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    partner_title_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    partner_title_en = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    description_ar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    description_en = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    icon_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    discriminator = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    partnership_title_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    partnership_title_en = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    rm_department_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    rm_department_en = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    contract_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active_contract = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
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
