using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Data.Migrations {
    /// <inheritdoc />
    public partial class lobbiesaddition : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Lobby",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player1Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Player2Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Lobby", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lobby_AspNetUsers_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lobby_AspNetUsers_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lobby_Player1Id",
                table: "Lobby",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lobby_Player2Id",
                table: "Lobby",
                column: "Player2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Lobby");
        }
    }
}
