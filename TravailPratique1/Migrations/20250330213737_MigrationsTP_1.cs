using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravailPratique1.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsTP_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vendeurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Product_Users_vendeurId",
                        column: x => x.vendeurId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientProduits",
                columns: table => new
                {
                    clientProduitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantité = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProduits", x => x.clientProduitId);
                    table.ForeignKey(
                        name: "FK_ClientProduits_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientProduits_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    commandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    factureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.commandeId);
                    table.ForeignKey(
                        name: "FK_Commandes_Users_clientId",
                        column: x => x.clientId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    factureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    vendeurId = table.Column<int>(type: "int", nullable: false),
                    commandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.factureId);
                    table.ForeignKey(
                        name: "FK_Factures_Commandes_commandeId",
                        column: x => x.commandeId,
                        principalTable: "Commandes",
                        principalColumn: "commandeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Factures_Users_clientId",
                        column: x => x.clientId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Factures_Users_vendeurId",
                        column: x => x.vendeurId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProduitCommandes",
                columns: table => new
                {
                    produitCommandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantité = table.Column<int>(type: "int", nullable: false),
                    commandeId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitCommandes", x => x.produitCommandeId);
                    table.ForeignKey(
                        name: "FK_ProduitCommandes_Commandes_commandeId",
                        column: x => x.commandeId,
                        principalTable: "Commandes",
                        principalColumn: "commandeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProduitCommandes_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientProduits_productId",
                table: "ClientProduits",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProduits_userId",
                table: "ClientProduits",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_clientId",
                table: "Commandes",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_factureId",
                table: "Commandes",
                column: "factureId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_clientId",
                table: "Factures",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_commandeId",
                table: "Factures",
                column: "commandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_vendeurId",
                table: "Factures",
                column: "vendeurId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_vendeurId",
                table: "Product",
                column: "vendeurId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitCommandes_commandeId",
                table: "ProduitCommandes",
                column: "commandeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitCommandes_productId",
                table: "ProduitCommandes",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Factures_factureId",
                table: "Commandes",
                column: "factureId",
                principalTable: "Factures",
                principalColumn: "factureId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Users_clientId",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Users_clientId",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Users_vendeurId",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Factures_factureId",
                table: "Commandes");

            migrationBuilder.DropTable(
                name: "ClientProduits");

            migrationBuilder.DropTable(
                name: "ProduitCommandes");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Commandes");
        }
    }
}
