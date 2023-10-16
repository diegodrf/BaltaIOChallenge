using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaltaIOChallenge.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWrongIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_State_Code",
                table: "Cities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_State_Code",
                table: "Cities",
                columns: new[] { "Name", "State", "Code" });
        }
    }
}
