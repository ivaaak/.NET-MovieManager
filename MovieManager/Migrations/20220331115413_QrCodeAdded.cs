using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManager.Migrations
{
    public partial class QrCodeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QrCode",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Actors");

            migrationBuilder.RenameColumn(
                name: "THISWILLSHOW",
                table: "AspNetUsers",
                newName: "Language");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Playlists",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrailerLink",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Actors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovieRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adult = table.Column<bool>(type: "bit", nullable: false),
                    Character = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieRole_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "ActorId");
                });

            migrationBuilder.CreateTable(
                name: "QRCodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QrCodeImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TextContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaylistId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QRCodes_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_UserId",
                table: "Actors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRole_ActorId",
                table: "MovieRole",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_QRCodes_PlaylistId",
                table: "QRCodes",
                column: "PlaylistId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_AspNetUsers_UserId",
                table: "Actors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_AspNetUsers_UserId",
                table: "Actors");

            migrationBuilder.DropTable(
                name: "MovieRole");

            migrationBuilder.DropTable(
                name: "QRCodes");

            migrationBuilder.DropIndex(
                name: "IX_Actors_UserId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TrailerLink",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Actors");

            migrationBuilder.RenameColumn(
                name: "Language",
                table: "AspNetUsers",
                newName: "THISWILLSHOW");

            migrationBuilder.AddColumn<string>(
                name: "QrCode",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Actors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
