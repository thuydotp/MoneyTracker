using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTracker.Migrations
{
    public partial class AddDescriptionPropertyToSpendingItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SpendingItems",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SpendingItems");
        }
    }
}
