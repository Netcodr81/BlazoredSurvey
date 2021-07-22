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

        public Survey GenerateSurveyToSave()
        {
            return new Survey()
            {
                SurveyName = this.SurveyName,
                Description = this.Description,
                SurveyQuestion = this.SurveyQuestion,
                FeaturedSurvey = this.FeaturedSurvey,
                TotalVotes = 0,
                TotalTimesTaken = 0,
                CreatedOn = DateTime.Now,
                SurveyOptions = SurveyOptionsToAdd
            };
        }
        public void AddSurveyOption(SurveyOption option, int maxId)
        {
            SelectListItem optionToAdd = new SelectListItem { Selected = false, Text = option.Description, Value = maxId.ToString() };
            SurveyOptions.Add(optionToAdd);
            
            ShowDeleteOption = false;
        }

        public void RemoveSurveyOption(int optionId)
        {
            string description = SurveyOptions.FirstOrDefault(x => x.Value == optionId.ToString()).Text;

            SurveyOptionsToAdd.Remove(SurveyOptionsToAdd.FirstOrDefault(x => x.Description == description));

            SurveyOptions.Remove(SurveyOptions.FirstOrDefault(x => x.Value == optionId.ToString()));

            if (SurveyOptions.Count == 1)
            {
                SurveyOptions.First().Text = "Please add an option...";

            }

            ShowDeleteOption = false;
        }

        public int GetMaxId()
        {
            var maxId = this.SurveyOptions.Count == 1 ? 0 : Int32.Parse(this.SurveyOptions.OrderByDescending(x => x.Value).FirstOrDefault().Value);
            maxId = maxId += 1;
            return maxId;
        }
        #endregion


    }
}
