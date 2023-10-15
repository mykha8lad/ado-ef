using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADO_EF_P12.Migrations
{
    /// <inheritdoc />
    public partial class NavMainDep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Departments_IdSecDep",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_IdSecDep",
                table: "Managers");

            migrationBuilder.AddColumn<Guid>(
                name: "SecDepId",
                table: "Managers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_SecDepId",
                table: "Managers",
                column: "SecDepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_SecDepId",
                table: "Managers",
                column: "SecDepId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Departments_SecDepId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_SecDepId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "SecDepId",
                table: "Managers");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdSecDep",
                table: "Managers",
                column: "IdSecDep");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_IdSecDep",
                table: "Managers",
                column: "IdSecDep",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
