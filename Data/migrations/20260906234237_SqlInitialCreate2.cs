using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace htmos.data.migrations
{
    /// <inheritdoc />
    public partial class SqlInitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Appointee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appointments_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BranchAdmins",
                columns: new[] { "BranchID", "UserID", "BranchID1", "ID" },
                values: new object[] { 1, 1, null, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_BranchID",
                table: "Appointments",
                column: "BranchID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DeleteData(
                table: "BranchAdmins",
                keyColumns: new[] { "BranchID", "UserID" },
                keyValues: new object[] { 1, 1 });
        }
    }
}
