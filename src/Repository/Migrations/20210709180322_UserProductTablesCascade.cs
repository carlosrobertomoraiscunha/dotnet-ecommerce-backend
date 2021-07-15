using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class UserProductTablesCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Imagens_id_imagem",
                table: "Produtos");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Imagens_id_imagem",
                table: "Produtos",
                column: "id_imagem",
                principalTable: "Imagens",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Imagens_id_imagem",
                table: "Produtos");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Imagens_id_imagem",
                table: "Produtos",
                column: "id_imagem",
                principalTable: "Imagens",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
