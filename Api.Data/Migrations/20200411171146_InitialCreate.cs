using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MakeATrinkspruch.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TagName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Toasts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ToastText = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToastTag",
                columns: table => new
                {
                    ToastId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToastTag", x => new { x.ToastId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ToastTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToastTag_Toasts_ToastId",
                        column: x => x.ToastId,
                        principalTable: "Toasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagName",
                table: "Tags",
                column: "TagName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Toasts_ToastText",
                table: "Toasts",
                column: "ToastText",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToastTag_TagId",
                table: "ToastTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToastTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Toasts");
        }
    }
}