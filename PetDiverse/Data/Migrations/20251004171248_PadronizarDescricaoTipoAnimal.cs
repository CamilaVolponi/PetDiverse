using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class PadronizarDescricaoTipoAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeTipoAnimal",
                table: "TipoAnimal",
                newName: "DescricaoTipoAnimal");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Animal",
                newName: "DescricaoAnimal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescricaoTipoAnimal",
                table: "TipoAnimal",
                newName: "NomeTipoAnimal");

            migrationBuilder.RenameColumn(
                name: "DescricaoAnimal",
                table: "Animal",
                newName: "Descricao");
        }
    }
}
