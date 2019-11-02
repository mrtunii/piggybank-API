using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AddUserAndTransactionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2(7)", nullable: false, defaultValueSql: "getdate()"),
                    DateDeleted = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    Username = table.Column<string>(maxLength: 400, nullable: false),
                    Firstname = table.Column<string>(maxLength: 200, nullable: false),
                    Lastname = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: false),
                    Point = table.Column<int>(nullable: false, defaultValue: 0),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2(7)", nullable: false, defaultValueSql: "getdate()"),
                    DateDeleted = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    AccountNumber = table.Column<string>(maxLength: 400, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MerchantName = table.Column<string>(maxLength: 400, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    HasProcessed = table.Column<bool>(nullable: false, defaultValue: false),
                    ProcessedAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    ProcessedPoint = table.Column<int>(nullable: false, defaultValue: 0),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
