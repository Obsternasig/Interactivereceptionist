using Microsoft.EntityFrameworkCore.Migrations;

namespace NOVIRP.Migrations
{
    public partial class Findvej : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Findvej",
                table: "NOVI",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Findvej",
                table: "NOVI");
        }
    }
}
