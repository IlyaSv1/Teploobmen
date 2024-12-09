using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTeploobmen.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    H0 = table.Column<int>(type: "INTEGER", nullable: false),
                    Tmal = table.Column<int>(type: "INTEGER", nullable: false),
                    Tbol = table.Column<int>(type: "INTEGER", nullable: false),
                    Wg = table.Column<double>(type: "REAL", nullable: false),
                    Cg = table.Column<double>(type: "REAL", nullable: false),
                    Cm = table.Column<double>(type: "REAL", nullable: false),
                    Gm = table.Column<double>(type: "REAL", nullable: false),
                    Av = table.Column<double>(type: "REAL", nullable: false),
                    Da = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variants");
        }
    }
}
