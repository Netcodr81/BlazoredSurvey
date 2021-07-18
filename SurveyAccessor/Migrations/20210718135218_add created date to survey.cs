using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyAccessor.Migrations
{
    public partial class addcreateddatetosurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Surveys");
        }
    }
}
