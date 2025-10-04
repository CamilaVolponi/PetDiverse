using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoRaca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRaca",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Raca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoRaca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoAnimal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raca_TipoAnimal_IdTipoAnimal",
                        column: x => x.IdTipoAnimal,
                        principalTable: "TipoAnimal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_IdRaca",
                table: "Animal",
                column: "IdRaca");

            migrationBuilder.CreateIndex(
                name: "IX_Raca_IdTipoAnimal",
                table: "Raca",
                column: "IdTipoAnimal");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Raca_IdRaca",
                table: "Animal",
                column: "IdRaca",
                principalTable: "Raca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Raca_IdRaca",
                table: "Animal");

            migrationBuilder.DropTable(
                name: "Raca");

            migrationBuilder.DropIndex(
                name: "IX_Animal_IdRaca",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "IdRaca",
                table: "Animal");
        }
    }
}
