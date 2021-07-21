using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlazorSurvey.Utils.CustomValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurveyAccessor.Context;
using SurveyAccessor.Models;

namespace BlazorSurvey.ViewModels
{
    public class AddSurveyViewModel
    {
        public AddSurveyViewModel()
        {
            SurveyOptions.Add(new SelectListItem() { Text = "Please add an option...", Value = "", Selected = true });
        }

        #region Properties
        [Required(ErrorMessage = "Survey name is required")]
        public string SurveyName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Survey question is required")]
        public string SurveyQuestion { get; set; }

        public List<SelectListItem> SurveyOptions { get; set; } = new List<SelectListItem>() { };

        [RequiredNumberOfItems(RequiredNumberOfRecords = 2, ErrorMessage = "At least 2 options are required")]
        public List<SurveyOption> SurveyOptionsToAdd { get; set; } = new List<SurveyOption>();

        public bool ShowDeleteOption { get; set; } = false;
        public SurveysDbContext Context { get; }
        public int MaxSurveyOptionId { get; set; }

        public bool FeaturedSurvey { get; set; }


        #endregion


        #region Methods
        public void AddSurveyOption(SurveyOption option, int maxId)
        {
            SelectListItem optionToAdd = new SelectListItem { Selected = false, Text = option.Description, Value = maxId.ToString() };
            SurveyOptions.Add(optionToAdd);
            
            ShowDeleteOption = false;
        }

        public async Task RemoveSurveyOption(int optionId)
        {

            SurveyOptions.Remove(SurveyOptions.SingleOrDefault(x => x.Value == optionId.ToString()));

            if (SurveyOptions.Count == 1)
            {
                SurveyOptions.First().Text = "Please add an option...";

            }

            ShowDeleteOption = false;
        }


        #endregion


    }
}
