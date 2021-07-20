using SurveyAccessor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlazorSurvey.ViewModels
{
    public class EditSurveyViewModel
    {
        public int SurveyId { get; set; }

        [Required(ErrorMessage="Survey name is required")]
        public string SurveyName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Survey question is required")]
        public string SurveyQuestion { get; set; }     

        public bool FeaturedSurvey { get; set; }



        public List<SelectListItem> SurveyOptions { get; set; }

        public bool ShowDeleteOption { get; set; } = false;

        public void AddSurveyOption(SurveyOption option)
        {
            SelectListItem optionToAdd = new SelectListItem {Selected = false, Text = option.Description, Value = option.SurveyOptionId.ToString()};
            SurveyOptions.Add(optionToAdd);
            ShowDeleteOption = false;
        }

        public void RemoveSurveyOption(int optionId)
        {
            SurveyOptions.Remove(SurveyOptions.SingleOrDefault(x => x.Value == optionId.ToString()));

            if (SurveyOptions.Count == 1)
            {
                SurveyOptions.First().Text = "Please add an option...";
                SurveyOptions.ForEach(x => x.Selected = false);

            }
            ShowDeleteOption = false;
        }
    }
}
