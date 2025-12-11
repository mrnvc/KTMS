using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeTablesCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentJudges_Judges_JudgeId",
                table: "TournamentJudges");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentJudges_Tournaments_TournamentId",
                table: "TournamentJudges");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentJudges_Judges_JudgeId",
                table: "TournamentJudges",
                column: "JudgeId",
                principalTable: "Judges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentJudges_Tournaments_TournamentId",
                table: "TournamentJudges",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentJudges_Judges_JudgeId",
                table: "TournamentJudges");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentJudges_Tournaments_TournamentId",
                table: "TournamentJudges");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentJudges_Judges_JudgeId",
                table: "TournamentJudges",
                column: "JudgeId",
                principalTable: "Judges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentJudges_Tournaments_TournamentId",
                table: "TournamentJudges",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
