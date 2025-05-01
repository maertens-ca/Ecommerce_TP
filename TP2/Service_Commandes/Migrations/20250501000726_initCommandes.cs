using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service_Commandes.Migrations
{
    /// <inheritdoc />
    public partial class initCommandes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Panier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCommande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    produitId = table.Column<int>(type: "int", nullable: false),
                    quantité = table.Column<int>(type: "int", nullable: false),
                    commandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCommande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCommande_Panier_commandeId",
                        column: x => x.commandeId,
                        principalTable: "Panier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCommande_commandeId",
                table: "ItemCommande",
                column: "commandeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCommande");

            migrationBuilder.DropTable(
                name: "Panier");
        }
    }
}
