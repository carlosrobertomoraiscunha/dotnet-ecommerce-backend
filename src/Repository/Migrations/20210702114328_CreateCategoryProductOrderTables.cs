using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class CreateCategoryProductOrderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Enderecos");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuarios",
                newName: "senha");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Usuarios",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Usuarios",
                newName: "ativo");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "Usuarios",
                newName: "IX_Usuarios_email");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "Enderecos",
                newName: "cep");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Enderecos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Enderecos",
                newName: "id_usuario");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Enderecos",
                newName: "rua");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Enderecos",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Enderecos",
                newName: "numero");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Enderecos",
                newName: "bairro");

            migrationBuilder.RenameColumn(
                name: "Complement",
                table: "Enderecos",
                newName: "complemento");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Enderecos",
                newName: "cidade");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_UserId",
                table: "Enderecos",
                newName: "IX_Enderecos_id_usuario");

            migrationBuilder.AddColumn<string>(
                name: "funcao",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "numero",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos",
                column: "id");

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

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    total = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_usuario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    preco = table.Column<double>(type: "float", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    id_categoria = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos_Produtos",
                columns: table => new
                {
                    id_pedido = table.Column<long>(type: "bigint", nullable: false),
                    id_produto = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos_Produtos", x => new { x.id_pedido, x.id_produto });
                    table.ForeignKey(
                        name: "FK_Pedidos_Produtos_Pedidos_id_pedido",
                        column: x => x.id_pedido,
                        principalTable: "Pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Produtos_Produtos_id_produto",
                        column: x => x.id_produto,
                        principalTable: "Produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_id_usuario",
                table: "Pedidos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Produtos_id_produto",
                table: "Pedidos_Produtos",
                column: "id_produto");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_id_categoria",
                table: "Produtos",
                column: "id_categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_id_usuario",
                table: "Enderecos",
                column: "id_usuario",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_id_usuario",
                table: "Enderecos");

            migrationBuilder.DropTable(
                name: "Pedidos_Produtos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "funcao",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                newName: "Addresses");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "senha",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameColumn(
                name: "cep",
                table: "Addresses",
                newName: "Cep");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Addresses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "rua",
                table: "Addresses",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "numero",
                table: "Addresses",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "Addresses",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Addresses",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "complemento",
                table: "Addresses",
                newName: "Complement");

            migrationBuilder.RenameColumn(
                name: "cidade",
                table: "Addresses",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "bairro",
                table: "Addresses",
                newName: "District");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_id_usuario",
                table: "Addresses",
                newName: "IX_Addresses_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Addresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
