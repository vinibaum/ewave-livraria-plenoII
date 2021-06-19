using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Livraria.Infra.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstituicaodeEnsino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituicaodeEnsino", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdInstituicaoDeEnsino = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_InstituicaodeEnsino_IdInstituicaoDeEnsino",
                        column: x => x.IdInstituicaoDeEnsino,
                        principalTable: "InstituicaodeEnsino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Genero = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Publicacao = table.Column<DateTime>(type: "date", nullable: false),
                    Paginas = table.Column<int>(type: "int", nullable: false),
                    Autor = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Editora = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Emprestado = table.Column<bool>(type: "bit", nullable: false),
                    Reservado = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuarioReserva = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livro_Usuario_IdUsuarioReserva",
                        column: x => x.IdUsuarioReserva,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLivroEmprestado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdLivro = table.Column<int>(type: "int", nullable: false),
                    DataEmprestimo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDevolucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDevolvido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLivroEmprestado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioLivroEmprestado_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_IdUsuarioReserva",
                table: "Livro",
                column: "IdUsuarioReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdInstituicaoDeEnsino",
                table: "Usuario",
                column: "IdInstituicaoDeEnsino");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLivroEmprestado_IdUsuario",
                table: "UsuarioLivroEmprestado",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "UsuarioLivroEmprestado");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "InstituicaodeEnsino");
        }
    }
}
