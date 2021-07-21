using SurveyAccessor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveyAccessor.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorSurvey.ViewModels
{
    public class EditSurveyViewModel
    {
        public EditSurveyViewModel(SurveysDbContext context)
        {
            Context = context;
        }
        public int SurveyId { get; set; }

        [Required(ErrorMessage="Survey name is required")]
        public string SurveyName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Survey question is required")]
        [MaxLength(255, ErrorMessage = "Survey question is too long")]
        public string SurveyQuestion { get; set; }     

        public bool FeaturedSurvey { get; set; }

        public List<SelectListItem> SurveyOptions { get; set; } = new List<SelectListItem>();

        public bool ShowDeleteOption { get; set; } = false;
        public SurveysDbContext Context { get; }

        public void AddSurveyOption(SurveyOption option)
        {
            SelectListItem optionToAdd = new SelectListItem {Selected = false, Text = option.Description, Value = option.SurveyOptionId.ToString()};
            SurveyOptions.Add(optionToAdd);
            ShowDeleteOption = false;
        }

        public async Task RemoveSurveyOption(int optionId)
        {
            var survey = await Context.Surveys.Where(x => x.SurveyId == SurveyId).Include(x => x.SurveyOptions).FirstOrDefaultAsync();
            var optionToRemove = survey.SurveyOptions.FirstOrDefault(x => x.SurveyOptionId == optionId);

            survey.TotalVotes -= optionToRemove.TotalVotes;
            
            survey.SurveyOptions.Remove(optionToRemove);

            Context.Attach(survey);
            Context.Entry(survey).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                var ex = e;
            }

            SurveyOptions.Remove(SurveyOptions.SingleOrDefault(x => x.Value == optionId.ToString()));

            if (SurveyOptions.Count == 1)
            {
                SurveyOptions.First().Text = "Please add an option...";              

            }

            ShowDeleteOption = false;
        }
    }
}
