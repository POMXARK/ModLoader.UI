using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModLoader.Migrations
{
    /// <inheritdoc />
    public partial class AddPacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "WebResources");

            migrationBuilder.CreateTable(
                name: "Packs",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModPack",
                columns: table => new
                {
                    ModsId = table.Column<uint>(type: "INTEGER", nullable: false),
                    PacksId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModPack", x => new { x.ModsId, x.PacksId });
                    table.ForeignKey(
                        name: "FK_ModPack_Mods_ModsId",
                        column: x => x.ModsId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModPack_Packs_PacksId",
                        column: x => x.PacksId,
                        principalTable: "Packs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModPack_PacksId",
                table: "ModPack",
                column: "PacksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModPack");

            migrationBuilder.DropTable(
                name: "Packs");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "WebResources",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
