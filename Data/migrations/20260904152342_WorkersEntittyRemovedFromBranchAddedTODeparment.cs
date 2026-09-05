using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace file_based_message_broker.data.migrations
{
    /// <inheritdoc />
    public partial class WorkersEntittyRemovedFromBranchAddedTODeparment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Branches_BranchID",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_BranchID",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "Workers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchID",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_BranchID",
                table: "Workers",
                column: "BranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Branches_BranchID",
                table: "Workers",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "ID");
        }
    }
}
