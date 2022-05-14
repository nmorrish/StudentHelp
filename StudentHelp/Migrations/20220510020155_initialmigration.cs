using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentHelp.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Course_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Course_Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Student_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Student_Id);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Assignment_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Course_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Assignment_Id);
                    table.ForeignKey(
                        name: "FK_Assignment_Course",
                        column: x => x.Course_Id,
                        principalTable: "Course",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student_Course",
                columns: table => new
                {
                    Student_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Course_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrolledDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => new { x.Student_Id, x.Course_Id });
                    table.ForeignKey(
                        name: "FK_StudentCourse_Course",
                        column: x => x.Course_Id,
                        principalTable: "Course",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Student",
                        column: x => x.Student_Id,
                        principalTable: "Student",
                        principalColumn: "Student_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student_Assignment",
                columns: table => new
                {
                    Student_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Assignment_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssignment", x => new { x.Student_Id, x.Assignment_Id });
                    table.ForeignKey(
                        name: "FK_StudentAssignment_Assignment",
                        column: x => x.Assignment_Id,
                        principalTable: "Assignment",
                        principalColumn: "Assignment_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAssignment_Student",
                        column: x => x.Student_Id,
                        principalTable: "Student",
                        principalColumn: "Student_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Course_Id",
                table: "Assignment",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "UC_CourseName",
                table: "Course",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_Assignment_Assignment_Id",
                table: "Student_Assignment",
                column: "Assignment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Course_Course_Id",
                table: "Student_Course",
                column: "Course_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student_Assignment");

            migrationBuilder.DropTable(
                name: "Student_Course");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
