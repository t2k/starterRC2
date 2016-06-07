using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication8.Migrations
{
    public partial class riskModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true),
                    Ordinal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Classification = table.Column<string>(nullable: true),
                    Ordinal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    RiskCategoryId = table.Column<int>(nullable: false),
                    RiskClassId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskItem_RiskCategory_RiskCategoryId",
                        column: x => x.RiskCategoryId,
                        principalTable: "RiskCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskItem_RiskClass_RiskClassId",
                        column: x => x.RiskClassId,
                        principalTable: "RiskClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskItem_RiskCategoryId",
                table: "RiskItem",
                column: "RiskCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskItem_RiskClassId",
                table: "RiskItem",
                column: "RiskClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskItem");

            migrationBuilder.DropTable(
                name: "RiskCategory");

            migrationBuilder.DropTable(
                name: "RiskClass");
        }
    }
}
