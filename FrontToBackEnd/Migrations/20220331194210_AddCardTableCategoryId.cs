using Microsoft.EntityFrameworkCore.Migrations;

namespace FrontToBackEnd.Migrations
{
    public partial class AddCardTableCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CategoryId",
                table: "Cards",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Categories_CategoryId",
                table: "Cards",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Categories_CategoryId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CategoryId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Cards");
        }
    }
}
