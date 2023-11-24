using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatternConstructor.Migrations
{
    /// <inheritdoc />
    public partial class addingCreatedFilesandDescriptionUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreatedFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PatternLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescribtionLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreatedFiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DescriptionUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionUnits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreatedFiles_UserId",
                table: "CreatedFiles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreatedFiles");

            migrationBuilder.DropTable(
                name: "DescriptionUnits");
        }
    }
}
