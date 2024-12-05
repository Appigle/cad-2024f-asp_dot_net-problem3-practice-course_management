using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lei_Chen_Prob_Asst_3.Migrations
{
    /// <inheritdoc />
    public partial class student001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgramID = table.Column<int>(type: "INTEGER", nullable: false),
                    CityID = table.Column<int>(type: "INTEGER", nullable: false),
                    Province = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_UGPrograms_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "UGPrograms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    CoursesID = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => new { x.CoursesID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CoursesID",
                        column: x => x.CoursesID,
                        principalTable: "Courses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentID",
                table: "StudentCourses",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CityID",
                table: "Students",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramID",
                table: "Students",
                column: "ProgramID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
