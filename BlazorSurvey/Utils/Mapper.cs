using System.Collections.Generic;
using BlazorSurvey.ViewModels;
using SurveyAccessor.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                FeaturedSurvey = survey.FeaturedSurvey
            };

            return result;
        }

        public static EditSurveyViewModel SurveyToEditSurveyModel(Survey survey)
        {
            EditSurveyViewModel results = new EditSurveyViewModel()
            {
                SurveyId = survey.SurveyId,
                SurveyName = survey.SurveyName,
                SurveyOptions = GenerateEditViewModelOptionSelectList(survey.SurveyOptions.ToList()),
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

        private static List<SelectListItem> GenerateEditViewModelOptionSelectList(List<SurveyOption> options)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            if (options.Count == 0)
            {
                selectList.Add(new SelectListItem { Text = "Please add an option...", Value = "", Selected = true });
            }
            else
            {
                selectList.Add(new SelectListItem { Text = "Select an option...", Value = "", Selected = true });
            }
           

            foreach (var surveyOption in options)
            {
                selectList.Add(new SelectListItem {Text = surveyOption.Description, Selected = false, Value = surveyOption.SurveyOptionId.ToString()});

            }

            return selectList;
        }
    }
}

