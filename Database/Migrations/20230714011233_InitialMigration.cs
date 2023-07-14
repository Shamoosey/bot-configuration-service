using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseMigrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultChannel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableKickCache = table.Column<bool>(type: "bit", nullable: false),
                    KickCacheDays = table.Column<int>(type: "int", nullable: false),
                    KickCacheHours = table.Column<int>(type: "int", nullable: false),
                    KickCacheServerMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KickCacheUserMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Triggers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageDelete = table.Column<bool>(type: "bit", nullable: false),
                    SendRandomResponse = table.Column<bool>(type: "bit", nullable: false),
                    IgnoreCooldown = table.Column<bool>(type: "bit", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triggers_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSecert = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscordUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReactEmote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriggerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactEmote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReactEmote_Triggers_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "Triggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TriggerResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriggerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggerResponses_Triggers_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "Triggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TriggerWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriggerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggerWords_Triggers_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "Triggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReactEmote_TriggerId",
                table: "ReactEmote",
                column: "TriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerResponses_TriggerId",
                table: "TriggerResponses",
                column: "TriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_ConfigurationId",
                table: "Triggers",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerWords_TriggerId",
                table: "TriggerWords",
                column: "TriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ConfigurationId",
                table: "Users",
                column: "ConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReactEmote");

            migrationBuilder.DropTable(
                name: "StatusMessages");

            migrationBuilder.DropTable(
                name: "TriggerResponses");

            migrationBuilder.DropTable(
                name: "TriggerWords");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Triggers");

            migrationBuilder.DropTable(
                name: "Configurations");
        }
    }
}
