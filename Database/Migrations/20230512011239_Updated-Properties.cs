using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joebot_Backend.Database.Models
{
    /// <inheritdoc />
    public partial class UpdatedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KickedUserMessage",
                table: "Configurations",
                newName: "KickUserMessage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KickUserMessage",
                table: "Configurations",
                newName: "KickedUserMessage");
        }
    }
}
