using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace file_based_message_broker.data.migrations
{
    /// <inheritdoc />
    public partial class EntityRefactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Members_MemberID",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_MemberID",
                table: "Workers");

            migrationBuilder.AddColumn<int>(
                name: "BranchID",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerDepartmentID",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerMemberID",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_BranchID",
                table: "Workers",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_WorkerDepartmentID_WorkerMemberID",
                table: "Members",
                columns: new[] { "WorkerDepartmentID", "WorkerMemberID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Workers_WorkerDepartmentID_WorkerMemberID",
                table: "Members",
                columns: new[] { "WorkerDepartmentID", "WorkerMemberID" },
                principalTable: "Workers",
                principalColumns: new[] { "DepartmentID", "MemberID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Branches_BranchID",
                table: "Workers",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Workers_WorkerDepartmentID_WorkerMemberID",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Branches_BranchID",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_BranchID",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Members_WorkerDepartmentID_WorkerMemberID",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WorkerDepartmentID",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "WorkerMemberID",
                table: "Members");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_MemberID",
                table: "Workers",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Members_MemberID",
                table: "Workers",
                column: "MemberID",
                principalTable: "Members",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
