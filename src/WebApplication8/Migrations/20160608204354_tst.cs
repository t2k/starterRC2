using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication8.Migrations
{
    public partial class tst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskItem_RiskReport_RiskReportId",
                table: "RiskItem");

            migrationBuilder.DropIndex(
                name: "IX_RiskItem_RiskReportId",
                table: "RiskItem");

            migrationBuilder.DropColumn(
                name: "RiskReportId",
                table: "RiskItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RiskReportId",
                table: "RiskItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskItem_RiskReportId",
                table: "RiskItem",
                column: "RiskReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskItem_RiskReport_RiskReportId",
                table: "RiskItem",
                column: "RiskReportId",
                principalTable: "RiskReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
