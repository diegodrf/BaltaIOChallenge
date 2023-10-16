using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaltaIOChallenge.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IbgeCode",
                table: "Cities",
                column: "IbgeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_State_IbgeCode",
                table: "Cities",
                columns: new[] { "Name", "State", "IbgeCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_State",
                table: "Cities",
                column: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Cities_IbgeCode",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_State_IbgeCode",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_State",
                table: "Cities");
        }
    }
}
