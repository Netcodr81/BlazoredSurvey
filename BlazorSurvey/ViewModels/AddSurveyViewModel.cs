using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BlazorSurvey.Utils.CustomValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveyAccessor.Context;
using SurveyManager.DTO;

namespace BlazorSurvey.ViewModels
{
    public class AddSurveyViewModel
    {
       
        #region Properties
        [Required(ErrorMessage = "Survey name is required")]
        public string SurveyName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Survey question is required")]
        public string SurveyQuestion { get; set; }

        public List<SelectListItem> SurveyOptions { get; set; } = new List<SelectListItem>() { };

        [RequiredNumberOfItems(RequiredNumberOfRecords = 2, ErrorMessage = "At least 2 options are required")]
        public List<SurveyOptionDTO> SurveyOptionsToAdd { get; set; } = new List<SurveyOptionDTO>();
     
        public SurveysDbContext Context { get; }
        public int MaxSurveyOptionId { get; set; }

        public bool FeaturedSurvey { get; set; }


        #endregion


        #region Methods

        public SurveyDTO GenerateSurveyToSave()
        {
            return new SurveyDTO()
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
        public void AddSurveyOption(SurveyOptionDTO option, int maxId)
        {
            SelectListItem optionToAdd = new SelectListItem { Selected = false, Text = option.Description, Value = maxId.ToString() };
            SurveyOptions.Add(optionToAdd);
            
            
        }

        public void RemoveSurveyOption(int optionId)
        {
            string description = SurveyOptions.FirstOrDefault(x => x.Value == optionId.ToString()).Text;

            SurveyOptionsToAdd.Remove(SurveyOptionsToAdd.FirstOrDefault(x => x.Description == description));

            SurveyOptions.Remove(SurveyOptions.FirstOrDefault(x => x.Value == optionId.ToString()));

            
        }

        public int GetMaxId()
        {
            var maxId = this.SurveyOptions.Count == 0 ? 0 : Int32.Parse(this.SurveyOptions.OrderByDescending(x => x.Value).FirstOrDefault().Value);
            maxId = maxId += 1;
            return maxId;
        }
        #endregion


    }
}
