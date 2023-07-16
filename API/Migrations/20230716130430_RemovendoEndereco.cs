using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Loja_LojaId",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_LojaId",
                table: "Endereco");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
