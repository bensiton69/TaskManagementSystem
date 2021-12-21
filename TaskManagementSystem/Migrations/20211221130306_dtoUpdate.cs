using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Migrations
{
    public partial class dtoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemTasks_Users_AppUserId",
                table: "SystemTasks");

            migrationBuilder.DropIndex(
                name: "IX_SystemTasks_AppUserId",
                table: "SystemTasks");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "SystemTasks");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "SystemTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SystemTasks_OwnerId",
                table: "SystemTasks",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemTasks_Users_OwnerId",
                table: "SystemTasks",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemTasks_Users_OwnerId",
                table: "SystemTasks");

            migrationBuilder.DropIndex(
                name: "IX_SystemTasks_OwnerId",
                table: "SystemTasks");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "SystemTasks");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "SystemTasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemTasks_AppUserId",
                table: "SystemTasks",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemTasks_Users_AppUserId",
                table: "SystemTasks",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
