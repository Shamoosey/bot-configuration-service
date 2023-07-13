using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joebot_Backend.Database
{
    /// <inheritdoc />
    public partial class RemovedExtraColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KickServerMessage",
                table: "Configurations");

            migrationBuilder.RenameColumn(
                name: "KickUserMessage",
                table: "Configurations",
                newName: "KickCacheUserMessage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KickCacheUserMessage",
                table: "Configurations",
                newName: "KickUserMessage");

            migrationBuilder.AddColumn<string>(
                name: "KickServerMessage",
                table: "Configurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
