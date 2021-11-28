using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class MigracaoCorrecao6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlterarStatus",
                table: "PessoaModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlterarStatus",
                table: "PessoaModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
