using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Created_Time",
                table: "Posts",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Posts",
                newName: "Created_Time");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Posts",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
