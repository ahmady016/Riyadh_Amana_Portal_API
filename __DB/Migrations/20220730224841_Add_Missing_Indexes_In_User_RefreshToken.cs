using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amana_mono.__DB.Migrations
{
    public partial class Add_Missing_Indexes_In_User_RefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens",
                newName: "refresh_tokens_user_id_index");

            migrationBuilder.CreateIndex(
                name: "users_email_unique_index",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "refresh_tokens_value_unique_index",
                table: "refresh_tokens",
                column: "value",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "users_email_unique_index",
                table: "users");

            migrationBuilder.DropIndex(
                name: "refresh_tokens_value_unique_index",
                table: "refresh_tokens");

            migrationBuilder.RenameIndex(
                name: "refresh_tokens_user_id_index",
                table: "refresh_tokens",
                newName: "IX_refresh_tokens_user_id");
        }
    }
}
