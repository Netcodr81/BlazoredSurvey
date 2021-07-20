using BlazorSurvey.ViewModels;
using SurveyAccessor.Models;
using System.Linq;

namespace BlazorSurvey.Utils
{
    public static class Mapper
    {
        public static Survey EditSurveyToSurvey(EditSurveyViewModel survey)
        {
            Survey result = new Survey()
            {
                SurveyId = survey.SurveyId,
                SurveyName = survey.SurveyName,
                Description = survey.Description,
                SurveyQuestion = survey.SurveyQuestion,
                FeaturedSurvey = survey.FeaturedSurvey,
                SurveyOptions = survey.SurveyOptions

            };

            return result;
        }

        public static EditSurveyViewModel SurveyToEditSurveyModel(Survey survey)
        {
            EditSurveyViewModel results = new EditSurveyViewModel()
            {
                SurveyId = survey.SurveyId,
                SurveyName = survey.SurveyName,
                SurveyOptions = survey.SurveyOptions.ToList(),
                SurveyQuestion = survey.SurveyQuestion,
                FeaturedSurvey = survey.FeaturedSurvey,
                Description = survey.Description
            };

            return results;
        }

        public static SurveyViewModel SurveyToSurveyViewModel(Survey survey)
        {
            var results = new SurveyViewModel()
            {
                SurveyId = survey.SurveyId,
                SurveyName = survey.SurveyName,
                SurveyOptions = survey.SurveyOptions.ToList(),
                SurveyQuestion = survey.SurveyQuestion,
                CreatedOn = survey.CreatedOn,
                TotalTimesTaken = survey.TotalTimesTaken,
                TotalVotes = survey.TotalVotes,
                Description = survey.Description,
                FeaturedSurvey = survey.FeaturedSurvey

            };

            return results;
        }
    }
}


//public int SurveyId { get; set; }
//public string SurveyName { get; set; }
//public string Description { get; set; }

//public string SurveyQuestion { get; set; }

//public int TotalVotes { get; set; }

//public bool FeaturedSurvey { get; set; }

//public int TotalTimesTaken { get; set; }

//public DateTime CreatedOn { get; set; }

//public ICollection<SurveyOption> SurveyOptions { get; set; }