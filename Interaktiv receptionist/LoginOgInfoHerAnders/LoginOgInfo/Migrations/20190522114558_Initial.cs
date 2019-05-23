using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginOgInfo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LokalerInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Lokalenummer = table.Column<string>(nullable: true),
                    Lejer = table.Column<string>(nullable: true),
                    Medarbejderantal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LokalerInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MedarbejderInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Navn = table.Column<string>(nullable: true),
                    Virksomhed = table.Column<string>(nullable: true),
                    Lokale = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Stilling = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedarbejderInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VirksomhederInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Navn = table.Column<string>(nullable: true),
                    CVR = table.Column<string>(nullable: true),
                    Medarbejdere = table.Column<string>(nullable: true),
                    Lokale = table.Column<string>(nullable: true),
                    Findvej = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirksomhederInfo", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LokalerInfo");

            migrationBuilder.DropTable(
                name: "MedarbejderInfo");

            migrationBuilder.DropTable(
                name: "VirksomhederInfo");
        }
    }
}
