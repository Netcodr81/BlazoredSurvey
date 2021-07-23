using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyAccessor.Migrations
{
    public partial class addfeaturesurveyandtotaltimestaken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FeaturedSurvey",
                table: "Surveys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TotalTimesTaken",
                table: "Surveys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedSurvey",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "TotalTimesTaken",
                table: "Surveys");
        }
    }
}
