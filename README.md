

# Getting Started

1.	Clone the repository:
2.	Using package manager run the following scripts
    * Update-Database -Project SurveyAccessor -Context SurveysDbContext
    * Update-Database -Project BlazorSurvey -Context ApplicationDbContext


# Notes
* Right now the database connection strings are pointed to your local db. If you want to change where the databases are created, update the connection strings in the appsettings.json
* The ApplicationDbContext is used for authorization/authentication. It's not being used right now so you don't necessarily have to run the update for the ApplicaitonDbContext in order to get the 
  application to run

