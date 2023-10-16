using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaltaIOChallenge.Core.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IbgeCode",
                table: "Cities",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_Name_State_IbgeCode",
                table: "Cities",
                newName: "IX_Cities_Name_State_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_IbgeCode",
                table: "Cities",
                newName: "IX_Cities_Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Cities",
                newName: "IbgeCode");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_Name_State_Code",
                table: "Cities",
                newName: "IX_Cities_Name_State_IbgeCode");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_Code",
                table: "Cities",
                newName: "IX_Cities_IbgeCode");
        }
    }
}
