using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AddAchievementTableAndLevelColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AchievementId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Users",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2(7)", nullable: false, defaultValueSql: "getdate()"),
                    DateDeleted = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    PointsToReach = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AchievementId",
                table: "Users",
                column: "AchievementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Achievements_AchievementId",
                table: "Users",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Achievements_AchievementId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Users_AchievementId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AchievementId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Users");
        }
    }
}
