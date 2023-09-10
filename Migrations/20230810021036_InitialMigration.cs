using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    MarkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject1Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject2Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject3Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject4Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject5Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject6Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.MarkId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    subjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject6 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.subjectId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identities");

            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
