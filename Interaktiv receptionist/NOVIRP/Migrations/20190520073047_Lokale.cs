using Microsoft.EntityFrameworkCore.Migrations;

namespace NOVIRP.Migrations
{
    public partial class Lokale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lokale",
                table: "NOVI",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lokale",
                table: "NOVI");
        }
    }
}
