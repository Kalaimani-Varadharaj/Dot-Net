using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraudDetectionRepositoryPatternProject.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampsToFraudulentIncidentDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_History",
                columns: table => new
                {
                    Transaction_Reference_Number = table.Column<int>(type: "int", nullable: false),
                    Transaction_DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sender_Account_Number = table.Column<long>(type: "bigint", nullable: true),
                    Beneficiary_Account_Number = table.Column<long>(type: "bigint", nullable: true),
                    Sender_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Beneficiary_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Payment_Method = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime", nullable: true),
                    Modified_At = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__E92BA0E4E089E93E", x => x.Transaction_Reference_Number);
                });

            migrationBuilder.CreateTable(
                name: "Fraudulent_Incident_Details",
                columns: table => new
                {
                    Incident_Number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Transaction_Reference_Number = table.Column<int>(type: "int", nullable: true),
                    Incident_Status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Fraudulent_Type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Fraudule__DBAD0997440DBEAA", x => x.Incident_Number);
                    table.ForeignKey(
                        name: "FK__Fraudulen__Trans__778AC167",
                        column: x => x.Transaction_Reference_Number,
                        principalTable: "Transaction_History",
                        principalColumn: "Transaction_Reference_Number");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fraudulent_Incident_Details_Transaction_Reference_Number",
                table: "Fraudulent_Incident_Details",
                column: "Transaction_Reference_Number");

            migrationBuilder.CreateIndex(
                name: "UQ__Transact__AAAD967C9E7E4890",
                table: "Transaction_History",
                column: "Sender_Account_Number",
                unique: true,
                filter: "[Sender_Account_Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Transact__CC76BB83FD038907",
                table: "Transaction_History",
                column: "Beneficiary_Account_Number",
                unique: true,
                filter: "[Beneficiary_Account_Number] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fraudulent_Incident_Details");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Transaction_History");
        }
    }
}
