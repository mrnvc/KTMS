using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeContestantsCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contestants_Belts_BeltId",
                table: "Contestants");

            migrationBuilder.DropForeignKey(
                name: "FK_Contestants_Clubs_ClubId",
                table: "Contestants");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Contestants_ContestantId",
                table: "Enrollments");

            migrationBuilder.AddForeignKey(
                name: "FK_Contestants_Belts_BeltId",
                table: "Contestants",
                column: "BeltId",
                principalTable: "Belts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contestants_Clubs_ClubId",
                table: "Contestants",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Contestants_ContestantId",
                table: "Enrollments",
                column: "ContestantId",
                principalTable: "Contestants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contestants_Belts_BeltId",
                table: "Contestants");

            migrationBuilder.DropForeignKey(
                name: "FK_Contestants_Clubs_ClubId",
                table: "Contestants");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Contestants_ContestantId",
                table: "Enrollments");

            migrationBuilder.AddForeignKey(
                name: "FK_Contestants_Belts_BeltId",
                table: "Contestants",
                column: "BeltId",
                principalTable: "Belts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contestants_Clubs_ClubId",
                table: "Contestants",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Contestants_ContestantId",
                table: "Enrollments",
                column: "ContestantId",
                principalTable: "Contestants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
