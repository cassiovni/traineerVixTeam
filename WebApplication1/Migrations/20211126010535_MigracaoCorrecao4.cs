using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class MigracaoCorrecao4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Funcionalidade",
                table: "PessoaModel",
                newName: "AlterarStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlterarStatus",
                table: "PessoaModel",
                newName: "Funcionalidade");
        }
    }
}
