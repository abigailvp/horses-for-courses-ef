using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorsesForCourses.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class OptionalCOachId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Coaches_CoachId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "CoachId",
                table: "Courses",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Coaches_CoachId",
                table: "Courses",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "CoachId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Coaches_CoachId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "CoachId",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Coaches_CoachId",
                table: "Courses",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "CoachId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
