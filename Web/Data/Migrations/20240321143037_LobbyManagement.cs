using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Data.Migrations;
/// <inheritdoc />
public partial class LobbyManagement : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "Lobby",
            columns: table => new {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Player1Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Player2Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_Lobby", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "Lobby");
    }
}
