using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace file_based_message_broker.data.migrations
{
    /// <inheritdoc />
    public partial class MemberEntityPhoneAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Members");
        }
    }
}
