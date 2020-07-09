using Microsoft.EntityFrameworkCore.Migrations;

namespace MakeATrinkspruch.Data.Migrations
{
    public partial class ReviewedTagForToast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Reviewed",
                table: "Toasts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reviewed",
                table: "Toasts");
        }
    }
}