using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InterOn.Repo.Migrations
{
    public partial class AddGroupPhotoCdn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "GroupRef",
                table: "GroupPhotos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupPhotos_GroupRef",
                table: "GroupPhotos",
                column: "GroupRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPhotos_Groups_GroupRef",
                table: "GroupPhotos",
                column: "GroupRef",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPhotos_Groups_GroupRef",
                table: "GroupPhotos");

            migrationBuilder.DropIndex(
                name: "IX_GroupPhotos_GroupRef",
                table: "GroupPhotos");

            migrationBuilder.DropColumn(
                name: "GroupRef",
                table: "GroupPhotos");

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Groups",
                nullable: true);
        }
    }
}
