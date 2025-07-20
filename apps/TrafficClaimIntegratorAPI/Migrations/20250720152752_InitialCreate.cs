using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafficClaimIntegratorAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsuranceClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IntersectionId = table.Column<string>(type: "text", nullable: false),
                    IncidentTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DriverName = table.Column<string>(type: "text", nullable: false),
                    ClaimDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrafficEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IntersectionId = table.Column<string>(type: "text", nullable: false),
                    EventTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    RawData = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimTrafficCorrelations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrafficEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchScore = table.Column<float>(type: "real", nullable: false),
                    CorrelationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimTrafficCorrelations", x => x.id);
                    table.ForeignKey(
                        name: "FK_ClaimTrafficCorrelations_InsuranceClaims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "InsuranceClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimTrafficCorrelations_TrafficEvents_TrafficEventId",
                        column: x => x.TrafficEventId,
                        principalTable: "TrafficEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimTrafficCorrelations_ClaimId",
                table: "ClaimTrafficCorrelations",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimTrafficCorrelations_TrafficEventId",
                table: "ClaimTrafficCorrelations",
                column: "TrafficEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimTrafficCorrelations");

            migrationBuilder.DropTable(
                name: "InsuranceClaims");

            migrationBuilder.DropTable(
                name: "TrafficEvents");
        }
    }
}
