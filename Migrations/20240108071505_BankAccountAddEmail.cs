using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingMVCApp.Migrations
{
    /// <inheritdoc />
    public partial class BankAccountAddEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BankAccounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "BankAccounts");
        }
    }
}
