using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Add_Comment_Reply_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    entity_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entity_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    commenter_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    commenter_email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    text = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    approved_at = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    approved_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_comments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "replies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    replier_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    replier_email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    text = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    approved_at = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    approved_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    comment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_replies", x => x.id);
                    table.ForeignKey(
                        name: "comments_replies_fk",
                        column: x => x.comment_id,
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "comments_replies_index",
                table: "replies",
                column: "comment_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "replies");

            migrationBuilder.DropTable(
                name: "comments");
        }
    }
}
