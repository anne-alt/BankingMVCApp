using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingMVCApp.Migrations
{
    /// <inheritdoc />
    public partial class BankAccountAddBalanceAndhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "BankAccounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionHistory",
                table: "BankAccounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "TransactionHistory",
                table: "BankAccounts");
        }
    }
}
