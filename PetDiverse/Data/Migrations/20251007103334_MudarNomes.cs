using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class MudarNomes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "PessoaFisica");

            migrationBuilder.RenameColumn(
                name: "DescricaoAnimal",
                table: "Animal",
                newName: "Descricao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Animal",
                newName: "DescricaoAnimal");

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "PessoaFisica",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
