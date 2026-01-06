using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOMSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBranchPKToCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Branches");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "BranchCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "BranchId");
        }
    }
}
