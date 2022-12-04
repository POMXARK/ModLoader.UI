using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModLoader.Migrations
{
    /// <inheritdoc />
    public partial class WebResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebResources",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: true),
                    SourseDownload = table.Column<string>(type: "TEXT", nullable: true),
                    LinkDownload = table.Column<string>(type: "TEXT", nullable: true),
                    AboutMod = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModWebResource",
                columns: table => new
                {
                    ModsId = table.Column<uint>(type: "INTEGER", nullable: false),
                    WebResourcesId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModWebResource", x => new { x.ModsId, x.WebResourcesId });
                    table.ForeignKey(
                        name: "FK_ModWebResource_Mods_ModsId",
                        column: x => x.ModsId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModWebResource_WebResources_WebResourcesId",
                        column: x => x.WebResourcesId,
                        principalTable: "WebResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModWebResource_WebResourcesId",
                table: "ModWebResource",
                column: "WebResourcesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModWebResource");

            migrationBuilder.DropTable(
                name: "WebResources");
        }
    }
}
