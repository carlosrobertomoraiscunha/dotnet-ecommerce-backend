using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class DropCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_id_categoria",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_id_categoria",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "id_categoria",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Produtos");

            migrationBuilder.AddColumn<long>(
                name: "id_categoria",
                table: "Produtos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_id_categoria",
                table: "Produtos",
                column: "id_categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_id_categoria",
                table: "Produtos",
                column: "id_categoria",
                principalTable: "Categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
