using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDiverse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEnemContagemIdade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContagemIdade",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContagemIdade",
                table: "Animal");
        }
    }
}
