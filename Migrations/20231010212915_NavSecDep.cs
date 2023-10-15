using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADO_EF_P12.Migrations
{
    /// <inheritdoc />
    public partial class NavSecDep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Departments_SecDepId",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "SecDepId",
                table: "Managers",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_SecDepId",
                table: "Managers",
                newName: "IX_Managers_DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdSecDep",
                table: "Managers",
                column: "IdSecDep");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_DepartmentId",
                table: "Managers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_IdSecDep",
                table: "Managers",
                column: "IdSecDep",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Departments_DepartmentId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Departments_IdSecDep",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_IdSecDep",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Managers",
                newName: "SecDepId");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_DepartmentId",
                table: "Managers",
                newName: "IX_Managers_SecDepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_SecDepId",
                table: "Managers",
                column: "SecDepId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
