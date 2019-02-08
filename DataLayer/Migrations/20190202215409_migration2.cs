using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Post_Category_Category_ID",
                table: "Post_Category");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Post_Category_Category_ID_Post_ID",
                table: "Post_Category",
                columns: new[] { "Category_ID", "Post_ID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Post_Category_Category_ID_Post_ID",
                table: "Post_Category");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Category_Category_ID",
                table: "Post_Category",
                column: "Category_ID");
        }
    }
}
