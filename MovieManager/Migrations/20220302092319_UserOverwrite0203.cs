using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManager.Migrations
{
    public partial class UserOverwrite0203 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_UserPlaylist_UserPlaylistUserId",
                table: "Playlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPlaylist",
                table: "UserPlaylist");

            migrationBuilder.RenameTable(
                name: "UserPlaylist",
                newName: "UserPlaylists");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Playlists",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "THISWILLSHOW",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPlaylists",
                table: "UserPlaylists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_AspNetUsers_UserId",
                table: "Playlists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_UserPlaylists_UserPlaylistUserId",
                table: "Playlists",
                column: "UserPlaylistUserId",
                principalTable: "UserPlaylists",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_AspNetUsers_UserId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_UserPlaylists_UserPlaylistUserId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPlaylists",
                table: "UserPlaylists");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "THISWILLSHOW",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserPlaylists",
                newName: "UserPlaylist");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPlaylist",
                table: "UserPlaylist",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_UserPlaylist_UserPlaylistUserId",
                table: "Playlists",
                column: "UserPlaylistUserId",
                principalTable: "UserPlaylist",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
