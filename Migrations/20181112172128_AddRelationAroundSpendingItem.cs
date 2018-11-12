using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTracker.Migrations
{
    public partial class AddRelationAroundSpendingItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SpendingItems_CategoryID",
                table: "SpendingItems",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SpendingItems_SpendingAccountID",
                table: "SpendingItems",
                column: "SpendingAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_SpendingItems_Categories_CategoryID",
                table: "SpendingItems",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpendingItems_Accounts_SpendingAccountID",
                table: "SpendingItems",
                column: "SpendingAccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpendingItems_Categories_CategoryID",
                table: "SpendingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SpendingItems_Accounts_SpendingAccountID",
                table: "SpendingItems");

            migrationBuilder.DropIndex(
                name: "IX_SpendingItems_CategoryID",
                table: "SpendingItems");

            migrationBuilder.DropIndex(
                name: "IX_SpendingItems_SpendingAccountID",
                table: "SpendingItems");
        }
    }
}
