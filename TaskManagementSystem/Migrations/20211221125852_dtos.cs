using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Migrations
{
    public partial class dtos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
