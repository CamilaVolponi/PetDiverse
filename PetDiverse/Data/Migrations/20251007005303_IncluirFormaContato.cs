using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncluirFormaContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormaContato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoFormaContato = table.Column<int>(type: "int", nullable: false),
                    IdPesssoaDoadora = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaContato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormaContato_PessoaDoadora_IdPesssoaDoadora",
                        column: x => x.IdPesssoaDoadora,
                        principalTable: "PessoaDoadora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormaContato_IdPesssoaDoadora",
                table: "FormaContato",
                column: "IdPesssoaDoadora");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormaContato");
        }
    }
}
