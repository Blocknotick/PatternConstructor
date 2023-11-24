using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatternConstructor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measure",
                columns: table => new
                {
                    MeasureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BustCenter = table.Column<double>(type: "float", nullable: false),
                    LegLength = table.Column<double>(type: "float", nullable: false),
                    ShoulderToWrist = table.Column<double>(type: "float", nullable: false),
                    SeatHeight = table.Column<double>(type: "float", nullable: false),
                    HipsGirth = table.Column<double>(type: "float", nullable: false),
                    BustGirth = table.Column<double>(type: "float", nullable: false),
                    UpperArm = table.Column<double>(type: "float", nullable: false),
                    WaistGirth = table.Column<double>(type: "float", nullable: false),
                    WristGirth = table.Column<double>(type: "float", nullable: false),
                    HipHeight = table.Column<double>(type: "float", nullable: false),
                    BustHeight = table.Column<double>(type: "float", nullable: false),
                    ShoulderToNeck = table.Column<double>(type: "float", nullable: false),
                    ElbowLength = table.Column<double>(type: "float", nullable: false),
                    BackWaistLength = table.Column<double>(type: "float", nullable: false),
                    FrontWaistLength = table.Column<double>(type: "float", nullable: false),
                    BackArmholeDepth = table.Column<double>(type: "float", nullable: false),
                    NeckGirth = table.Column<double>(type: "float", nullable: false),
                    BustWidth = table.Column<double>(type: "float", nullable: false),
                    BottomWidth = table.Column<double>(type: "float", nullable: false),
                    BustGirthUp = table.Column<double>(type: "float", nullable: false),
                    SleeveWidthBottom = table.Column<double>(type: "float", nullable: false),
                    BackWidth = table.Column<double>(type: "float", nullable: false),
                    WaistFloorSideLength = table.Column<double>(type: "float", nullable: false),
                    WaistFloorFrontLength = table.Column<double>(type: "float", nullable: false),
                    WaistFloorBackLength = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.MeasureId);
                });

            migrationBuilder.CreateTable(
                name: "standartMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    MeasureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_standartMeasures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_standartMeasures_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_standartMeasures_MeasureId",
                table: "standartMeasures",
                column: "MeasureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "standartMeasures");

            migrationBuilder.DropTable(
                name: "Measure");
        }
    }
}
