using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatternConstructor.Migrations
{
    /// <inheritdoc />
    public partial class addingMeasures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_standartMeasures_Measure_MeasureId",
                table: "standartMeasures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measure",
                table: "Measure");

            migrationBuilder.RenameTable(
                name: "Measure",
                newName: "Measures");

            migrationBuilder.AlterColumn<int>(
                name: "MeasureId",
                table: "standartMeasures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measures",
                table: "Measures",
                column: "MeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_standartMeasures_Measures_MeasureId",
                table: "standartMeasures",
                column: "MeasureId",
                principalTable: "Measures",
                principalColumn: "MeasureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_standartMeasures_Measures_MeasureId",
                table: "standartMeasures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measures",
                table: "Measures");

            migrationBuilder.RenameTable(
                name: "Measures",
                newName: "Measure");

            migrationBuilder.AlterColumn<int>(
                name: "MeasureId",
                table: "standartMeasures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measure",
                table: "Measure",
                column: "MeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_standartMeasures_Measure_MeasureId",
                table: "standartMeasures",
                column: "MeasureId",
                principalTable: "Measure",
                principalColumn: "MeasureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
