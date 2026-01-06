using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOMSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_LocationCode",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "Stationaries",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Stationaries_BranchCode",
                table: "Stationaries",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LocationCode",
                table: "Products",
                column: "LocationCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stationaries_Branches_BranchCode",
                table: "Stationaries",
                column: "BranchCode",
                principalTable: "Branches",
                principalColumn: "BranchCode",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stationaries_Branches_BranchCode",
                table: "Stationaries");

            migrationBuilder.DropIndex(
                name: "IX_Stationaries_BranchCode",
                table: "Stationaries");

            migrationBuilder.DropIndex(
                name: "IX_Products_LocationCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "Stationaries");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LocationCode",
                table: "Products",
                column: "LocationCode");
        }
    }
}
