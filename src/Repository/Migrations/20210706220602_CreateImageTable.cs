using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class CreateImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_id_categoria",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ativo",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "funcao",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Customer",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id_imagem",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "id_imagem",
                table: "Produtos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Opened",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Imagens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conteudo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_id_imagem",
                table: "Usuarios",
                column: "id_imagem");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_id_imagem",
                table: "Produtos",
                column: "id_imagem");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_id_categoria",
                table: "Produtos",
                column: "id_categoria",
                principalTable: "Categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Imagens_id_imagem",
                table: "Produtos",
                column: "id_imagem",
                principalTable: "Imagens",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Imagens_id_imagem",
                table: "Usuarios",
                column: "id_imagem",
                principalTable: "Imagens",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_id_categoria",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Imagens_id_imagem",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Imagens_id_imagem",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Imagens");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_id_imagem",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_id_imagem",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "id_imagem",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "id_imagem",
                table: "Produtos");

            migrationBuilder.AlterColumn<string>(
                name: "funcao",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Customer");

            migrationBuilder.AddColumn<bool>(
                name: "ativo",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Opened");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_id_categoria",
                table: "Produtos",
                column: "id_categoria",
                principalTable: "Categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
