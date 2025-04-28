using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service_Produits.Migrations
{
    /// <inheritdoc />
    public partial class initProduit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Produit",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Produit",
                newName: "id");
        }
    }
}
