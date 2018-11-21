using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTracker.Migrations
{
    public partial class AddMissingProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpendingItems_Categories_CategoryID",
                table: "SpendingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SpendingItems_Accounts_SpendingAccountID",
                table: "SpendingItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpendingItems",
                table: "SpendingItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "SpendingItems",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "SpendingAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_SpendingItems_SpendingAccountID",
                table: "Transactions",
                newName: "IX_Transactions_SpendingAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_SpendingItems_CategoryID",
                table: "Transactions",
                newName: "IX_Transactions_CategoryID");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecordDate",
                table: "Transactions",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpendingAccounts",
                table: "SpendingAccounts",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryID",
                table: "Transactions",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_SpendingAccounts_SpendingAccountID",
                table: "Transactions",
                column: "SpendingAccountID",
                principalTable: "SpendingAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryID",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_SpendingAccounts_SpendingAccountID",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpendingAccounts",
                table: "SpendingAccounts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RecordDate",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "SpendingItems");

            migrationBuilder.RenameTable(
                name: "SpendingAccounts",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SpendingAccountID",
                table: "SpendingItems",
                newName: "IX_SpendingItems_SpendingAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CategoryID",
                table: "SpendingItems",
                newName: "IX_SpendingItems_CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpendingItems",
                table: "SpendingItems",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "ID");

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
    }
}
