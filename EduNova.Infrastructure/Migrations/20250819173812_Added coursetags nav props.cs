using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduNova.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedcoursetagsnavprops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Courses_CourseId1",
                table: "CourseTags");

            migrationBuilder.DropIndex(
                name: "IX_CourseTags_CourseId1",
                table: "CourseTags");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "CourseTags");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId1",
                table: "CourseTags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CourseTags",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555551"),
                column: "CourseId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseTags",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555552"),
                column: "CourseId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseTags",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555553"),
                column: "CourseId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseTags",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555561"),
                column: "CourseId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseTags",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555562"),
                column: "CourseId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseTags",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555563"),
                column: "CourseId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTags_CourseId1",
                table: "CourseTags",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Courses_CourseId1",
                table: "CourseTags",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
