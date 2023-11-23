using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Endereco_LojaId",
                table: "Endereco",
                column: "LojaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Loja_LojaId",
                table: "Endereco",
                column: "LojaId",
                principalTable: "Loja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Loja_LojaId",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_LojaId",
                table: "Endereco");
        }
    }
}
