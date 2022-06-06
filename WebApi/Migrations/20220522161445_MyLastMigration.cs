using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class MyLastMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "User",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "User",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "SuperHeroes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "SuperHeroes");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "User",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "userId");
        }
    }
}
