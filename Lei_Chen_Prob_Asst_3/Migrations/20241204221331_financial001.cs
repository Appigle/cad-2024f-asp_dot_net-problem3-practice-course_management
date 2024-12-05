using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lei_Chen_Prob_Asst_3.Migrations
{
    /// <inheritdoc />
    public partial class financial001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeePolicies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TuitionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegistrationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FacilitiesFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeePolicies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FinancialStatements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastChanged = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StudentID = table.Column<int>(type: "INTEGER", nullable: false),
                    FeePolicyID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FinancialStatements_FeePolicies_FeePolicyID",
                        column: x => x.FeePolicyID,
                        principalTable: "FeePolicies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialStatements_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatementEntries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinancialStatementID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementEntries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StatementEntries_FinancialStatements_FinancialStatementID",
                        column: x => x.FinancialStatementID,
                        principalTable: "FinancialStatements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatements_FeePolicyID",
                table: "FinancialStatements",
                column: "FeePolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatements_StudentID",
                table: "FinancialStatements",
                column: "StudentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatementEntries_FinancialStatementID",
                table: "StatementEntries",
                column: "FinancialStatementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatementEntries");

            migrationBuilder.DropTable(
                name: "FinancialStatements");

            migrationBuilder.DropTable(
                name: "FeePolicies");
        }
    }
}
