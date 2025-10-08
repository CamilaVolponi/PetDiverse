using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditarNomes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeBairro",
                table: "Bairro",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "NomeCidade",
                table: "Cidade",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "NomeEstado",
                table: "Estado",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "DescricaoRaca",
                table: "Raca",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "DataCirurgia",
                table: "RegistroCirurgia",
                newName: "DataRegistro");

            migrationBuilder.RenameColumn(
                name: "DataVacina",
                table: "RegistroVacina",
                newName: "DataRegistro");

            migrationBuilder.RenameColumn(
                name: "DescricaoTipoAnimal",
                table: "TipoAnimal",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "DescricaoCirurgia",
                table: "TipoCirurgia",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
               name: "DescricaoVacina",
               table: "TipoVacina",
               newName: "Descricao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome", // <--- Invertido
                table: "Bairro",
                newName: "NomeBairro"); // <--- Invertido

            migrationBuilder.RenameColumn(
                name: "Nome", // <--- Invertido
                table: "Cidade",
                newName: "NomeCidade"); // <--- Invertido

            migrationBuilder.RenameColumn(
                name: "Nome", // <--- Invertido
                table: "Estado",
                newName: "NomeEstado"); // <--- Invertido

            migrationBuilder.RenameColumn(
                name: "Descricao", // <--- Invertido
                table: "Raca",
                newName: "DescricaoRaca"); // <--- Invertido

            migrationBuilder.RenameColumn(
                name: "DataRegistro", // <--- Invertido
                table: "RegistroCirurgia",
                newName: "DataCirurgia"); // <--- Invertido

            migrationBuilder.RenameColumn(
                name: "DataRegistro", // <--- Invertido
                table: "RegistroVacina",
                newName: "DataVacina"); // <--- Invertido

            migrationBuilder.RenameColumn(
                name: "Descricao", // <--- Invertido
                table: "TipoAnimal",
                newName: "DescricaoTipoAnimal"); // <--- Invertido

            migrationBuilder.RenameColumn(
                name: "Descricao", // <--- Invertido
                table: "TipoCirurgia",
                newName: "DescricaoCirurgia"); // <--- Invertido

            migrationBuilder.RenameColumn(
               name: "Descricao", // <--- Invertido
               table: "TipoVacina",
               newName: "DescricaoVacina"); // <--- Invertido
        }
    }
}
