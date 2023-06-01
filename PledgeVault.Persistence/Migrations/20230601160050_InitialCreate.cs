using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PledgeVault.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateEstablished = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GovernmentType = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityActive = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateEstablished = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SiteUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parties_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Politicians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SexType = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CountryOfBirth = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    IsPartyLeader = table.Column<bool>(type: "bit", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Politicians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Politicians_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Politicians_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pledges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityActive = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DatePledged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFulfilled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PledgeCategoryType = table.Column<int>(type: "int", nullable: false),
                    PledgeStatusType = table.Column<int>(type: "int", nullable: false),
                    PoliticianId = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    FulfilledSummary = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pledges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pledges_Politicians_PoliticianId",
                        column: x => x.PoliticianId,
                        principalTable: "Politicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityActive = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SiteUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ResourceType = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    PledgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Pledges_PledgeId",
                        column: x => x.PledgeId,
                        principalTable: "Pledges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_CountryId",
                table: "Parties",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_Name_CountryId",
                table: "Parties",
                columns: new[] { "Name", "CountryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pledges_PoliticianId",
                table: "Pledges",
                column: "PoliticianId");

            migrationBuilder.CreateIndex(
                name: "IX_Pledges_Title_DatePledged_PoliticianId",
                table: "Pledges",
                columns: new[] { "Title", "DatePledged", "PoliticianId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Politicians_Name",
                table: "Politicians",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Politicians_Name_DateOfBirth_PartyId",
                table: "Politicians",
                columns: new[] { "Name", "DateOfBirth", "PartyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Politicians_PartyId",
                table: "Politicians",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Politicians_PositionId",
                table: "Politicians",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Title",
                table: "Positions",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_PledgeId",
                table: "Resources",
                column: "PledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_SiteUrl_PledgeId",
                table: "Resources",
                columns: new[] { "SiteUrl", "PledgeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Pledges");

            migrationBuilder.DropTable(
                name: "Politicians");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
